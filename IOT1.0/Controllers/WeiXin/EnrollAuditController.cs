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


        public ActionResult EnrollAuditInfo(string APID)
        {

            EnrollAuditInfoViewModel model = new EnrollAuditInfoViewModel();
            EnrollAuditInfoSearchModel search = new EnrollAuditInfoSearchModel();// 页面的搜索条件

            search.StateID = "2";
            search.DiscountID = "-1";
            search.APID = APID;//根据预约单号
            model.EnrollAuditInfoList = EnrollAuditInfoData.GetEnrollAuditInfo(search);//业务层获取底层方法，返回数据

            return View(model);
        }



        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult Audited()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var APID = Request["APID"];//获取前台传递的数据 
            if (string.IsNullOrEmpty(APID))
            {
                return Json(ajax);
            }

            EnrollAudit eran = new EnrollAudit();

            eran.APID = APID;
            eran.UpdateTime = DateTime.Now;
            eran.UpdatorId = UserSessionWX.userid;
            eran.ApprovedTime = DateTime.Now;
            eran.ApprovedBy = UserSessionWX.userid;

            eran.StateID = 3;//审核通过状态
            if (EnrollAuditInfoData.Audited(eran)>0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult AuditNotThrough()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var APID = Request["APID"];//获取前台传递的数据 
            if (string.IsNullOrEmpty(APID))
            {
                return Json(ajax);
            }
            EnrollAudit eran = new EnrollAudit();


            eran.APID = APID;
            eran.UpdateTime = DateTime.Now;
            eran.UpdatorId = UserSession.userid;


            eran.StateID = 4;//审核通过状态


            if (EnrollAuditInfoData.AuditNotThrough(eran, 7) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }


    }
}
