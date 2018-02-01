﻿using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOT1._0.Models
{
    public class UserSessionWX
    {
        public static string username
        {
            set
            {
                HttpContext.Current.Session["username"] = value;
            }
            get
            {
                if (HttpContext.Current.Session["username"] == null || HttpContext.Current.Session["username"].ToString().Trim() == "")
                {
                    HttpContext.Current.Response.Redirect("~/login.html", true);
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    return HttpContext.Current.Session["username"].ToString();
                }
            }
        }

        public static string userid
        {
            set
            {
               
                HttpContext.Current.Session["userid"] = value;
            }
            get
            {
                if (HttpContext.Current.Session["userid"] == null || HttpContext.Current.Session["userid"].ToString().Trim() == "")
                {
                    HttpContext.Current.Response.Redirect("~/Weixin/login", true);
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    return  HttpContext.Current.Session["userid"].ToString();
                }
            }
        }
        /// <summary>
        /// 账号所属分校
        /// </summary>
        public static string comcode
        {
            set
            {

                HttpContext.Current.Session["comcode"] = value;
            }
            get
            {
                if (HttpContext.Current.Session["comcode"] == null || HttpContext.Current.Session["userid"].ToString().Trim() == "")
                {
                    HttpContext.Current.Response.Redirect("~/login.html", true);
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    return HttpContext.Current.Session["comcode"].ToString();
                }
            }
        }

        public static string UserLoginTime
        {
            set
            {

                HttpContext.Current.Session["UserLoginTime"] = value;
            }
            get
            {
                if (HttpContext.Current.Session["UserLoginTime"] == null || HttpContext.Current.Session["UserLoginTime"].ToString().Trim() == "")
                {
                    HttpContext.Current.Response.Redirect("~/login.html", true);
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    return HttpContext.Current.Session["UserLoginTime"].ToString();
                }
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public static string ValidateCode
        {
            set
            {
                HttpContext.Current.Session["ValidateCode"] = value;
            }
            get
            {
                return HttpContext.Current.Session["ValidateCode"].ToString();
            }
        }
        /// <summary>
        /// roleid，逗号隔开
        /// </summary>
        public static string roleids
        {
            set
            {
                HttpContext.Current.Session["roleids"] = value;
            }
            get
            {
                return HttpContext.Current.Session["roleids"].ToString();
            }
        }
        public static List<string> roles
        {
            get {
                string[] strArray = roleids.Split(','); //字符串转数组
                return new List<string>(strArray);
            }
        }

        //是否成功登陆
        public static bool islogin
        {

            get
            {
                if (HttpContext.Current.Session["userid"] == null || HttpContext.Current.Session["userid"].ToString().Trim() == "")
                {
                    return false;
                }
                else
                {
                      return true;
                }
              

            }
        }
    }
}