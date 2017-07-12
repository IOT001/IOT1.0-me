using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    public class vw_Message
    {
        /// <summary>
        /// 留言ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// 留言标题
        /// </summary>				
        public string Title { get; set; }
        /// <summary>
        /// 有权限的角色
        /// </summary>				
        public string ToRoles { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>				
        public string ToRolesName { get; set; }
        /// <summary>
        /// 父级留言，回复的时候用到
        /// </summary>				
        public int ParentID { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>				
        public string Messageing { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>				
        public string CreateMan { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        List<string> picurls { get; set; }
    }
}
