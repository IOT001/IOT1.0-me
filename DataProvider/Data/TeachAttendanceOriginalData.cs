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
    public class TeachAttendanceOriginalData
    {
       /// <summary>
        /// 分页获取考勤识别列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_AttendanceOriginal> GetAttendanceOriginalDataList(TeachAttendanceOriginalListSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_AttendanceOriginal ";//表或者视图
           orderby = "InputDate desc";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 AND len(UserID) <=5");
           if (!string.IsNullOrWhiteSpace(search.username))//姓名
               sb.AppendFormat(" and username like '%{0}%' ", search.username);  

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
       



    }
}
