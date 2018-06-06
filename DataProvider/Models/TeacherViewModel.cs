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
    public class TeacherViewModel
    {

        public long[] School { get; set; } //多选框

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public TeacherSearchModel search = new TeacherSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_Teachers> Teacherslist { get; set; }
        /// <summary>
        /// 按钮下拉框，演示用
        /// </summary>
        public List<SelectListItem> buttonIL { get; set; }

        /// <summary>
        /// 分校下拉框
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; } 

  

    }
    public class TeacherSearchModel : CommonPageEntity
    {
        ///<summary>
        ///教师名称 
        ///</summary>
        public string TeacherName { set; get; }
        /// <summary>
        /// 下拉框按钮的选中值
        /// </summary>
        public int LeaveDate { set; get; }
         /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { set; get; }

        /// <summary>
        /// 分校下拉框的选中值
        /// </summary>
        public string ComCode { set; get; }


        /// <summary>
        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL1 { get; set; }

         


        
    }
}
