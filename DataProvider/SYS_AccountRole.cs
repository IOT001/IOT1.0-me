//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYS_AccountRole
    {
        public int AR_Id { get; set; }
        public int AR_AccountId { get; set; }
        public int AR_SystemRoleId { get; set; }
        public byte[] AR_RowVersion { get; set; }
    
        public virtual SYS_Account SYS_Account { get; set; }
        public virtual SYS_SystemRole SYS_SystemRole { get; set; }
    }
}
