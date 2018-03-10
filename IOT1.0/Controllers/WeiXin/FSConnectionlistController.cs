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
            if (s != null)
            {
                search.AttendanceRecord_StudentID = s.ID;//获取学号
            }
            //开始时间 
            search.timeStart = search.timeStart += " 00:00:00:000";//对日期做特殊处理,取第一个星期的最小值

            // 结束时间 
            search.timeEnd = search.timeEnd += " 23:59:59.999";//对日期做特殊处理,取最后一个星期的最大值

            PagedList<vw_AttendanceRecord> vw_AttendanceRecord = StudentScheduleListData.GetAttendanceRecordList(search);//查询学员课程表
            model.FSConnectionlist = vw_AttendanceRecord;
            return View(model);
        }
    }
}
