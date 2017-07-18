using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    using System;
    using System.Collections.Generic;

    public partial class SYSAccountRole
    { 
        public int AR_Id { get; set; }
        public int AR_AccountId { get; set; }
        public int AR_SystemRoleId { get; set; }
        public string[] AR_SystemRoleIdS { get; set; } 
     
    }

    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class SYSSystemRoleORMMapper : ClassMapper<SYSAccountRole>
    {
        public SYSSystemRoleORMMapper()
        {
            base.Table("SYS_AccountRole");

            Map(f => f.AR_SystemRoleIdS).Ignore();//设置忽略
            Map(f => f.AR_Id).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }

}
