using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    /// <summary>
    /// 课程考勤列表
    /// </summary>
    public class vw_ClassAttendanceList
    {
        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID
        {
            get;
            set;
        }
        /// <summary>
        /// ClassIndex
        /// </summary>
        public int ClassIndex
        {
            get;
            set;
        }
        /// <summary>
        /// ClassName
        /// </summary>
        public string ClassName
        {
            get;
            set;
        }
        /// <summary>
        /// ClassDate
        /// </summary>
        public DateTime ClassDate
        {
            get;
            set;
        }
        /// <summary>
        /// TimePeriod
        /// </summary>
        public string TimePeriod
        {
            get;
            set;
        }
        public string TimePeriodValue
        {
            get;
            set;
        }
        /// <summary>
        /// StateID
        /// </summary>
        public string StateID
        {
            get;
            set;
        }
        /// <summary>
        /// TeacherID
        /// </summary>
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// TeacherID
        /// </summary>
        public string Teacher2ID
        {
            get;
            set;
        }
        /// <summary>
        /// RoomID
        /// </summary>
        public int? RoomID
        {
            get;
            set;
        }
        /// <summary>
        /// TeacherName
        /// </summary>
        public string TeacherName
        {
            get;
            set;
        }
        /// <summary>
        /// RoomName
        /// </summary>
        public string RoomName
        {
            get;
            set;
        }
        /// <summary>
        /// SumStudents
        /// </summary>
        public int? SumStudents
        {
            get;
            set;
        }
        /// <summary>
        /// CheckINStudents
        /// </summary>
        public int? CheckINStudents
        {
            get;
            set;
        }
        /// <summary>
        /// UnCheckStudents
        /// </summary>
        public int? UnCheckStudents
        {
            get;
            set;
        }
        /// <summary>
        /// EvaluateStudents
        /// </summary>
        public int? EvaluateStudents
        {
            get;
            set;
        }
        public int? TotalLesson
        {
            get;
            set;
        }

        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 所属分校ID
        /// </summary>
        public string ComCode { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class vw_ClassAttendanceListORMMapper : ClassMapper<vw_ClassAttendanceList>
    {
        public vw_ClassAttendanceListORMMapper()
        {
            base.Table("vw_ClassAttendanceList");

            //Map(f => f.socketouts).Ignore();//设置忽略
            //Map(f => f.ClassID).Key(KeyType.Assigned);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
