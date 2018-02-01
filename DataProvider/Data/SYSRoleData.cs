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
   public class SYSRoleData
    {
       public static SYSRole GetRoleByid(int roleid)
       {
           return MsSqlMapperHepler.GetOne<SYSRole>(roleid, DBKeys.PRX);
       }
    }
}
