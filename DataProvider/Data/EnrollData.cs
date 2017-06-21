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
    }
}
