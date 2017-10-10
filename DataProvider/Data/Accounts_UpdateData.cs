using Dapper;
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
    public class Accounts_UpdateData
    {
       /// <summary>
       /// 跳转查询账号信息
       /// </summary>
       /// <param name="apid"></param>
       /// <returns></returns>
        public static List<SYSAccount> GetAccounts_Update(string ACC_Account)
        {
            String sql = "select * from SYS_Account where ACC_Account = @ACC_Account   ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@ACC_Account", ACC_Account);
            return MsSqlMapperHepler.SqlWithParams<SYSAccount>(sql, dynamic, DBKeys.PRX);  
        }

          
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SYSAccount GetSYS_AccountByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<SYSAccount>(ID, DBKeys.PRX);
        }
      
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateSYS_Account(SYSAccount sys)
        {
            SYSAccount sysa = Accounts_UpdateData.GetSYS_AccountByID(sys.ACC_Id);//获取对象
            Cloner<SYSAccount, SYSAccount>.CopyTo(sys, sysa);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(sysa, DBKeys.PRX);

        }



    }

}
