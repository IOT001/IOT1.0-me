using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class WX_ClasshourDetailedlistModel
    {
       /// <summary>
       /// 学员上课记录明细
       /// </summary>
        public PagedList<vw_AttendanceRecord> ClasshourDetailedlist { get; set; }

 
         
    }
    public class ClasshourDetailedSearchModel : CommonPageEntity
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentID { set; get; }
        /// <summary>
        /// 班级
        /// </summary>
        public string ClassID { set; get; }
    }
}
