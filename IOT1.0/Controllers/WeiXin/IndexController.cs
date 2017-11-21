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
            if (string.IsNullOrEmpty(UserSessionWX.userid))
            {
                return Redirect(Url.Action("login", "Weixin")); //没有session就跳转到登录界面重新登录
            }
            WXIndexViewModel model = new WXIndexViewModel();
            EnrollListSearchModel search = new EnrollListSearchModel();
            List<vw_Menu> menu = MenuData.GetMenuList(UserSessionWX.userid);//获取微信版菜单
            int AuditNum = EnrollAuditListData.GetEnrollAuditListCount();
            foreach (vw_Menu obj in menu)
            {
                if (obj.ID == 99 && AuditNum > 0)//如果是优惠审核
                {
                    obj.badgeNum = AuditNum.ToString();
                }
            }
            model.MenuList = menu;
            return View(model);
        }

    }
}
