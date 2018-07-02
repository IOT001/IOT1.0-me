using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IOT1._0.Models;


namespace IOT1._0.Controllers.Teach
{
    public partial class TeachController : Controller
    {
 
        
        /// 列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult TeachAttendanceOriginal(TeachAttendanceOriginalListSearchModel search)
        {
            search.ComCode = UserSession.comcode;
            TeachAttendanceOriginalListViewModel model = new TeachAttendanceOriginalListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
             
            model.AttendanceOriginallist = TeachAttendanceOriginalData.GetAttendanceOriginalDataList(search);//填充页面模型数据
            return View(model);//返回页面模型


        }



    }
}
