using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public partial class Files
    {
        /// <summary>
        /// 文件管理主键
        /// </summary>				
        public int id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>				
        public string FileTitle { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>				
        public string FileName { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>				
        public string Remark { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public Nullable<System.DateTime> CreateTime { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// DeletorId
        /// </summary>				
        public string DeletorId { get; set; }
        /// <summary>
        /// DeleteTime
        /// </summary>				
        public Nullable<System.DateTime> DeleteTime { get; set; }
        /// <summary>
        /// 角色，表示那些角色有权限下载，用分号分隔
        /// </summary>				
        public string ToRoles { get; set; }
        /// <summary>
        /// 方便查询，存的中文，比如人事,财务，市场
        /// </summary>				
        public string ToRolesName { get; set; }



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


   /// <summary>
   /// Deploy：实体对象映射关系
   /// </summary>
   [Serializable]
   public sealed class FilesORMMapper : ClassMapper<Files>
   {
       public FilesORMMapper()
       {
           base.Table("Files");

           Map(f => f.id).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)  
           Map(f => f.FileRoute).Ignore();//设置忽略 
           AutoMap();
       }
   }


}
