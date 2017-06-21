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
    public class DiscountListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public DiscountListSearchModel search = new DiscountListSearchModel();
        /// <summary>
        /// 页面的列表数据tudents
        /// </summary>
        public PagedList<vw_Discount> Discountlist { get; set; }

        /// <summary>
        ///  下拉框 
        /// </summary>
        public List<SelectListItem> SourceIL { get; set; } 

    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class DiscountListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///姓名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        ///姓名
        /// </summary>
        public int DiscountTypeID { set; get; }
 

    }

}



