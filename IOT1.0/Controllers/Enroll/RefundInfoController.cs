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
        // GET: /RefundInfo/

        public ActionResult RefundInfo(string ID, int typeID)
        {
            RefundCheckListModel model = new RefundCheckListModel();//页面模型
            model.search.ID = ID;//页面的搜索模型 
            model.RefundlList = RefundCheckData.GetRefundList(model.search);//填充页面模型数据
            model.search.typeID = typeID;
            return View(model);//返回页面模型
        }



        

    }
}
