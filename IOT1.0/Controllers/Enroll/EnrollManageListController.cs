using DataProvider.Data;
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
        // GET: /EnrollManageList/

        public ActionResult EnrollManageList(EnrollListSearchModel search)
        {
            EnrollManageListModel model = new EnrollManageListModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

            model.EnrollManagelist = EnrollData.GeEnrollList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

    }
}
