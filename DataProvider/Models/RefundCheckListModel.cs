using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class RefundCheckListModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public RefundCheckListSearchModel search = new RefundCheckListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_Refund> RefundlList { get; set; }
    }
     

    public class RefundCheckListSearchModel : CommonPageEntity
    
    {
       
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string SutdentName { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string BindPhone { set; get; }
 
        /// <summary>
        /// 报名日期-开始时间
        /// </summary>
        public string timeStart { set; get; }
        /// <summary>
        /// 报名日期-结束时间
        /// </summary>
        public string timeEnd { set; get; }

        /// <summary>
        /// 默认审核状态
        /// </summary>
        public string StateID { set; get; }
        
    }

}
