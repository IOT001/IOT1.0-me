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
    public class ClasshourDetailedlistData
    {
       /// <summary>
       /// 分页获取学生列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_AttendanceRecord> ClasshourDetailedlist(ClasshourDetailedSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_AttendanceRecord ";//表或者视图
           orderby = "ID";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1  and AttendanceTypeID=2");
 
           if (!string.IsNullOrWhiteSpace(search.StudentID))//学号
               sb.AppendFormat(" and  StudentID ='{0}' ", search.StudentID);


           if (!string.IsNullOrWhiteSpace(search.ClassID))//班级ID
               sb.AppendFormat(" and  ClassID ='{0}' ", search.ClassID);
           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_AttendanceRecord>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_AttendanceRecord>(list, search.CurrentPage, search.PageSize, allcount);
       }

         
       

    }
}
