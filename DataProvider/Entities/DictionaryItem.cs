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
    using DapperExtensions.Mapper;
    using System;
    using System.Collections.Generic;
    
    public partial class DictionaryItem
    {
        /// <summary>
        /// 字典类别
        /// </summary>
        public int DicTypeID { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public int DicItemID { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DicItemName { get; set; }
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public int recordState { get; set; }
    }


    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class DictionaryItemORMMapper : ClassMapper<DictionaryItem>
    {
        public DictionaryItemORMMapper()
        {
            base.Table("DictionaryItem");

           // Map(f => f.socketouts).Ignore();//设置忽略
            Map(f => f.DicTypeID).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
            Map(f => f.DicItemID).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
             
            AutoMap();
        }
    }


}
