using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class WX_UserProgressListModel
    {
       /// <summary>
        ///vw_UserProgress 学员课程记录
       /// </summary>
        public PagedList<vw_UserProgress> UserProgressList { get; set; }
        /// <summary>
        /// 页面的搜索条件
        /// </summary>
        public vw_UserProgressSearchModel search = new vw_UserProgressSearchModel();

     
    }

    public class vw_UserProgressSearchModel : CommonPageEntity
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentID { set; get; }
    }

}
