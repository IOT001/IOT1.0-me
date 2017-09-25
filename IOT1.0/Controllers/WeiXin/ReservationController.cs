using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
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
        // GET: /Reservation/

        public ActionResult Reservation()
        {
            ReservationModel re = new ReservationModel();
            re.coureselist = CourseData.GetCourseIL();
            return View(re);
        }
        /// <summary>
        /// 新增预约单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAppointment()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Appointment obj = (Appointment)(JsonConvert.DeserializeObject(data.ToString(), typeof(Appointment)));
            obj.ID = CommonData.DPGetTableMaxId("AP", "ID", "Appointment", 8);
            obj.CreateTime = DateTime.Now;
            obj.CreatorId = UserSessionWX.userid;
            obj.ApStateID = 1;//默认未跟进
            if (string.IsNullOrEmpty(obj.ComCode))
            {

               obj.ComCode = UserSessionWX.comcode;
            }
            if (AppointmentData.Add(obj))//注意时间类型
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
    }
}
