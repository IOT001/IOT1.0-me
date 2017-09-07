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
            string tid = Request["tid"];//获取协议id
            if (string.IsNullOrEmpty(tid))
            {
                return Json(ajax);
            }
            Transfer rb = TransferData.GetTransferByID(int.Parse(tid));//获取协议
            DataProvider.Entities.Enroll en = EnrollData.GetEnrollByID(rb.JENID)
            rb.StateID = 1;//状态1为待审核，2为审核，3为不通过
            rb.CreateTime = DateTime.Now; //创建时间
            rb.CreatorId = UserSession.userid;//创建人
            if (TransferData.AddTransfer(rb) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
    }
}
