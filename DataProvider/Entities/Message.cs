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
    
    public partial class Message
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 留言标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///留言内容
        /// </summary>
        public string Messageing { get; set; } 
        /// <summary>
        /// 主角色，表示那些角色有权限阅读，用分号分隔
        /// </summary>
        public string ToRoles { get; set; }
        /// <summary>
        /// 方便查询，存的中文，比如人事,财务，市场
        /// </summary>
        public string ToRolesName { get; set; }
        /// <summary>
        /// 父级ID，如果不为NULL，代表这是一条回复的留言
        /// </summary>
        public Nullable<int> ParentID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatorId { get; set; }
    }


    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class MessageORMMapper : ClassMapper<Message>
    {
        public MessageORMMapper()
        {
            base.Table("Message");

            //Map(f => f.socketouts).Ignore();//设置忽略
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }



}
