using DataProvider;
using DataProvider.Data;
using DataProvider.Models;
using DataProvider.Paging;
using IOT1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.WeiXin
{
    public partial class WeixinController : Controller
    {


        /// <summary>
        /// 查询上课记录
        /// </summary>
        /// <returns></returns>
        public ActionResult FSConnectionlist(StudentScheduleSearchModel search)
        {
            WX_FSConnectionlistModel model = new WX_FSConnectionlistModel(); 
            model.search = search;
            model.search.PageSize = 15;//每页显示15条数据
            model.search.CurrentPage = model.search.CurrentPage <= 0 ? 1 : model.search.CurrentPage;//获取当前页
            Students s =StudentData.GetStudentByAccountID(UserSessionWX.userid);//获取学员





            DateTime dt = DateTime.Now;  //当前时间  

            //本周,以星期天为第一天
            //DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一  
            //DateTime endWeek = startWeek.AddDays(6);  //本周周日  


            int weeknow = Convert.ToInt32(dt.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            DateTime startWeek = dt.AddDays(daydiff);

            //星期天为最后一天  
            int weeknow1 = Convert.ToInt32(dt.DayOfWeek);
            weeknow1 = (weeknow1 == 0 ? 7 : weeknow1);
            int daydiff1 = (7 - weeknow1);

            //本周最后一天  
            DateTime endWeek = dt.AddDays(daydiff1);
             

            if (search.timeStart == null)
            {
                string timeStart = startWeek.ToString("yyyy-MM-dd");
                search.timeStart = timeStart += " 00:00:00:000";
                string timeEnd = endWeek.ToString("yyyy-MM-dd");
                search.timeEnd = timeEnd += " 23:59:59.999";
            }
            else
            {
                //上课时间-开始时间 
                search.timeStart = search.timeStart += " 00:00:00:000";//对日期做特殊处理,取第一个星期的最小值

                // 上课时间-结束时间 
                search.timeEnd = search.timeEnd += " 23:59:59.999";//对日期做特殊处理,取最后一个星期的最大值


            }
      


            if (s != null)
            {
                search.AttendanceRecord_StudentID = s.ID;//获取学号
            }
      

            PagedList<vw_AttendanceRecord> vw_AttendanceRecord = StudentScheduleListData.GetAttendanceRecordList(search);//查询学员课程表
            model.FSConnectionlist = vw_AttendanceRecord;
            return View(model);
        }
    }
}
