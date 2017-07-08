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


        //public ActionResult FSConnectionInfo()
        //{
        //    return View();
        //}


        /// <summary>
        /// 查询FSConnectionInfo页面详细记录
        /// </summary>
        /// <returns></returns>
        public ActionResult FSConnectionInfo(int ID)
        {
            WX_FSConnectionlistModel model = new WX_FSConnectionlistModel();
            vw_AttendanceRecord vw_AttendanceRecord = StudentScheduleListData.Getvw_AttendanceRecordByID(ID);//查询学员课程表
            model.FSConnectionInfo = vw_AttendanceRecord;
            return View(model);
        }
    }
}
