﻿using DataProvider;
using DataProvider.Data;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {


       

        /// <summary>
        /// 回访记录
        /// </summary>
        public ActionResult FollowList(FollowListSearchModel search)
        {
            FollowListListViewModel model = new FollowListListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

            model.FollowList = FollowListData.GetFollowList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

     


    }
}