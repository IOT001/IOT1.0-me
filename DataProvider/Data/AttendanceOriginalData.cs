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
                                    ORDER BY ABS(DATEDIFF(Hour,ClassDate,'{0}'))", ao.workDates, ao.UserID);
                    ClassList cl = db.Query<ClassList>(str.ToString()).FirstOrDefault();//找到唯一班次
                    if (cl != null)//打卡正确，找到班次
                    {
                        //是否之前有考勤，有过考勤记录则不处理
                        string strar = "select top 1 * from AttendanceRecord where ClassID = '" + cl.ClassID + "' and ClassIndex = " + cl.ClassIndex + " and StudentID = '" + ao.UserID + "'";
                        AttendanceRecord ar = db.Query<AttendanceRecord>(strar).FirstOrDefault();
        
                        if (ar == null)//不存在就新增
                        {
                            ar = new AttendanceRecord();
                            ar.StudentID = ao.UserID;
                            ar.ClassID = cl.ClassID;
                            ar.ClassIndex = cl.ClassIndex;
                            ar.ClockTime = null;
                            ar.AttendanceTypeID = 1;//未考勤
                            ar.AttendanceWayID = null;
                            ar.CreateTime = DateTime.Now;
                            ar.CreatorId = operatorid;
                            int arid =  db.Insert(ar);
                            ar = db.GetById<AttendanceRecord>(arid);
                        }

                            if (ar.AttendanceTypeID == 1)//还没考勤过
                            {
                                ar.ClockTime = ao.workDates;
                                ar.AttendanceTypeID = 2;//正常
                                ar.AttendanceWayID = 2;//设备考勤
                                ar.UpdateTime = DateTime.Now;
                                ar.UpdatorId = operatorid;
                                string stren = "select * from Enroll where StudentID = '" + ao.UserID + "' and ClassID = '" + cl.ClassID + "'";
                                Enroll en = db.Query<Enroll>(stren).FirstOrDefault();// 找到报名记录
                                if (en == null)
                                {
                                    ao.Recognise = "无效";
                                    ao.Remark = "没有报名记录";
                                    ao.RecogniseTime = DateTime.Now;
                                    db.Update(ao);
                                    return;
                                    //throw new Exception("没有报名记录");
                                }
                                else//扣掉学时
                                {
                                    //-----添加课时变化日志记录 begin
                                    TransferRecord tr = new TransferRecord();//添加课时变化日志记录
                                    tr.StudentID = en.StudentID;
                                    tr.BeforeHours = en.ClassHour - en.UsedHour;
                                    tr.AfterHours = en.ClassHour - en.UsedHour - 1;
                                    tr.TypeID = 4;//考勤机自动识别
                                    tr.CreateTime = DateTime.Now;
                                    tr.CreatorId = operatorid;
                                    tr.ENID = en.ID;
                                    tr.ClassID = en.ClassID;
                                    tr.ClassIndex = ar.ClassIndex;
                                    db.Insert(tr);
                                    //-----添加课时变化日志记录 end

                                    en.UsedHour = en.UsedHour + 1;
                                    en.UpdateTime = DateTime.Now;
                                    en.UpdatorId = operatorid;
                                    db.Update(en);
                                    if (cl.StateID < 3)//班级状态如果没上课，设置成上课
                                    {
                                        cl.StateID = 3;
                                        db.Update(cl);
                                    }
                                    ao.Recognise = "有效";
                                    ao.Remark = "更新考勤记录，识别成功,当前剩余课时：" + (en.ClassHour - en.UsedHour).ToString();
                                    ao.Classid = cl.ClassID;
                                    ao.ClassIndex = cl.ClassIndex;
                                    ao.RecogniseTime = DateTime.Now;
                                    db.Update(ao);
                                }
                                ar.Remark = ao.Remark;
                                db.Update(ar);
                            }
                            else{
                                ao.Recognise = "无效";
                                ao.Remark = "重复的考勤";
                                ao.RecogniseTime = DateTime.Now;
                                ao.Classid = cl.ClassID;
                                ao.ClassIndex = cl.ClassIndex;
                                db.Update(ao);
                            }

                    }
                    else//没找到对应的班次
                    {
                        ao.Recognise = "无效";
                        ao.Remark = "未找到对应的班次";
                        ao.RecogniseTime = DateTime.Now;
                        db.Update(ao);
                    }
                }
                db.Commit();
                db.Dispose();
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
