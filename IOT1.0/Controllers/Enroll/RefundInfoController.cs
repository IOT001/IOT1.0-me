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

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {
        //
        // GET: /RefundInfo/

        public ActionResult RefundInfo(string ID,int typeID)
        {
            RefundInfoModel model = new RefundInfoModel();//页面模型


            if (typeID == 1)
            {
                model.vw_EnrollList = RefundInfoData.Getvw_EnrollByID(ID);//填充报名数据
            }

            else
            {
                model.RefundList = RefundInfoData.Getvw_RefundByID(int.Parse(ID));//填充退款数据
            }
            model.typeID = typeID;//页面跳转类型
            return View(model);//返回页面模型
        }




        /// <summary>
        /// 保存退款申请
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveRefundInfo()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Refund obj = (Refund)(JsonConvert.DeserializeObject(data.ToString(), typeof(Refund)));//序列化成对象
            obj.CreateTime = DateTime.Now;
            obj.CreatorId = UserSession.userid;

             

            if (RefundInfoData.Refund(obj.StudentID, obj.EnrollID) > 0)
            {
                ajax.msg = "您已经申请过了，无法再次申请！";//前台会安装这个信息弹出信息
                ajax.status = EnumAjaxStatus.Success;
            }
            else
            {
                if (RefundInfoData.AddRefund(obj) > 0)//注意时间类型，而且需要在前台把所有的值，也能在后台复制
                {
                    ajax.msg = "保存成功！";//前台会安装这个信息弹出信息
                    ajax.status = EnumAjaxStatus.Success;
                }
            }


            return Json(ajax);
        }




        /// <summary>
        /// 保存退款审核
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveRefund()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Refund obj = (Refund)(JsonConvert.DeserializeObject(data.ToString(), typeof(Refund)));//序列化成对象
            obj.AuditingTime = DateTime.Now;
            obj.AuditingID = UserSession.userid;


            if (RefundInfoData.UpdateRefund(obj))//注意时间类型，而且需要在前台把所有的值，也能在后台复制
            {
                ajax.msg = "保存成功！";//前台会安装这个信息弹出信息
                ajax.status = EnumAjaxStatus.Success;
            }

            return Json(ajax);
        }


        

    }
}
