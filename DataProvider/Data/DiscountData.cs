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
    public class DiscountData
    {
        /// <summary>
        /// 分页优惠表列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Discount> GetDiscountList(DiscountListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Discount ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.Name))//名称
                sb.AppendFormat(" and DiscountName like '%{0}%' ", search.Name); 
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Discount>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Discount>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Discount GetDiscountByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<Discount>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddDiscount(Discount Dis)
        {
            return MsSqlMapperHepler.Insert<Discount>(Dis, DBKeys.PRX);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateDiscount(Discount Dis)
        {
            Discount Stuto = DiscountData.GetDiscountByID(Dis.ID);//获取对象
            Cloner<Discount, Discount>.CopyTo(Dis, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

        }



    }

}
