using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class vw_ClassListJob
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
 
        public string Size { get; set; } 
 
       /// <summary>
        /// 文件名称
        /// </summary>	
        public string FileName { get; set; }
        /// <summary>
        ///文件
        /// </summary>	
        public string FileTitle { get; set; }

         /// <summary>
        ///作业标题
        /// </summary>	
        public string JobTitle { get; set; }
        /// <summary>
        ///作业内容
        /// </summary>	
        public string JobContent { get; set; }
      

        public string FileRoute
        {
            
            get
            {
                string route = ConfigurationManager.AppSettings["ClassJobPath"].ToString() + FileName;
                return route;
            }
            set { FileRoute = value; }
       
        }

    }

 
}
