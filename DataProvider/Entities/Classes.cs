using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class Classes
    {
        /// <summary>
        /// ID
        /// </summary>			
        public string ID { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>			
        public string ClassName { get; set; }
        /// <summary>
        /// 课程ID，字典表
        /// </summary>			
        public Nullable<int> CourseID { get; set; }
        /// <summary>
        /// 授课方式，字典表
        /// </summary>		
        public Nullable<int> TeachTypeID { get; set; }
        /// <summary>
        /// 计划招收
        /// </summary>	
        public Nullable<int> PlanEnroll { get; set; }

        /// <summary>
        /// 总课时
        /// </summary>	
        public int TotalLesson { get; set; }
        /// <summary>
        /// 目前已上课时
        /// </summary>	
        public Nullable<int> PresentLesson { get; set; }
        /// <summary>
        /// 开课时间
        /// </summary>	
        public Nullable<System.DateTime> StartTime { get; set; }
          /// <summary>
        /// 预计结束时间
        /// </summary>	
        public Nullable<System.DateTime> EndTime { get; set; }

         /// <summary>
        ///    实际开课，当选排课的时候生成。
        /// </summary>	
        public Nullable<System.DateTime> ActualStartTime { get; set; }
     
         /// <summary>
        /// 时间段，如9:00~11:00,中间用波浪线表示
        /// </summary>	
        public int? TimePeriod { get; set; }
        /// <summary>
        /// 课程状态ID，字典表
        /// </summary>	
        public Nullable<int> StateID { get; set; }
        /// <summary>
        /// 教室ID
        /// </summary>	
        public Nullable<int> RoomID { get; set; }
        /// <summary>
        /// 讲师
        /// </summary>	
        public string TeacherID { get; set; }

        public string CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdatorId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeletorId { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        /// <summary>
        /// 报名费用
        /// </summary>
        public decimal Expenses { get; set; }
        /// <summary>
        /// 所属分校
        /// </summary>
        public string ComCode { get; set; }

        /// <summary>
        /// 讲师2
        /// </summary>	
        public string Teacher2ID { get; set; }

    }
}
