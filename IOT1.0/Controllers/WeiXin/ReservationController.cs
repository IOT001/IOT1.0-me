using DataProvider.Data;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.WeiXin
{
    public partial class WeixinController : Controller
    {
        //
        // GET: /Reservation/

        public ActionResult Reservation()
        {
            ReservationModel re = new ReservationModel();
            re.coureselist = CourseData.GetCourseIL();
            return View(re);
        }

    }
}
