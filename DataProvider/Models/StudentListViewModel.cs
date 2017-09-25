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
    public class StudentListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public StudentListSearchModel search = new StudentListSearchModel();
        /// <summary>
        /// 页面的列表数据tudents
        /// </summary>
        public PagedList<vw_Students> Studentlist { get; set; }

        /// <summary>
        ///来源渠道下拉框  
        /// </summary>
        public List<SelectListItem> SourceIL { get; set; }

       
        /// <summary>
        /// 分校下拉框
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; } 

    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class StudentListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///姓名
        /// </summary>
        public string Name { set; get; }

        
        /// <summary>
        /// 创建时间开始
        /// </summary>
        public string CreateTime_start
        { set; get; }
         

       
        /// <summary>
        /// 创建时间结束
        /// </summary>
        public string CreateTime_end
        { set; get; }


        /// <summary>
        /// 下拉框按钮的选中值
        /// </summary>
        public int DicItemID { set; get; }


        /// <summary>
        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; }
        /// <summary>
        /// 所属分校的选中值
        /// </summary>
        public string ComCode { get; set; }

      

    }

}



