using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
   public class WX_MessageBrowseModel
    {
       /// <summary>
       /// 留言列表
       /// </summary>
      public List<vw_Message> MessageList { set; get; }
    }
}
