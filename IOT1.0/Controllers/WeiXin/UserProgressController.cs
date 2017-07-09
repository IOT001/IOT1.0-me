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
 


        /// <summary>
        /// 查询UserProgress页面详细记录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserProgress()
        {
            WX_UserProgressListModel model = new WX_UserProgressListModel();
            vw_UserProgressSearchModel search = new vw_UserProgressSearchModel();
            model.search = search;
            model.search.PageSize = 15;//每页显示15条数据
            model.search.CurrentPage = model.search.CurrentPage <= 0 ? 1 : model.search.CurrentPage;//获取当前页
            Students s = StudentData.GetStudentByAccountID(UserSession.userid);//获取学员
            search.StudentID = s.ID;//获取学号
            PagedList<vw_UserProgress> vw_UserProgress = UserProgressListData.Getvw_UserProgressList(search);//查询学员课程表
            model.UserProgressList = vw_UserProgress;
            return View(model);
        }
    }
}
