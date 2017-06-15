using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataProvider.Data;

namespace IOT1._0.Controllers.Attendance
{
    public class AttendanceController : Controller
    {
        //
        // GET: /AttendanceList/

        public ActionResult AttendanceList(AttendanceSearchModel search)
        {
            AttendanceListViewModel model = new AttendanceListViewModel();
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            model.buttonlist = AttendaceData.GetButtonList(search);
            return View(model);
        }

    }
}
