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
    
    public partial class vw_AttendanceRecord
    {
        public int ID { get; set; }
        public string StudentID { get; set; }
        public string ClassID { get; set; }
        public int ClassIndex { get; set; }
        public Nullable<System.DateTime> ClockTime { get; set; }
        public Nullable<int> AttendanceTypeID { get; set; }
        public string AttendanceTypeIDName { get; set; }
        public Nullable<int> AttendanceWayID { get; set; }
        public string Evaluate { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; } 
        public Nullable<System.DateTime> ClassDate { get; set; }
        public int CourseID { get; set; }
        public string weekday { get; set; }
        public string CourseName { get; set; }
        public string TeachersName { get; set; }
        public string JobContent { get; set; }
        /// <summary>
        /// 分校ID
        /// </summary>
        public string ComCode { get; set; }

        /// <summary>
        /// 分校名称
        /// </summary>
        public string CompName { get; set; }
        
    }
}
