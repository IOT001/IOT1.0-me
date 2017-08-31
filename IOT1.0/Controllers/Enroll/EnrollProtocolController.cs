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
            int typeid = int.Parse(Request["typeid"].ToString());//获取类型，1入学协议，2转让协议
            SignImage si = CommonData.GetStringKeySign(apid, typeid);
            model.apid = apid;
            model.si = si;
            return View(model);
        }

    }
}
