using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
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
        //
        // GET: /EnrollAudit/

        public ActionResult EnrollAuditList()
        {
            return View();
        }
        /// <summary>
        /// 转让协议的审核
        /// </summary>
        /// <returns></returns>
        public ActionResult TransferAudit() {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            string tid = Request["tfid"];//获取协议id
            if (string.IsNullOrEmpty(tid))
            {
                return Json(ajax);
            }
            Transfer rb = TransferData.GetTransferByID(int.Parse(tid));//获取协议
            DataProvider.Entities.Enroll jen = EnrollData.GetEnrollByID(rb.JENID);//甲方报名记录
            DataProvider.Entities.Enroll yen = EnrollData.getEnrollByStudentClass(rb.YStudentID, rb.YClassid);//乙方报名记录

            if (yen != null)//如果乙方有报名记录，则在原来的报名记录上增加课时，并且减少甲方课时，同时插入流水记录
            {
                EnrollData.TransferAudit1(jen, yen, UserSession.userid);
            }
            else//如果乙方没有报名记录，则新增报名记录
            {
                DataProvider.Entities.Enroll yy = new DataProvider.Entities.Enroll();
                yy.ID = CommonData.DPGetTableMaxId("EN", "ID", "Enroll", 8);
                yy.APID = "";
                yy.StudentID = rb.YStudentID;
                yy.ClassID = rb.YClassid;
                yy.ClassHour = rb.TranHour;
                yy.UsedHour = 0;
                yy.Price = 0;
                yy.Paid = 0;
                yy.CreatorId = UserSession.userid;
                yy.CreateTime = DateTime.Now;
                yy.StateID = 0;
                EnrollData.TransferAudit2(jen, yy, UserSession.userid);
            }


                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            
            return Json(ajax);
        }
    }
}
