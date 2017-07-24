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
    public class StudentScheduleListData
    {
       /// <summary>
       /// 分页获取学生列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_AttendanceRecord> GetAttendanceRecordList(StudentScheduleSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_AttendanceRecord ";//表或者视图
           orderby = "ID";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 ");
           if (!string.IsNullOrWhiteSpace(search.Name)) //学员姓名
               sb.AppendFormat(" and Name like '%{0}%' ", search.Name);
           //if (search.timeStart != null && search.timeEnd != null)//时间
           //    sb.AppendFormat(" and ClassDate between '{0}'  and  '{1}'", search.timeStart, search.timeEnd);

           if (!string.IsNullOrWhiteSpace(search.timeStart))//开班时间
               sb.AppendFormat(" and ClassDate > = '{0}' ", search.timeStart);
           if (!string.IsNullOrWhiteSpace(search.timeEnd))//结束时间
               sb.AppendFormat(" and ClassDate <= '{0}' ", search.timeEnd);

           if (!string.IsNullOrWhiteSpace(search.AttendanceRecord_StudentID))//学号
               sb.AppendFormat(" and  StudentID ='{0}' ", search.AttendanceRecord_StudentID);

           if (!string.IsNullOrWhiteSpace(search.AttendanceRecord_ID))//vw_AttendanceRecord ID
               sb.AppendFormat(" and  ID ='{0}' ", search.AttendanceRecord_ID);
           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_AttendanceRecord>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_AttendanceRecord>(list, search.CurrentPage, search.PageSize, allcount);
       }




        /// <summary>
        /// 获取FSConnectionInfo页面单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static vw_AttendanceRecord Getvw_AttendanceRecordByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<vw_AttendanceRecord>(ID, DBKeys.PRX);
        }
      


       /// <summary>
       /// 获取单条数据
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static AttendanceRecord GetAttendanceRecordByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<AttendanceRecord>(ID, DBKeys.PRX);
        }
      
       /// <summary>
       /// 保存
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static bool UpdateAttendanceRecord(AttendanceRecord att)
       {
           AttendanceRecord Stuto = StudentScheduleListData.GetAttendanceRecordByID(att.ID);//获取对象
           Cloner<AttendanceRecord, AttendanceRecord>.CopyTo(att, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
           return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

       }



        #region 获取字典列表
        /// <summary>
        /// 获取班级下拉
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>班级用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetClassesList(int CourseID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select distinct cl.id+','+CAST(cll.ClassIndex as nvarchar(10)) as id,cl.ClassName+'('+convert(varchar(100), cll.ClassDate ,23)+')' as Name");
            sb.Append(" from Classes cl left join ClassList cll");
            sb.Append(" on	cl.ID=cll.ClassID ");
            sb.Append(" where  cl.StateID <> 4 and 	cl.CourseID = @CourseID");
            var parameters = new DynamicParameters();
            parameters.Add("@CourseID", CourseID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);
        }
        #endregion

 
       

    }
}
