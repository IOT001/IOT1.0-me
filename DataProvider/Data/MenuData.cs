using DataProvider.Entities;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Data
{
   public class MenuData
    {
       public static string preurl = Common.GetAppConfig<string>("preurlconfig", "");
       public static List<vw_Menu> GetMenuList(string staffid)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("SELECT *,'{0}' + URL as logurl ", preurl);
           sb.AppendFormat(" FROM [vw_StaffMenu] a WITH(NOLOCK)");
           sb.AppendFormat(" ORDER BY ParentId,OrderIndex");
           return MsSqlMapperHepler.SqlWithParams<vw_Menu>(sb.ToString(), null, DBKeys.PRX);
       }
    }
}
