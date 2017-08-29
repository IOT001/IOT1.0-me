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
    public class EnrollAuditInfoViewModel
    {
       /// <summary>
       /// 页面的搜索条件
       /// </summary>
        public EnrollAuditInfoSearchModel search = new EnrollAuditInfoSearchModel();

       /// <summary>
       /// 页面的列表数据
       /// </summary>
        public PagedList<vw_EnrollAudit> EnrollAuditInfoList { get; set; }
      
    }

    public class EnrollAuditInfoSearchModel : CommonPageEntity
    {

        /// <summary>
        /// 预约单号
        /// </summary>
        public string APID { set; get; }
        /// <summary>
        /// 自定义优惠
        /// </summary>
        public string DiscountID { set; get; }
        /// <summary>
        /// 待审核的记录
        /// </summary>
        public string StateID { set; get; }
      

    }

}
