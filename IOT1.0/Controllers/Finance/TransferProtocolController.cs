using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Finance
{
    public partial class FinanceController : Controller
    {
        //
        // GET: /TransferProtocol/

        public ActionResult TransferProtocol(int id)
        {
            TransferProtocolViewModel model = new TransferProtocolViewModel();
            int ID = id; 
            int typeid = int.Parse(Request["typeid"].ToString());//获取类型，1入学协议，2转让协议
            vw_Transfer si = TransferData.GetIntKeySign(ID, typeid);
            model.ID = ID;
            model.si = si;
            return View(model);
        }

    }
}
