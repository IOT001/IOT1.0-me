using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class ClassListJob
    {
        /// <summary>
        /// ID
        /// </summary>			
        public int ID { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>			
        public string Classid { get; set; }
        /// <summary>
        /// 课程表行号
        /// </summary>			
        public int Classindex { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>		
        public string ContentType { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>	
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>	
        public Nullable<DateTime> CreateTime { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>	
<<<<<<< HEAD
        public string Size { get; set; }
=======
        public int Size { get; set; }
>>>>>>> 29b8f778205d047c73f822a825197896aac55be6
 
       /// <summary>
        /// 文件名称
        /// </summary>	
        public string FileName { get; set; }
        /// <summary>
        ///文件
        /// </summary>	
        public string FileTitle { get; set; }


        public string FileRoute
        {
            get
            {
                string route = ConfigurationManager.AppSettings["ClassJobPath"].ToString() + FileName;
                return route;
            }
            set;
        }

    }



    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class ClassListJobORMMapper : ClassMapper<ClassListJob>
    {
        public ClassListJobORMMapper()
        {
            base.Table("ClassListJob");

            //Map(f => f.IsJoin).Ignore();//设置忽略 
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
