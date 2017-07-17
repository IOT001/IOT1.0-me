using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Models;
using DataProvider.SqlServer;
using Dapper;

namespace DataProvider.Data
{
   public class CourseData
    {

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
       public static PagedList<Course> GetButtonList(CurriculumSearchModel search)
        {
            ///UPDATE TOP(1) [PRX].[dbo].[Course] SET [ID]='3', [CourseName]=N'绘画', [CoursePrice]='11.00', [StateID]='2', [Hours]='11', [Introduce]=N'没有介绍' WHERE ([ID]='3');
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  *,dbo.getDicNameByID(17,StateID) as StateName, dbo.getDicNameByID(17,TypeID) as TypeName ";//输出字段
            table = @" Course ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.CourseName))//按钮中文名称
                sb.AppendFormat(" and CourseName like '%{0}%' ", search.CourseName);
            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Course>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Course>(list, search.CurrentPage, search.PageSize, allcount);
        }

       public static Course getCourseById(int id)
       {

           return MsSqlMapperHepler.SqlWithParamsSingle<Course>("select  *,dbo.getDicNameByID(17,StateID) as StateName ,dbo.getDicNameByID(17,TypeID) as TypeName from  Course where ID =  " + id, null, DBKeys.PRX);
       }

       public static int addCourse(Course course)
       {
           return MsSqlMapperHepler.Insert<Course>(course, DBKeys.PRX);
       }

       public static bool updateCourse(Course course)
       {
           Course to = CourseData.getCourseById(course.ID);
           Cloner<Course, Course>.CopyTo(course, to);
           return MsSqlMapperHepler.Update(to, DBKeys.PRX);
       }
       /// <summary>
       /// 获取启用课程下拉
       /// </summary>
       /// <returns></returns>
       public static List<CommonEntity> GetCourseIL()
       {
           List<CommonEntity> ret;
           string sql = "select id,CourseName as name from Course";
           ret = MsSqlMapperHepler.SqlWithParams<CommonEntity>(sql.ToString(), null, DBKeys.PRX);
           return ret;
       }
    }
}
