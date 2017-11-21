using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
    public class vw_Menu
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string FT_Desc { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public bool IsSuspended { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 父级菜单id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string IconPic { get; set; }
        /// <summary>
        /// 菜单块的css类
        /// </summary>
        public string FT_Class { get; set; }
        /// <summary>
        /// 菜单的相对URL
        /// </summary>
        public string logurl { get; set; }
        private IList<vw_Menu> childs;

        /// <summary>
        /// 子级
        /// </summary>
        public IList<vw_Menu> Childs
        {
            get
            {
                if (childs == null)
                    childs = new List<vw_Menu>();
                return childs;
            }
            set { childs = value; }
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string ACC_Account { get; set; }
        /// <summary>
        /// 标记数
        /// </summary>
        public string badgeNum { get; set; }
    }
}
