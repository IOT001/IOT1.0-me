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
    
    public partial class ClassList
    {
  
        /// <summary>
        ///班级号
        /// </summary>	
        public string ClassID { get; set; }
        /// <summary>
        /// 班级次号，表示第几节课
        /// </summary>	
        public int ClassIndex { get; set; }
        /// <summary>
        ///  上课日期
        /// </summary>	
        public System.DateTime ClassDate { get; set; }
        /// <summary>
        /// 上课的时段 比如 9:00~11:00
        /// </summary>	
        public int? TimePeriod { get; set; }
        /// <summary>
        /// 状态，取字典表
        /// </summary>	
        public int StateID { get; set; }
        /// <summary>
        /// 讲师ID
        /// </summary>	
        public string TeacherID { get; set; }
        /// <summary>
        /// 讲师ID2
        /// </summary>	
        public string Teacher2ID { get; set; }
        /// <summary>
        /// 教师ID，取字典表
        /// </summary>	
        public Nullable<int> RoomID { get; set; } 
         /// <summary>
        /// 星期几
        /// </summary>	
        public Nullable<int> weekday { get; set; } 
        

        public System.DateTime CreateTIme { get; set; }
        public string CreatorId { get; set; }
        /// <summary>
        /// 作业头
        /// </summary>
         public string JobTitle { get; set; }
        /// <summary>
        /// 作业内容
        /// </summary>
         public string JobContent { get; set; }
        
            
    }


    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class ClassListORMMapper : ClassMapper<ClassList>
    {
        public ClassListORMMapper()
        {
            base.Table("ClassList");

            // Map(f => f.socketouts).Ignore();//设置忽略
            Map(f => f.ClassID).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
            Map(f => f.ClassIndex).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)   

            AutoMap();
        }
    }

}
