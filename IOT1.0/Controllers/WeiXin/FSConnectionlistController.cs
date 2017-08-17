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
            Students s =StudentData.GetStudentByAccountID(UserSession.userid);//获取学员
            search.AttendanceRecord_StudentID = s.ID;//获取学号

            //if (search.timeStart == null && search.timeEnd == null)
            //{
            
            //DateTime datetime = DateTime.Now;
            //int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            ////因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            //weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            //int daydiff = (-1) * weeknow;

            ////本周第一天  
            //string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            //DateTime timeStart = Convert.ToDateTime(FirstDay);

            ////星期天为最后一天  
            //int weeknow_sum = Convert.ToInt32(datetime.DayOfWeek);
            //weeknow_sum = (weeknow_sum == 0 ? 7 : weeknow_sum);
            //int daydiff_sum = (7 - weeknow_sum);

            ////本周最后一天  
            //string LastDay = datetime.AddDays(daydiff_sum).ToString("yyyy-MM-dd");
            //DateTime timeEnd = Convert.ToDateTime(LastDay);  


            //search.timeStart = timeStart.ToString();//开始时间
            //search.timeEnd = timeEnd.ToString();//结束时间

            //}

            PagedList<vw_AttendanceRecord> vw_AttendanceRecord = StudentScheduleListData.GetAttendanceRecordList(search);//查询学员课程表
            model.FSConnectionlist = vw_AttendanceRecord;
            return View(model);
        }
    }
}
