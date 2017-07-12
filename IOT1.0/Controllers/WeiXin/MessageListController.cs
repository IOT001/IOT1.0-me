using DataProvider.Data;
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
        // GET: /MessageList/
        /// <summary>
        /// 微信端返回留言信息
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageBrowse()
        {
            WX_MessageBrowseModel model = new WX_MessageBrowseModel();
            List<string> roles = UserSession.roles;
            model.MessageList = MessageBrowseData.GetMessageList(roles);

            return View();
        }

    }
}
