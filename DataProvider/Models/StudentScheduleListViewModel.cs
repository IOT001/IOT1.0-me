using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using DataProvider.Paging;
using System.Web.Mvc;

namespace DataProvider.Models
{



    public class StudentScheduleListViewModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public StudentScheduleSearchModel search = new StudentScheduleSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_AttendanceRecord> AttendanceRecordlist { get; set; }
        /// <summary>
        ///下拉框  
        /// </summary>
        public List<SelectListItem> AttendanceRecordIL { get; set; } 

        
    }
    public class StudentScheduleSearchModel : CommonPageEntity
    {
        
        /// <summary>
        /// 上课时间-开始时间
        /// </summary>
        public string timeStart { set; get; }
        /// <summary>
        /// 上课时间-结束时间
        /// </summary>
        public string timeEnd { set; get; }

         /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 下拉框绑定班级ID
        /// </summary>
        public string ID { set; get; }
        
 
    }
}
