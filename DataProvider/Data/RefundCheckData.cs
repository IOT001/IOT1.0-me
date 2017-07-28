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
    public class RefundCheckData
    {
         

        /// <summary>
        /// 分页获取审核表的数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Refund> GetRefundList(RefundCheckListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Refund ";//表或者视图
            orderby = "ID desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.SutdentName))//姓名
                sb.AppendFormat(" and SutdentName like '%{0}%' ", search.SutdentName);
            if (!string.IsNullOrWhiteSpace(search.BindPhone))//学号
            {
                sb.AppendFormat(" and BindPhone like '%{0}%' ", search.BindPhone);
            }

            if (!string.IsNullOrWhiteSpace(search.timeStart))//开班时间
                sb.AppendFormat(" and CreateTime > = '{0}' ", search.timeStart);
            if (!string.IsNullOrWhiteSpace(search.timeEnd))//结束时间
                sb.AppendFormat(" and CreateTime <= '{0}' ", search.timeEnd);

            //if (!string.IsNullOrWhiteSpace(search.StateID))//学号
            //{
            //    sb.AppendFormat(" and StateID = '{0}' ", search.StateID);
            //} 
            where = sb.ToString();

            int allcount = 0;
            var list = CommonPage<vw_Refund>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Refund>(list, search.CurrentPage, search.PageSize, allcount);

        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Refund GetRefundByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<Refund>(ID, DBKeys.PRX);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateRefund(Refund re)
        {
            Refund Ref = RefundCheckData.GetRefundByID(re.ID);//获取对象
            Cloner<Refund, Refund>.CopyTo(re, Ref);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Ref, DBKeys.PRX);

        }



    }
}
