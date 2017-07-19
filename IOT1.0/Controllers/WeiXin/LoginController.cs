using DataProvider;
using DataProvider.SqlServer;
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
        // GET: /Login/
        public ActionResult login(string openid)
        {
            return View(); ;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public JsonResult login1()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.msg = "操作失败，请稍后尝试！";

            try
            {
                string userName = Request["userName"];
                string password = Request["password"];
                var aa = Request["param"];
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))//用户名或者密码为空
                {
                    ajax.msg = "请填写用户名和密码！";
                }

                var dt = DPAuthority.Login(userName, password);//执行查询

                if (dt.Count == 0)//用户或者密码不正确
                {
                    ajax.msg = "用户名或密码错误！";
                    return Json(ajax);
                }

                foreach (var d in dt)//设置session
                {
                    UserSession.userid = d.ACC_Account;
                    UserSession.username = d.ACC_Account;
                    UserSession.UserLoginTime = DateTime.Now.ToString();
                }
                ajax.msg = "登录成功！";
                ajax.status = EnumAjaxStatus.Success;
                ajax.data = Url.Action("Index", "Weixin");
                return Json(ajax);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

    }
}
