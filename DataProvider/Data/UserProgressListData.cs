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
    public class UserProgressListData
    {
       /// <summary>
        /// 分页获取UserProgressListData页面数据
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_UserProgress> Getvw_UserProgressList(vw_UserProgressSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_UserProgress ";//表或者视图
           orderby = "StudentID";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 "); 
           if (!string.IsNullOrWhiteSpace(search.StudentID))//学生ID，学号
               sb.AppendFormat(" and  StudentID ='{0}' ", search.StudentID);
           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_UserProgress>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_UserProgress>(list, search.CurrentPage, search.PageSize, allcount);
       }


         
 
       

    }
}
