using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
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
        // GET: /Weixin/


 
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCenter()
        {
            WX_UserCenterViewModel model = new WX_UserCenterViewModel();
            string UserRoleNames = "";
            if (UserSessionWX.roles != null)
            {
                foreach (string roleid in UserSessionWX.roles)
                {
                    UserRoleNames += SYSRoleData.GetRoleByid(int.Parse(roleid)).ROLE_Name + ",";
                }
            }
            model.UserRoleName = UserRoleNames.TrimEnd(',');
            return View(model);
        }


    }
}
