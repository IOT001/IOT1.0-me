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
    public class AppointmentData
    {
        /// <summary>
        /// 返回所有的市场资源/预约单信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<Appointment> GetAPList(EnrollListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Appointment ";//表或者视图
            orderby = "CreateTime desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.ApName)) //按钮中文名称
                sb.AppendFormat(" and [ApName] like '%{0}%' ", search.ApName);
            if (!string.IsNullOrWhiteSpace(search.ApTel))//城市
                sb.AppendFormat(" and [ApTel] like '%{0}%' ", search.ApTel);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Appointment>.GetPageList(
            out allcount, table, fields: fields, where: where.Trim(),
            orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Appointment>(list, search.CurrentPage, search.PageSize, allcount);
        }
    }
}
