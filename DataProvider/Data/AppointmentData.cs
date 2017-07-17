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
    public class AppointmentData
    {
        /// <summary>
        /// 返回所有的市场资源/预约单信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Appointment> GetAPList(EnrollListSearchModel search)
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
            var list = CommonPage<vw_Appointment>.GetPageList(
            out allcount, table, fields: fields, where: where.Trim(),
            orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Appointment>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 根据主键获取一条预约信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Appointment GetOneByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<Appointment>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增预约记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Add(Appointment obj)
        {
            bool ret = false;
            try
            {
               MsSqlMapperHepler.Insert<Appointment>(obj, DBKeys.PRX);
               ret = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
        
        /// <summary>
        /// 修改预约记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Update(Appointment obj)
        {
            Appointment objTo = AppointmentData.GetOneByID(obj.ID);//获取对象
            obj.CreateTime = objTo.CreateTime;
            obj.ApStateID = objTo.ApStateID;
            Cloner<Appointment, Appointment>.CopyTo(obj, objTo);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(objTo, DBKeys.PRX);
        }
        /// <summary>
        /// 根据预约ID获取更近记录
        /// </summary>
        /// <param name="apid"></param>
        /// <returns></returns>
        public static List<FollowRecord> GetFollowListByAPID(string apid)
        {
            string sql = @" select * from FollowRecord where APID = @APID";
            var parameters = new DynamicParameters();
            parameters.Add("@APID", apid);

            return MsSqlMapperHepler.SqlWithParams<FollowRecord>(sql, parameters, DBKeys.PRX);
        }

        /// <summary>
        /// 新增跟进记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool AddFollow(FollowRecord obj)
        {
            bool ret = false;
            try
            {
                MsSqlMapperHepler.Insert<FollowRecord>(obj, DBKeys.PRX);
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
        /// <summary>
        /// 获取优惠下拉
        /// </summary>
        /// <returns></returns>
        public static List<Discount> GetDiscountItems()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append(" FROM [Discount] a WITH(NOLOCK)");
            sb.Append(" WHERE a.StateID <> 2");
            sb.Append(" ORDER BY CreateTime desc");
            return MsSqlMapperHepler.SqlWithParams<Discount>(sb.ToString(), null, DBKeys.PRX);
        }
    }
}
