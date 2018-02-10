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
    public class DictionaryItemViewModel
    {
         
        
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public DictionaryItemSearchModel search = new DictionaryItemSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<DictionaryItem>  DictionaryItemlist { get; set; }
        /// <summary>
        /// <summary>
        ///  时间段下拉框
        /// </summary>
        public List<SelectListItem> DicItemNameIL { get; set; }

    }
    public class DictionaryItemSearchModel : CommonPageEntity
    {
        ///<summary>
        ///时间段 
        ///</summary>
        public string DicItemName { set; get; }

        /// <summary>
        /// 下拉框的选中值
        /// </summary>
        public string DicItemID { set; get; }

        
    }
}
