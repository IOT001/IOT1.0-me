using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using DataProvider.Paging;
using System.Web.Mvc;

namespace DataProvider.Models
{



    public class FollowListListViewModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public FollowListSearchModel search = new FollowListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_FollowList> FollowList { get; set; }

        
    }
    public class FollowListSearchModel : CommonPageEntity
    {
        /// <summary>
        /// 学生名称
        /// </summary>
        public string Name { set; get; }
        ///<summary>
        ///联系电话 
        ///</summary>
        public string BindPhone { set; get; }
        /// <summary>
        /// 报名日期-开始时间
        /// </summary>
        public string timeStart { set; get; }
        /// <summary>
        /// 报名日期-结束时间
        /// </summary>
        public string timeEnd { set; get; }
     
       
 
    }
}
