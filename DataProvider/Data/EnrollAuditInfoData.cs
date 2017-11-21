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
    public class EnrollAuditListData
    {
        /// <summary>
        /// 返回所有的市场资源/预约单信息   ,EnrollAuditList页面的 微信页面
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Appointment> GetEnrollAuditLis(EnrollAuditListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Appointment ";//表或者视图
            orderby = "CreateTime desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.ApStateID)) //判断值。
                sb.AppendFormat(" and ApStateID = {0} ", search.ApStateID);
             
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Appointment>.GetPageList(
            out allcount, table, fields: fields, where: where.Trim(),
            orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Appointment>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 获取待审的记录条数
        /// </summary>
        /// <returns></returns>
        public static int GetEnrollAuditListCount()
        {
            string strsql = "select count(*) from EnrollAudit where StateID = 2";
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql,null,DBKeys.PRX);
        }


    }
}
