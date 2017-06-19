using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Enroll
{
    public class EnrollController : Controller
    {
        /// <summary>
        /// 报名记录
        /// </summary>
        /// <returns></returns>
        public ActionResult EnrollList(EnrollListSearchModel search )
        {
            EnrollListViewModel model = new EnrollListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //根据方式拉项
            List<CommonEntity> FollowTypeList = CommonData.GetDictionaryList(13);//13跟进方式
            model.FollowTypeIL = CommonData.Instance.GetBropDownListData(FollowTypeList);

            //根据方式拉项
            List<CommonEntity> IntentTypeList = CommonData.GetDictionaryList(14);//13跟进方式
            model.IntentTypeIL = CommonData.Instance.GetBropDownListData(IntentTypeList);

            model.AppointmentList = AppointmentData.GetAPList(search); //填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 保存编辑预约单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveAppointment()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Appointment obj = (Appointment)(JsonConvert.DeserializeObject(data.ToString(), typeof(Appointment)));//序列化成对象
            obj.UpdateTime = DateTime.Now;
            obj.UpdatorId = UserSession.userid;
            if (AppointmentData.Update(obj))//注意时间类型，而且需要在前台把所有的值，也能在后台复制
            {
                ajax.msg = "保存成功！";//前台会安装这个信息弹出信息
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 新增预约单
        /// </summary>
        /// <returns></returns>
        public JsonResult AddAppointment()
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
            obj.CreatorId = UserSession.userid;
            if (AppointmentData.Add(obj))//注意时间类型
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 根据预约号返回跟进记录
        /// </summary>
        /// <param name="apid"></param>
        /// <returns></returns>
        public JsonResult GetFollowListByAPID(string apid)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            List<FollowRecord> FollowList = AppointmentData.GetFollowListByAPID(apid);
            ajax.data = FollowList;
            return Json(new { total = 1, rows = FollowList, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增跟进记录
        /// </summary>
        /// <returns></returns>
        public JsonResult AddFollow()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            FollowRecord obj = (FollowRecord)(JsonConvert.DeserializeObject(data.ToString(), typeof(FollowRecord)));
            obj.FollowTime = DateTime.Now;
            obj.FollowPersonID = UserSession.userid;
            if (AppointmentData.AddFollow(obj))//注意时间类型
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 回访记录
        /// </summary>
        /// <returns></returns>
        public ActionResult FollowList()
        {
            return View();
        }
        /// <summary>
        /// 票据打印
        /// </summary>
        /// <returns></returns>
        public ActionResult InvoicePrint()
        {
            return View();
        }

        /// <summary>
        ///  按条件查询试听课
        /// </summary>
        /// <returns></returns>
        public ActionResult STClassList()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            ClassesListSearchModel search = (ClassesListSearchModel)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassesListSearchModel)));//序列化吃查询模型
            search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            search.PageSize = 99999;//不想分页就设置成一个较大的值
            List<vw_Classes> vw_Classes = ClassesData.GeClassesList(search);
            ajax.data = vw_Classes;
            return Json(new { total = 1, rows = vw_Classes, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }

    }
}
