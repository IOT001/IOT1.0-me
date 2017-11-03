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
    public class ReimburseData
    {
        /// <summary>
        /// 报销列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Reimburse> GetReimburseList(ReimbursetListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Reimburse ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.TeacherID))//老师名称
                sb.AppendFormat(" and TeacherID = '{0}' ", search.TeacherID);

            if (!string.IsNullOrWhiteSpace(search.StateID))//状态
                sb.AppendFormat(" and StateID = '{0}' ", search.StateID);

            if (!string.IsNullOrWhiteSpace(search.CreateTime_start))//开班时间
                sb.AppendFormat(" and CreateTime > = '{0}' ", search.CreateTime_start);
            if (!string.IsNullOrWhiteSpace(search.CreateTime_end))//结束时间
                sb.AppendFormat(" and CreateTime <= '{0}' ", search.CreateTime_end);

            if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
                sb.AppendFormat(" and [ComCode] = '{0}' ", search.ComCode);

            if (!string.IsNullOrWhiteSpace(search.CreatorId))//创建人
                sb.AppendFormat(" and CreatorId = '{0}' ", search.CreatorId);

            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Reimburse>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Reimburse>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Reimburse GetReimburseByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<Reimburse>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddReimburse(Reimburse Rb)
        {
            return MsSqlMapperHepler.Insert<Reimburse>(Rb, DBKeys.PRX);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateReimburse(Reimburse Rb)
        {
            Reimburse Rbs = ReimburseData.GetReimburseByID(Rb.ID);//获取对象
            Cloner<Reimburse, Reimburse>.CopyTo(Rb, Rbs);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Rbs, DBKeys.PRX);

        }



    }

}
