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
    public class FollowListData
    {
        /// <summary>
        /// 分页每日结账单列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_FollowList> GetFollowList(FollowListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_FollowList ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.Name))//学生姓名
                sb.AppendFormat(" and ApName like '%{0}%' ", search.Name);
            if (!string.IsNullOrWhiteSpace(search.Name))//联系电话
                sb.AppendFormat(" and ApTel like '%{0}%' ", search.Name);
            if (search.timeStart != null && search.timeEnd != null)//开班时间
                sb.AppendFormat(" and CreateTime between '{0}'  and  '{1}'", search.timeStart, search.timeEnd);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_FollowList>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_FollowList>(list, search.CurrentPage, search.PageSize, allcount);
        }
       



    }

}
