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
    public class EnrollAuditListViewModel
    {
       /// <summary>
       /// 页面的搜索条件
       /// </summary>
        public EnrollAuditListSearchModel search = new EnrollAuditListSearchModel();

       /// <summary>
       /// 页面的列表数据
       /// </summary>
       public PagedList<vw_Appointment> EnrollAuditList { get; set; }
      
    }

    public class EnrollAuditListSearchModel : CommonPageEntity
    {

        /// <summary>
        /// 状态ID
        /// </summary>
        public string ApStateID { set; get; }
        /// <summary>
        ///  开始时间
        /// </summary>
        public string timeStart { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string timeEnd { set; get; }

    }

}
