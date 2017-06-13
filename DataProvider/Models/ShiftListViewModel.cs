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
    public class ShiftListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public ShiftListSearchModel search = new ShiftListSearchModel();
        /// <summary>
        /// 页面的列表数据tudents
        /// </summary>
         public PagedList<vw_Classes> Shiftlist { get; set; }

        /// <summary>
        /// 按钮下拉框，演示用
        /// </summary>
        public List<SelectListItem> SourceIL { get; set; } 

    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class ShiftListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///班级名称
        /// </summary>
        public string ClassName { set; get; }


         /// <summary>
        ///课程名称
        /// </summary>
        public string CourseID { set; get; }

        
        /// <summary>
        /// 开班开始时间 
        /// </summary>
        public DateTime? StartTime_start
        { set; get; }
         

       
        /// <summary>
        /// 开班结束时间
        /// </summary>
        public DateTime? StartTime_end
        { set; get; }


        /// <summary>
        /// 结班开始时间 
        /// </summary>
        public DateTime? EndTime_start
        { set; get; }



        /// <summary>
        /// 结班结束时间
        /// </summary>
        public DateTime? EndTime_end
        { set; get; }

        /// <summary>
        /// 下拉框按钮的选中值
        /// </summary>
        public int DicItemID { set; get; }


    }

}



