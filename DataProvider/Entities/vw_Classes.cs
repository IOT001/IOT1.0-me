using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class vw_Classes
    {
        public string ID { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public int CourseID { get; set; }
        public string TeachTypeID { get; set; }
        public string Enroll { get; set; } 
        public string Lesson { get; set; } 
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string TimePeriod { get; set; } 
        public string StateIDname { get; set; }
        public int StateID { get; set; }
        public int RoomID { get; set; } 
        public string RoomIDname { get; set; }
        
        public string TeacherID { get; set; }
        public string Teacher2ID { get; set; }
        public string name { get; set; } 
        public string CreatorId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string UpdatorId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeletorId { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public int StateIDed { get; set; }
        /// <summary>
        /// 初始报名费用
        /// </summary>
        public decimal Expenses { get; set; }
        /// <summary>
        /// 总课时
        /// </summary>
        public int TotalLesson { get; set; }
        /// <summary>
        /// 授课方式中文
        /// </summary>
        public string TeachTypeIDname { get; set; }
        /// <summary>
        /// 所属分校
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 所属分校ID
        /// </summary>
        public string ComCode { get; set; }
          /// <summary>
        /// 上课时段
        /// </summary>
        public string TimePeriodName { get; set; }
        
    }
}
