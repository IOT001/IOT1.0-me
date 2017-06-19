using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    //Course
    public class Course
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

    }
    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class CourseORMMapper : ClassMapper<Course>
    {
        public CourseORMMapper()
        {
            base.Table("Course");

            //Map(f => f.IsJoin).Ignore();//设置忽略
            Map(f => f.StateName).Ignore();//设置忽略
            Map(f => f.TypeName).Ignore();//设置忽略
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
