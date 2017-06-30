using DataProvider;
using DataProvider.Data;
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
        /// 支付中心
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentList()
        {
            WX_PaymentListModel model = new WX_PaymentListModel();
            EnrollListSearchModel search = new EnrollListSearchModel();
            model.search = search;
            model.search.PageSize = 15;//每页显示15条数据
            model.search.CurrentPage = model.search.CurrentPage <= 0 ? 1 : model.search.CurrentPage;//获取当前页
            Students s =StudentData.GetStudentByAccountID(UserSession.userid);//获取学员
            search.Enroll_StudentID = s.ID;//获取学号
            PagedList<vw_Enroll> vw_Enroll = EnrollData.GeEnrollList(search);//查询报名记录
            model.StudentEnrollList = vw_Enroll;
            return View(model);
        }
    }
}
