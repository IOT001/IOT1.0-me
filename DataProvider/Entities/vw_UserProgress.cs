using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class vw_UserProgress
    {
        /// <summary>
        /// 学号
        /// </summary>			
        public string StudentID { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>			
        public string ClassID { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>			
        public string ClassName { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>		
        public string CourseName { get; set; }
         
        /// <summary>
        /// 课程介绍
        /// </summary>	
        public string Introduce { get; set; }
        /// <summary>
        /// 报名未用课时
        /// </summary>
        public Nullable<int> ClassHour { get; set; }
        /// <summary>
        /// 考勤已用课时
        /// </summary>
        public Nullable<int> UsedHour { get; set; }

        public int Progress_Bar { get; set; } 

    }
}
