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

    public partial class SYSAccount
    {
        
        public int ACC_Id { get; set; }
        public string ACC_Account { get; set; }
        public string ACC_Password { get; set; }
        public byte[] ACC_BinaryPassword { get; set; }
        public string ACC_Email { get; set; }
        public string ACC_MobilePhone { get; set; }
        public string ACC_ActiveStatus { get; set; }
        public string ACC_ActiveCode { get; set; }
        public Nullable<System.DateTime> ACC_ActiveDateTime { get; set; }
        public Nullable<System.DateTime> ACC_LastLoginTime { get; set; }
        public Nullable<System.DateTime> ACC_LastUpdatePwdTime { get; set; }
        public Nullable<int> ACC_UserKey1 { get; set; }
        public string ACC_UserKey2 { get; set; }
        public bool ACC_IsSuspended { get; set; }
        public string ACC_Mark { get; set; }
        public string ACC_CreatedBy { get; set; }
        public System.DateTime ACC_CreatedOn { get; set; }
        public string ACC_LastUpdBy { get; set; }
        public Nullable<System.DateTime> ACC_LastUpdOn { get; set; } 
        public string USRP_UserCode { get; set; } 
    }

    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class SYSAccountORMMapper : ClassMapper<SYSAccount>
    {
        public SYSAccountORMMapper()
        {
            base.Table("SYS_Account");
            Map(f => f.ACC_Id).Key(KeyType.Identity);//设置 
            Map(f => f.ACC_BinaryPassword).Ignore();//设置忽略
            Map(f => f.ACC_Account).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }

}
