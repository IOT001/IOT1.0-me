using System;
using System.Collections.Generic;
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
        public DateTime CreateTime { get; set; }
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
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 角色，表示那些角色有权限下载，用分号分隔
        /// </summary>				
        public string ToRoles { get; set; }
        /// <summary>
        /// 方便查询，存的中文，比如人事,财务，市场
        /// </summary>				
        public string ToRolesName { get; set; }
    }
}
