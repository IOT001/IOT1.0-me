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
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public TeacherSearchModel search = new TeacherSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<Teachers> buttonlist { get; set; }
        /// <summary>
        /// 按钮下拉框，演示用
        /// </summary>
        public List<SelectListItem> buttonIL { get; set; } 
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
        public int DicItemID { set; get; }
    }
}
