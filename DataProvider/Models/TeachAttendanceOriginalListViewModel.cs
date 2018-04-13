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
    public class TeachAttendanceOriginalListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public TeachAttendanceOriginalListSearchModel search = new TeachAttendanceOriginalListSearchModel();
        /// <summary>
        /// 页面的列表数据AttendanceOriginal
        /// </summary>
        public PagedList<vw_AttendanceOriginal> AttendanceOriginallist { get; set; }

  

    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class TeachAttendanceOriginalListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///姓名
        /// </summary>
        public string username { set; get; }

    


        
        /// <summary>
        ///  导入时间开始
        /// </summary>
        public string InputDate_start
        { set; get; }



        /// <summary>
        /// 导入时间结束
        /// </summary>
        public string InputDate_end
        { set; get; }

         
    }

}



