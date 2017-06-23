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
   public class EnrollData
    {
        /// <summary>
        /// 新增报名记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Add(Enroll obj)
        {
            bool ret = false;
            try
            {
                MsSqlMapperHepler.Insert<Enroll>(obj, DBKeys.PRX);
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }




        /// <summary>
        /// 分页获取报名列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Enroll> GeEnrollList(EnrollListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Enroll ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.ApName))//姓名
                sb.AppendFormat(" and name like '%{0}%' ", search.ApName);
            if (!string.IsNullOrWhiteSpace(search.Enroll_StudentID))//学号
                sb.AppendFormat(" and studentid like '%{0}%' ", search.Enroll_StudentID);
            
            where = sb.ToString();

            int allcount = 0;
            var list = CommonPage<vw_Enroll>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Enroll>(list, search.CurrentPage, search.PageSize, allcount);

        }

    }
}
