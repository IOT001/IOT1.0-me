using DataProvider.Entities;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Data
{
    public class MessageBrowseData
    {
        /// <summary>
        /// 根据角色的权限来获取相应的留言信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public static List<vw_Message> GetMessageList(List<string> roleids)
        {
            StringBuilder where = new StringBuilder();
            foreach (string rid in roleids)
            {
                where.AppendFormat("or ToRoles like {0}", rid);
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select top 100 * from vw_Message where 1=1 {0} {1}", where.ToString(), "order by CreateTime desc");
            List<vw_Message> ret = MsSqlMapperHepler.SqlWithParams<vw_Message>(sql.ToString(), null, DBKeys.PRX);
            return ret;
        }
    }
}
