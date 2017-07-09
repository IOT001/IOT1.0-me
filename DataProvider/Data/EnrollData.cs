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
   public class EnrollData
    {
        /// <summary>
        /// 新增报名记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Add(Enroll obj)
        {
            bool ret = false;
            try
            {
                MsSqlMapperHepler.Insert<Enroll>(obj, DBKeys.PRX);
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
       /// <summary>
       /// 批量新增报名记录
       /// </summary>
       /// <param name="list"></param>
       /// <returns></returns>
        public static bool AddList(List<Enroll> list)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {
                            
                foreach (var obj in list)
                {
                    db.Insert<Enroll>(obj);
                }
                ret = true;
                db.Commit();
            }          
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }
        public static Enroll getEnrollByStudentClass(string studentID, string classId)
        {
            String sql = "select * from Enroll where StudentID = @StudentID and ClassID = @ClassID ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);

            return MsSqlMapperHepler.SqlWithParamsSingle<Enroll>(sql, dynamic, DBKeys.PRX);            
        }

        public static Enroll GetEnrollByID(String ID)
        {
            return MsSqlMapperHepler.GetOne<Enroll>(ID, DBKeys.PRX);
        }

        /// <summary>
        /// 更新 教师信息
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateEnroll(Enroll btn)
        {
            Enroll btnto = EnrollData.GetEnrollByID(btn.ID);//获取对象
            Cloner<Enroll, Enroll>.CopyTo(btn, btnto);
            //Cloner<Teachers, Teachers>.CopyTo(btn, btnto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
        }




        /// <summary>
        /// 分页获取报名列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Enroll> GeEnrollList(EnrollListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Enroll ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.ApName))//姓名
                sb.AppendFormat(" and name like '%{0}%' ", search.ApName);
            if (!string.IsNullOrWhiteSpace(search.Enroll_StudentID))//学号
            {
                sb.AppendFormat(" and studentid like '%{0}%' ", search.Enroll_StudentID);
            }
            if (!string.IsNullOrWhiteSpace(search.ClassID))//班级号
            {
                sb.AppendFormat(" and ClassID = '{0}' ", search.ClassID);
            }
            
            where = sb.ToString();

            int allcount = 0;
            var list = CommonPage<vw_Enroll>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Enroll>(list, search.CurrentPage, search.PageSize, allcount);

        }
       /// <summary>
       /// 跟进预约单获取报名记录，用于打印票据
       /// </summary>
       /// <param name="apid"></param>
       /// <returns></returns>
        public static List<vw_Enroll> GetEnrollPrintByApid(string apid)
        {
            String sql = "select * from vw_Enroll where APID = @APID  ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@APID", apid);


            return MsSqlMapperHepler.SqlWithParams<vw_Enroll>(sql, dynamic, DBKeys.PRX);  
        }
       /// <summary>
       /// 获取单据配置信息
       /// </summary>
       /// <returns></returns>
        public static BillConfig GetOneBillConfig()
        {
            return MsSqlMapperHepler.GetOne<BillConfig>(1, DBKeys.PRX);
        }
    }
}
