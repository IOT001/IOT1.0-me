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
    public class EnrollManageListData
    {

        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddRefund(Refund obj)
        {
            return MsSqlMapperHepler.Insert<Refund>(obj, DBKeys.PRX);
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

    }
}
