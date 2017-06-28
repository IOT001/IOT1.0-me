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
    
    public partial class AttendanceRecord
    {
        /// <summary>
        /// 考勤记录表主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 学员号
        /// </summary>
        ///     /// <summary>
        public string StudentID { get; set; } 
        /// 班级编号
        /// </summary>
        public string ClassID { get; set; }
    
        /// <summary>
        /// 班次序号，也就是班级生成的集体上课记录
        /// </summary>
        public int ClassIndex { get; set; }
        /// <summary>
        /// 考勤打卡时间
        /// </summary>
        public Nullable<System.DateTime> ClockTime { get; set; }
        /// <summary>
        /// 字典表，出勤状态，出勤或缺勤
        /// </summary>
        public Nullable<int> AttendanceTypeID { get; set; }
        /// <summary>
        /// 打卡方式，微信，设备，教师
        /// </summary>
        public Nullable<int> AttendanceWayID { get; set; }
        /// <summary>
        /// 学员评价
        /// </summary>
        public string Evaluate { get; set; }
        /// <summary>
        /// 缺勤原因-字典表
        /// </summary>
        public int OutStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatorId { get; set; }
        /// <summary>
        /// UpdatorId
        /// </summary>				
        public string UpdatorId { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>				
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 剩余课时，联合查询enroll表中计算得到
        /// </summary>
        public int LeftHour { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }
    [Serializable]
    public sealed class AttendanceRecordORMMapper : ClassMapper<AttendanceRecord>
    {
        public AttendanceRecordORMMapper()
        {
            base.Table("AttendanceRecord");

            Map(f => f.LeftHour).Ignore();//设置忽略
            Map(f => f.Name).Ignore();//设置忽略
            Map(f => f.Phone).Ignore();//设置忽略
            //Map(f => f.StateName).Ignore();//设置忽略
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
