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
    public class AttendaceData
    {
        public static PagedList<vw_ClassAttendanceList> GetButtonList(AttendanceSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_ClassAttendanceList ";//表或者视图
            orderby = "ClassDate";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
           

            if (!string.IsNullOrWhiteSpace(search.className))//按钮中文名称
                sb.AppendFormat(" and ClassName like '%{0}%' ", search.className);
            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_ClassAttendanceList>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_ClassAttendanceList>(list, search.CurrentPage, search.PageSize, allcount);
        }
    }
}
