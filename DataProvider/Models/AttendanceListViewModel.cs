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
  


    public class AttendanceListViewModel
    {
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public AttendanceSearchModel search = new AttendanceSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<vw_ClassAttendanceList> buttonlist { get; set; }

        
    }
    public class AttendanceSearchModel : CommonPageEntity
    {
        ///<summary>
        ///班级名称 
        ///</summary>
        public string className { set; get; }
        /// <summary>
        /// 上课时间-开始时间
        /// </summary>
        public string timeStart { set; get; }
        /// <summary>
        /// 上课时间-结束时间
        /// </summary>
        public string timeEnd { set; get; }
        /// <summary>
        /// 课程IDW
        /// </summary>
        public string classId { set; get; }
        /// <summary>
        /// 课程索引
        /// </summary>
        public string classIndex { set; get; }
      
 
    }
}
