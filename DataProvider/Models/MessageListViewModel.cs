using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DataProvider.Models
{
    public class MessageListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public MessageListSearchModel search = new MessageListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<Message> Messagelist { get; set; }
 
          /// <summary>
        /// 按钮下拉框，演示用
        /// </summary>
        public List<SelectListItem> SYS_SystemRoleIL { get; set; } 
        
    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class MessageListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///留言标题
        /// </summary>
        public string Title { set; get; }

       

    }

}



