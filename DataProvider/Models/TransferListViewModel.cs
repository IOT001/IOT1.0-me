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
    public class TransferListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public TransferListSearchModel search = new TransferListSearchModel();
        /// <summary>
        /// 页面的列表数据vw_Transfer视图的
        /// </summary>
        public PagedList<vw_Transfer> Transferlist { get; set; }

        /// <summary>
        ///下拉框  
        /// </summary>
        public List<SelectListItem> SourceIL { get; set; }



        /// <summary>
        ///状态下拉框  
        /// </summary>
        public List<SelectListItem> StateIDIL { get; set; }



        /// <summary>
        ///学生下拉框  
        /// </summary>
        public List<SelectListItem> StudentsIDIL { get; set; }


          /// <summary>
        ///学生下拉框  
        /// </summary>
        public List<SelectListItem> StudentsIDIL_1 { get; set; }
        
    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class TransferListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///甲方
        /// </summary>
        public string JName { set; get; }

        /// <summary>
        ///乙方
        /// </summary>
        public string YName { set; get; }
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
        /// 学生下拉框按钮的选中值
        /// </summary>
        public string JStudentID { set; get; }
        
        /// <summary>
        /// 学生下拉框按钮的选中值,乙方学员id，也是被转至的学员id
        /// </summary>
        public string YStudentID { set; get; }

        
        /// <summary>
        /// 状态下拉框按钮的选中值
        /// </summary>
        public string StateID { set; get; }
    }

}



