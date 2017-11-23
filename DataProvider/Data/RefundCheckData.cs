using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        /// 导出到Excel表格
        /// </summary>
        /// <returns></returns>
        public static DataTable DPExportToExcel(string Name, string BindPhone, string timeStart, string timeEnd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PrxConnectionString"].ToString()))//创建连接字符串，因为连接的库不同
                {
                    string sql = "";
                   // sql = " SELECT CreateTime as '报名日期',Name as '学员姓名',BindPhone as '学员电话',ClassName as '报名班级',ClassID as '班级编号',TotalLesson as '班级课时',ClassHour as '报名课时',Expenses as '班级费用',Paid as '报名费用',ReduceAmount as '优惠费用' FROM [vw_DailyReport] where 1=1";

                    if (!string.IsNullOrWhiteSpace(Name))//学生姓名
                        sql += " and SutdentName like '" + Name + "'";

                    if (!string.IsNullOrWhiteSpace(BindPhone))//联系电话
                        sql += " and BindPhone like  '" + BindPhone + "'";

                    if (!string.IsNullOrWhiteSpace(timeStart))//开班时间
                    {
                        sql += "and CreateTime >=  '" + timeStart + "'";
                    }

                    if (!string.IsNullOrWhiteSpace(timeEnd)) //结束时间
                    {
                        sql += "and CreateTime <=  '" + timeEnd + "'";
                    }

                    sql += "  order by ID desc ";


                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "转换的过程中发生了错误!");
            }
        }


    }
}
