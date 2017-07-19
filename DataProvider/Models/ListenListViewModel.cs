using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
   public class ListenListViewModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
       public EnrollListSearchModel search = new EnrollListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_Enroll> ListenList { get; set; }
    }

}
