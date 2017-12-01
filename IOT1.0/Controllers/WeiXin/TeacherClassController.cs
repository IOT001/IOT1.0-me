using DataProvider;
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
        // GET: /TeacherClass/
        /// <summary>
        /// 教师对应的上课记录
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherClassList(TeacherClassListSearch search)
        {
            WX_TeacherClassListViewModel model = new WX_TeacherClassListViewModel();
            model.search = search;
            model.search.PageSize = 9999;//每页显示1000条数据
            model.search.CurrentPage = model.search.CurrentPage <= 0 ? 1 : model.search.CurrentPage;//获取当前页
            Teachers s = TeacherData.GetTeachByID(UserSessionWX.userid);//获取当前教师
            if (s != null)
            {
                model.TeacherClassList = TeacherClassData.GetAttendanceRecordList(search);
            }
            return View();
        }

    }
}
