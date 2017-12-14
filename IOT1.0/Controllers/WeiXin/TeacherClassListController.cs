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

        //GET: /TeacherClass/
        //<summary>
        //教师对应的上课记录
        //</summary>
        //<returns></returns>
        public ActionResult TeacherClassList(TeacherClassListSearch search)
        {
            WX_TeacherClassListViewModel model = new WX_TeacherClassListViewModel();
            model.search = search;
            model.search.PageSize = 15;
            model.search.CurrentPage = Convert.ToInt32(Request["CurrentPage"]) <= 0 ? 1 : Convert.ToInt32(Request["CurrentPage"]);//获取当前页

            search.PageSize = model.search.PageSize * model.search.CurrentPage;//微信端显示数量
            search.CurrentPage = 1;

            Teachers s = TeacherData.GetTeachByID(UserSessionWX.userid);//获取当前教师
            if (s != null)
            {
                model.search.teacherID = s.ID;
                model.TeacherClassList = TeacherClassData.GetAttendanceRecordList(search);
                model.search.TotalPageCount = model.TeacherClassList.TotalPageCount;
            }
            return View(model);
        }

 



    }


}
