using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
   public class WX_TeacherClassInfoViewModel
    {
       /// <summary>
       /// 对应班级下的学员考勤记录
       /// </summary>
       public List<vw_AttendanceRecord> arlist { set; get; }
       /// <summary>
       /// 班级信息实体
       /// </summary>
       public vw_ClassAttendanceList acl { set; get; }
    }
}
