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
    public class FileManageListData
    {
        /// <summary>
        /// 分页优惠表列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<Files> GetFileseList(FileManageListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" Files ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.FileName))//名称
                sb.AppendFormat(" and FileName like '%{0}%' ", search.FileName); 
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Files>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Files>(list, search.CurrentPage, search.PageSize, allcount);
        }
    
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddFiles(Files files)
        {
            return MsSqlMapperHepler.Insert<Files>(files, DBKeys.PRX);
        }
 


    }

}
