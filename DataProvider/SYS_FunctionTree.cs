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
    
    public partial class SYS_FunctionTree
    {
        public SYS_FunctionTree()
        {
            this.SYS_FunctionItem = new HashSet<SYS_FunctionItem>();
            this.SYS_FunctionTree1 = new HashSet<SYS_FunctionTree>();
        }
    
        public int FT_Id { get; set; }
        public string FT_Name { get; set; }
        public string FT_Name_En { get; set; }
        public string FT_Desc { get; set; }
        public int FT_OrderIndex { get; set; }
        public string FT_FormNameOrUrl { get; set; }
        public bool FT_IsSuspended { get; set; }
        public Nullable<int> FT_ParentId { get; set; }
        public string FT_Mark { get; set; }
        public string FT_CreatedBy { get; set; }
        public System.DateTime FT_CreatedOn { get; set; }
        public string FT_LastUpdBy { get; set; }
        public Nullable<System.DateTime> FT_LastUpdOn { get; set; }
        public byte[] FT_RowVersion { get; set; }
        public string FT_Type { get; set; }
    
        public virtual ICollection<SYS_FunctionItem> SYS_FunctionItem { get; set; }
        public virtual ICollection<SYS_FunctionTree> SYS_FunctionTree1 { get; set; }
        public virtual SYS_FunctionTree SYS_FunctionTree2 { get; set; }
    }
}
