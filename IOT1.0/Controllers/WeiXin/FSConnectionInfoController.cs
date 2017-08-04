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
            model.Enclosure = AttendaceData.ClassListJob(vw_AttendanceRecord.ClassID, vw_AttendanceRecord.ClassIndex);//业务层获取底层方法，返回数据
            
            return View(model);
        }



        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        public JsonResult ClassListJob()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            ClassListJob cls = new ClassListJob();
            List<vw_ClassListJob> btn = AttendaceData.ClassListJob(cls.Classid, cls.Classindex);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }




    }
}
