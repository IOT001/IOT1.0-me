using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
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
        // GET: /RefundInfo/

        public ActionResult RefundInfo_Print(int ID)
        {
            RefundInfoModel model = new RefundInfoModel();//页面模型 
             
            model.RefundList = RefundInfoData.Getvw_RefundByID(ID);//填充退款数据 
            return View(model);//返回页面模型
        }


         
        

    }
}
