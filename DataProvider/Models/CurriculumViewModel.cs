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
    public class CurriculumViewModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public CurriculumSearchModel search = new CurriculumSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_Course> buttonlist { get; set; }


        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; }

    }
    public class CurriculumSearchModel : CommonPageEntity
    {
        ///<summary>
        ///课程名称 
        ///</summary>
        public string CourseName { set; get; }


        /// <summary>
        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL1 { get; set; }

        /// <summary>
        /// 所属分校的选中值
        /// </summary>
        public string ComCode { get; set; }

    }
}
