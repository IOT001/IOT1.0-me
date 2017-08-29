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
        // GET: /EnrollAudit/

        public ActionResult EnrollAuditList()
        {

            EnrollAuditListViewModel model = new EnrollAuditListViewModel();
            EnrollAuditListSearchModel search = new EnrollAuditListSearchModel();

            search.ApStateID = "5";//因为只查待审核的
            model.EnrollAuditList = EnrollAuditListData.GetEnrollAuditLis(search);//业务层获取底层方法，返回数据

            return View(model);

        }
 
    }
}
