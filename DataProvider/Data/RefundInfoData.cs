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
    public class RefundInfoData
    {

        /// <summary>
        /// 获取单条报名数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static vw_Enroll Getvw_EnrollByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<vw_Enroll>(ID, DBKeys.PRX);
        }

    

        /// <summary>
        /// 获取单条退款申请数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static vw_Refund Getvw_RefundByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<vw_Refund>(ID, DBKeys.PRX);
        }



        /// <summary>
        /// 获取单条退款申请数据
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
            Refund Ref = RefundInfoData.GetRefundByID(re.ID);//获取对象
            Cloner<Refund, Refund>.CopyTo(re, Ref);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Ref, DBKeys.PRX);

        }


        /// <summary>
        /// 判断是否已经有申请数据
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static int Refund(string StudentID, string EnrollID)
        {

            string strsql = "select count(ID) from Refund where StudentID=@StudentID and EnrollID=@EnrollID and StateID=1 ";
            var parameters = new DynamicParameters();
            parameters.Add("@EnrollID", EnrollID);
            parameters.Add("@StudentID", StudentID);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql.ToString(), parameters, DBKeys.PRX);



        }


        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddRefund(Refund obj)
        {
            return MsSqlMapperHepler.Insert<Refund>(obj, DBKeys.PRX);
        }

        


    }
}
