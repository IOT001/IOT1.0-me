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
   public class TeacherClassData
    {
        /// <summary>
        /// 分页获取教师上课表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
       public static PagedList<vw_ClassAttendanceList> GetAttendanceRecordList(TeacherClassListSearch search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_ClassAttendanceList ";//表或者视图
            orderby = "ClassIndex";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");


            if (!string.IsNullOrWhiteSpace(search.timeStart))//开班时间
                sb.AppendFormat(" and ClassDate > = '{0}' ", search.timeStart);
            if (!string.IsNullOrWhiteSpace(search.timeEnd))//结束时间
                sb.AppendFormat(" and ClassDate <= '{0}' ", search.timeEnd);


            if (!string.IsNullOrWhiteSpace(search.teacherID))//vw_AttendanceRecord ID
                sb.AppendFormat(" and  TeacherID ='{0}' ", search.teacherID);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_ClassAttendanceList>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_ClassAttendanceList>(list, search.CurrentPage, search.PageSize, allcount);
        }

       /// <summary>
       /// 根据班级ID获取学员考勤信息
       /// </summary>
       /// <param name="classid"></param>
       /// <returns></returns>
       public static List<vw_AttendanceRecord> GetAttendanceRecordByClassID(string classid, int ClassIndex)
       {
           string strsql = "select * from vw_AttendanceRecord where ClassID = '" + classid + "' and ClassIndex = " + ClassIndex;
           List<vw_AttendanceRecord> ret = new List<vw_AttendanceRecord>();
           ret = MsSqlMapperHepler.SqlWithParams<vw_AttendanceRecord>(strsql, null, DBKeys.PRX);
           return ret;
       }
       /// <summary>
       /// 获取本次上课信息
       /// </summary>
       /// <param name="classid"></param>
       /// <param name="classindex"></param>
       /// <returns></returns>
       public static vw_ClassAttendanceList GetOneClassAttendanceList(string classid, int classindex)
       {
           string strsql = "select * from vw_ClassAttendanceList where ClassID = '" + classid + "' and ClassIndex = " + classindex;
           vw_ClassAttendanceList ret = new vw_ClassAttendanceList();
           ret = MsSqlMapperHepler.SqlWithParamsSingle<vw_ClassAttendanceList>(strsql, null, DBKeys.PRX);
           return ret;
       }







       /// <summary>
       /// 获取文件路径
       /// </summary>
       /// <param name="classId"></param>
       /// <param name="classIndex"></param>
       /// <returns></returns>
       public static List<vw_ClassListJob> ClassListJob(string classid, int classindex)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_ClassListJob ";//表或者视图
           orderby = "id";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append("select * from vw_ClassListJob where ");
           sb.Append(" 1=1 ");


           if (!string.IsNullOrWhiteSpace(classid))//班级ID
               sb.Append(" and classid = @ClassID");
           if (classindex != 0)//班级行号
               sb.Append(" and classindex = @ClassIndex  ");
           sb.Append(" order by id ");


           var parameters = new DynamicParameters();
           parameters.Add("@classid", classid);
           parameters.Add("@classindex", classindex);
           return MsSqlMapperHepler.SqlWithParams<vw_ClassListJob>(sb.ToString(), parameters, DBKeys.PRX);

       }


       public static int UpdateClassList(ClassList cls)
       {

           StringBuilder sb = new StringBuilder();
           sb.Append(" update ClassList set JobTitle=@JobTitle  ");
           sb.Append(" , JobContent=@JobContent  ");
           sb.Append(" where ClassID=@ClassID ");
           sb.Append(" AND ClassIndex=@ClassIndex ");
           var parameters = new DynamicParameters();
           parameters.Add("@JobTitle", cls.JobTitle);
           parameters.Add("@JobContent", cls.JobContent);
           parameters.Add("@ClassID", cls.ClassID);
           parameters.Add("@ClassIndex", cls.ClassIndex);
           return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
       }


       /// <summary>
       /// 新增,返回的是主键
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
       public static int AddClassListJob(ClassListJob Clas)
       {
           return MsSqlMapperHepler.Insert<ClassListJob>(Clas, DBKeys.PRX);
       }



    }
}
