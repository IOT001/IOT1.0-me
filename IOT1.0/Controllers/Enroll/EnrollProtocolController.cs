using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {
        //
        // GET: /EnrollProtocol/

        public ActionResult EnrollProtocol(string id)
        {
            EnrollProtocolViewModel model = new EnrollProtocolViewModel();
            string apid = id;
       
            SignImage si = CommonData.GetStringKeySign(apid, 0);
            SignImage si_jbr = CommonData.GetStringKeySign(apid, 1);
            model.apid = apid;
            model.si = si;
            model.si_jbr = si_jbr;
            model.EnrollList = EnrollData.GetEnrollPrintByApid(apid);
            model.ap = AppointmentData.GetOnevw_AppointmentByID(apid);
            model.bill = EnrollData.GetOneBillConfig();
            return View(model);
        }

    }
}
