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
    public class ReimbursetListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public ReimbursetListSearchModel search = new ReimbursetListSearchModel();
        /// <summary>
        /// 页面的列表数据tudents
        /// </summary>
        public PagedList<vw_Reimburse> Reimbursetlist { get; set; }

        /// <summary>
        ///状态下拉框  
        /// </summary>
        public List<SelectListItem> StateIDIL { get; set; }


        /// <summary>
        ///老师下拉框  
        /// </summary>
        public List<SelectListItem> TeacherIDIL { get; set; }



    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class ReimbursetListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///老师姓名
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
        /// 状态下拉框按钮的选中值
        /// </summary>
        public string StateID { set; get; }

        /// <summary>
        /// 状态下拉框按钮的选中值
        /// </summary>
        public string TeacherID { set; get; }

    }

}



