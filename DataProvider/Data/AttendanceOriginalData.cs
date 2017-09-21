using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Data
{
    public class AttendanceOriginalData
    {
       /// <summary>
        /// 分页获取考勤识别列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_AttendanceOriginal> GetAttendanceOriginalDataList(AttendanceOriginalListSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_AttendanceOriginal ";//表或者视图
           orderby = "InputDate desc";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 ");
           if (!string.IsNullOrWhiteSpace(search.username))//姓名
               sb.AppendFormat(" and username like '%{0}%' ", search.username);
           if (!string.IsNullOrWhiteSpace(search.UserID))//学号
               sb.AppendFormat(" and UserID like '%{0}%' ", search.UserID);
           if (!string.IsNullOrWhiteSpace(search.ClassName))//班级名称
               sb.AppendFormat(" and ClassName like '%{0}%' ", search.ClassName);

           if (!string.IsNullOrWhiteSpace(search.InputDate_start))//开班时间
               sb.AppendFormat(" and InputDate > = '{0}' ", search.InputDate_start);
           if (!string.IsNullOrWhiteSpace(search.InputDate_end))//结束时间
               sb.AppendFormat(" and InputDate <= '{0}' ", search.InputDate_end);
           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_AttendanceOriginal>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_AttendanceOriginal>(list, search.CurrentPage, search.PageSize, allcount);
       }
        /// <summary>
        /// 处理原始考勤记录
        /// </summary>
        public static void DealAttendanceOriginal(string operatorid)
        {

            string strsql = "select * from AttendanceOriginal where RecogniseTime is null";
            List<AttendanceOriginal> aolist = MsSqlMapperHepler.SqlWithParams<AttendanceOriginal>(strsql, null, DBKeys.PRX);//获取所有未识别的数据
            DBRepository db = new DBRepository(DBKeys.PRX);
            try
            {
                db.BeginTransaction();
                foreach (AttendanceOriginal ao in aolist)//循环处理
                {
                    //智能匹配到这个学员报名过的，时间最接近的班
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat(@"SELECT TOP 1 * from ClassList 
                                    WHERE ABS(DATEDIFF(Hour,ClassDate,'{0}')) < 4 
                                    AND ClassID IN (SELECT ClassID FROM Enroll WHERE StudentID = {1}) 
                                    ORDER BY ABS(DATEDIFF(Hour,ClassDate,GETDATE()))", ao.workDates, ao.UserID);
                    ClassList cl = MsSqlMapperHepler.SqlWithParamsSingle<ClassList>(str.ToString(), null, DBKeys.PRX);//找到唯一班次
                    if (cl != null)//打卡正确，找到班次
                    {
                        //是否之前有考勤，有过考勤记录则不处理
                        string strar = "select top 1 * from AttendanceRecord where ClassID = '" + cl.ClassID + "' and ClassIndex = " + cl.ClassIndex + " and StudentID = '" + ao.UserID + "'";
                        AttendanceRecord ar = MsSqlMapperHepler.SqlWithParamsSingle<AttendanceRecord>(strar, null, DBKeys.PRX);
                        if (ar == null)//不存在就新增
                        {
                            ar = new AttendanceRecord();
                            ar.StudentID = ao.UserID;
                            ar.ClassID = cl.ClassID;
                            ar.ClassIndex = ar.ClassIndex;
                            ar.ClockTime = ao.workDates;
                            ar.AttendanceTypeID = 2;//正常
                            ar.AttendanceWayID = 2;//设备考勤
                            ar.CreateTime = DateTime.Now;
                            ar.CreatorId = operatorid;
                            db.Insert(ar);

                            string stren = "select * from Enroll where StudentID = '" + ao.UserID + "' and ClassID = '" + cl.ClassID + "'";
                            Enroll en = MsSqlMapperHepler.SqlWithParamsSingle<Enroll>(stren, null, DBKeys.PRX);// 找到报名记录
                            if (en == null)
                            {
                                throw new Exception("没有报名记录");
                            }
                            else//扣掉学时
                            {
                                en.UsedHour = en.UsedHour + 1;
                                en.UpdateTime = DateTime.Now;
                                en.UpdatorId = operatorid;
                                db.Update(en);
                            }
                        }
                        else
                        {
                            if (ar.ClockTime == null)//没考勤过
                            {
                                ar.ClockTime = ao.workDates;
                                ar.AttendanceTypeID = 2;//正常
                                ar.AttendanceWayID = 2;//设备考勤
                                ar.UpdateTime = DateTime.Now;
                                ar.UpdatorId = operatorid;
                                db.Update(ar);

                                string stren = "select * from Enroll where StudentID = '" + ao.UserID + "' and ClassID = '" + cl.ClassID + "'";
                                Enroll en = MsSqlMapperHepler.SqlWithParamsSingle<Enroll>(stren, null, DBKeys.PRX);// 找到报名记录
                                if (en == null)
                                {
                                    throw new Exception("没有报名记录");
                                }
                                else//扣掉学时
                                {
                                    en.UsedHour = en.UsedHour + 1;
                                    en.UpdateTime = DateTime.Now;
                                    en.UpdatorId = operatorid;
                                    db.Update(en);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
        }
    }
}
