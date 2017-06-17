using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider;
using Newtonsoft.Json;

namespace IOT1._0.Controllers.Attendance
{
    public class AttendanceController : Controller
    {
        //
        // GET: /AttendanceList/

        public ActionResult AttendanceList(AttendanceSearchModel search)
        {
            AttendanceListViewModel model = new AttendanceListViewModel();
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            model.buttonlist = AttendaceData.GetButtonList(search);
            return View(model);
        }

        public JsonResult StudentEvaluate(vw_ClassAttendanceList cls)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
             List<vw_StudentEvaluate> btn =AttendaceData.getStudentEvaluate(cls.ClassID, cls.ClassIndex);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }
        public JsonResult saveStudentEvaluate()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            List<vw_StudentEvaluate> btn = (List<vw_StudentEvaluate>)(JsonConvert.DeserializeObject(data.ToString(), typeof(List<vw_StudentEvaluate>)));
            if (AttendaceData.saveStudentEvalute(btn))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存成功";
                ajax.data = ajax.msg;
            }
            
            return Json(ajax);
        }

        public JsonResult getModifyClassOptions()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            
            

            Object[] data = new Object[3];
            
            ///房间
            data[0] = CommonData.GetDictionaryList(11);
            ///时间段
            data[1] = CommonData.GetDictionaryList(8);
            ///老师列表
            data[2] = TeacherData.getOnWorkTeachers();

            
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功";
                ajax.data = data;
            

            return Json(ajax);
        }
        public JsonResult saveModifyClass()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            ClassList btn = (ClassList)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassList)));
            int obj = ClassListData.UpdateClassList(btn);
            if (obj > 0 )
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存成功";
                ajax.data = ajax.msg;
            }

            return Json(ajax);
        }
    }
}
