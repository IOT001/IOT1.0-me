using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.SqlServer;
using DataProvider;
using Dapper;

namespace DataProvider.Data
{
    public class TimeData
    {
        /// <summary>
        /// 分页获取时间段列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<DictionaryItem> GetDictionaryItemList(DictionaryItemSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" dictionaryItem ";//表或者视图
            orderby = "CreateTime";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 AND  DicTypeID=8 ");
          
            if (!string.IsNullOrWhiteSpace(search.DicItemName))//时间段填写的条件查询 
                sb.AppendFormat(" and DicItemName like '%{0}%' ", search.DicItemName);
            if (!string.IsNullOrWhiteSpace(search.DicItemID))//时间段下拉框的条件查询 
                sb.AppendFormat(" and DicItemID = '{0}' ", search.DicItemID);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<DictionaryItem>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<DictionaryItem>(list, search.CurrentPage, search.PageSize, allcount);
        }



       

        /// <summary>
        /// 删除dictionaryItem表的数据
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateDictionaryItem(DictionaryItem DIC)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" DELETE FROM DictionaryItem ");
            sb.Append(" where DicTypeID=8 AND DicItemID=@DicItemID");
            var parameters = new DynamicParameters();
            parameters.Add("@DicItemID", DIC.DicItemID);    
            return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
        }
          

    }
}
