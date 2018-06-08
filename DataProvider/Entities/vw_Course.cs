using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    //Course
    public class vw_Course
    {

        /// <summary>
        /// 课程主键自增
        /// </summary>
        public  int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 课程名称
        /// </summary>
        public  string CourseName
        {
            get;
            set;
        }
        /// <summary>
        /// 课程预计费用
        /// </summary>
        public  decimal CoursePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 课程状态，字典表
        /// </summary>
        public  int StateID
        {
            get;
            set;
        }
        /// <summary>
        /// 课程状态名称
        /// </summary>
        public  string StateName { get; set; }
        /// <summary>
        /// 授课方式
        /// </summary>
        public  int TypeID { get; set; }
       /// <summary>
       /// 授课方式名称
       /// </summary>
        public  string TypeName { get; set; }

        /// <summary>
        /// 课时数
        /// </summary>
        public  int Hours
        {
            get;
            set;
        }
        /// <summary>
        /// 课程介绍
        /// </summary>
        public  string Introduce
        {
            get;
            set;
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        public string ComCode
        {
            get;
            set;
        }

        /// 校区中文名称
        /// </summary>
        public string COMP_Name
        {
            get;
            set;
        }

        



    }
 
}
