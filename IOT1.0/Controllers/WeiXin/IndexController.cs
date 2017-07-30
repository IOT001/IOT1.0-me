using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
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
        //
        // GET: /Index/

        public ActionResult Index()
        {

            WXIndexViewModel model = new WXIndexViewModel();
            EnrollListSearchModel search = new EnrollListSearchModel();
            List<vw_Menu> menu = MenuData.GetMenuList();//获取微信版菜单
            model.MenuList = menu;
            return View(model);
        }

    }
}
