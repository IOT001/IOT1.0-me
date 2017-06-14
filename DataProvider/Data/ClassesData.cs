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
    public class ClassesData
    {
       /// <summary>
       /// 分页获取班级列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_Classes> GeClassesList(ClassesListSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_Classes ";//表或者视图
           orderby = "ID";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 ");
           if (!string.IsNullOrWhiteSpace(search.ClassName))//班级名称
               sb.AppendFormat(" and ClassName like '%{0}%' ", search.ClassName);
           if (!string.IsNullOrWhiteSpace(search.CourseID))//课程名称
               sb.AppendFormat(" and CourseID like '%{0}%' ", search.CourseID);
           if (search.StartTime_start != null && search.StartTime_end != null)//开班时间
               sb.AppendFormat(" and CreateTime between '{0}'  and  '{1}'", search.StartTime_start, search.StartTime_end);
           if (search.EndTime_start != null && search.EndTime_end != null)//结班时间
               sb.AppendFormat(" and CreateTime between '{0}'  and  '{1}'", search.EndTime_start, search.EndTime_end);
           if (!string.IsNullOrWhiteSpace(search.TeacherID))//当前讲师
               sb.AppendFormat(" and TeacherID = '{0}' ", search.TeacherID);
           where = sb.ToString();

           int allcount = 0;
           var list = CommonPage<vw_Classes>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_Classes>(list, search.CurrentPage, search.PageSize, allcount);
           
       }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Classes GetClassesByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<Classes>(ID, DBKeys.PRX);
        }
       /// <summary>
       /// 新增,返回的是主键
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static string AddStudent(Classes Clas)
       {
           return MsSqlMapperHepler.Insert<Classes>(Clas, DBKeys.PRX);
       }
       /// <summary>
       /// 保存
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static bool UpdateStudent(Classes Stu)
       {
           Classes Stuto = ClassesData.GetClassesByID(Stu.ID);//获取对象
           Cloner<Classes, Classes>.CopyTo(Stu, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
           return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

       }

  



        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static Classes GetClassesList()
        {

            string strsql = "select id from Classes  order by CreateTime desc     ";
            var parameters = new DynamicParameters();
            return MsSqlMapperHepler.SqlWithParamsSingle<Classes>(strsql.ToString(), parameters, DBKeys.PRX);
             

        }


        /// <summary>
        /// 获取报表人数列表
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        //public static int Getnumber()
        //{

        //    string strsql = "select id from Classes  order by CreateTime desc     ";
        //    var parameters = new DynamicParameters();
        //    return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql.ToString(), parameters, DBKeys.PRX);


        //}



       

    }
}
