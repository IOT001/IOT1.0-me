using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
   public class WX_TeacherClassListViewModel
    {
        /// <summary>
        /// 教师对应的班级记录
        /// </summary>
       public PagedList<vw_ClassAttendanceList> TeacherClassList { get; set; }
       /// <summary>
       /// 页面的搜索条件
       /// </summary>
       public TeacherClassListSearch search = new TeacherClassListSearch();
    }

   public class TeacherClassListSearch : CommonPageEntity
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
       /// 教师ID
       /// </summary>
       public string teacherID { set; get; }
   }
}
