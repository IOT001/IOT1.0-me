using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataProvider.Models
{
   public class EnrollManageListModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
       public EnrollListSearchModel search = new EnrollListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_Enroll> EnrollManagelist { get; set; }


        /// <summary>
        /// 分校下拉框
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; }

        /// <summary>
        /// 存储当前用户分校值,来判断前端页面的绑定条件
        /// </summary>
        public string TeacherComCode { get; set; } 

    }


      

}
