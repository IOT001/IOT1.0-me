using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
  public class SYSRole
    {
        /// <summary>
        /// ROLE_Id
        /// </summary>				
        public int ROLE_Id { get; set; }
        /// <summary>
        /// ROLE_Name
        /// </summary>				
        public string ROLE_Name { get; set; }
        /// <summary>
        /// ROLE_Name_En
        /// </summary>				
        public string ROLE_Name_En { get; set; }
        /// <summary>
        /// 0为超级管理员
        /// </summary>				
        public int ROLE_Level { get; set; }
        /// <summary>
        /// ROLE_Desc
        /// </summary>				
        public string ROLE_Desc { get; set; }
        /// <summary>
        /// ROLE_OrderIndex
        /// </summary>				
        public int ROLE_OrderIndex { get; set; }
        /// <summary>
        /// ROLE_IsSuspended
        /// </summary>				
        public bool ROLE_IsSuspended { get; set; }
        /// <summary>
        /// ROLE_Mark
        /// </summary>				
        public string ROLE_Mark { get; set; }
        /// <summary>
        /// ROLE_CreatedBy
        /// </summary>				
        public string ROLE_CreatedBy { get; set; }
        /// <summary>
        /// ROLE_CreatedOn
        /// </summary>				
        public DateTime? ROLE_CreatedOn { get; set; }
        /// <summary>
        /// ROLE_LastUpdBy
        /// </summary>				
        public string ROLE_LastUpdBy { get; set; }
        /// <summary>
        /// ROLE_LastUpdOn
        /// </summary>				
        public DateTime? ROLE_LastUpdOn { get; set; }

    }
  /// <summary>
  /// Deploy：实体对象映射关系
  /// </summary>
  [Serializable]
  public sealed class SYS_SystemRoleORMMapper : ClassMapper<SYSRole>
  {
      public SYS_SystemRoleORMMapper()
      {
          base.Table("SYS_SystemRole");
          Map(f => f.ROLE_Id).Key(KeyType.Identity);//设置 
          //Map(f => f.ACC_Account).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
          AutoMap();
      }
  }
}
