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
            orderby = "ID";//排序信息
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

    }
}
