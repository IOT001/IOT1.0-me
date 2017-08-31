using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Common
{
    public class CommonController : Controller
    {
    
        /// <summary>
        /// 调用签名窗口
        /// </summary>
        /// <returns></returns>
        public ActionResult sign()
        {
            return View();
        }
        /// <summary>
        /// 保存对应主键是Int表的签名
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveIntKeySign()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;
            ajax.msg = "签名失败！";
            int keyid = int.Parse(Request["id"].ToString());//获取主键值
            int typeid = int.Parse(Request["typeid"].ToString());//获取类型，1入学协议，2转让协议
            string imagedata = Request["imagedata"];
            SignImage si = new SignImage();
            si.IntKey = keyid;
            si.TypeID = typeid;
            si.ImageData = imagedata;
            si.CreateTime = DateTime.Now;
            int res = CommonData.SaveSign(si);
            if (res > 0)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "签名成功！";
            }
            return Json(ajax);
            
        }
        /// <summary>
        /// 字符串主键签名
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveStringKeySign()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;
            ajax.msg = "签名失败！";
            string keyid = Request["id"].ToString();//获取主键值
            int typeid = int.Parse(Request["typeid"].ToString());//获取类型，1入学协议，2转让协议
            string imagedata = Request["imagedata"];
            SignImage si = new SignImage();
            si.StringKey = keyid;
            si.TypeID = typeid;
            si.ImageData = imagedata;
            si.CreateTime = DateTime.Now;
            int res = CommonData.SaveSign(si);
            if (res > 0)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "签名成功！";
            }
            return Json(ajax);

        }
    }
}
