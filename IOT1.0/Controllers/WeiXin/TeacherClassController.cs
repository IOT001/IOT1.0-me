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

namespace IOT1._0.Controllers.WeiXin
{
    public partial class WeixinController : Controller
    {

        //GET: /TeacherClass/
        //<summary>
        //教师对应的上课记录
        //</summary>
        //<returns></returns>
        public ActionResult TeacherClassList(TeacherClassListSearch search)
        {
            WX_TeacherClassListViewModel model = new WX_TeacherClassListViewModel();
            model.search = search;
            model.search.PageSize = 15;
            model.search.CurrentPage = Convert.ToInt32(Request["CurrentPage"]) <= 0 ? 1 : Convert.ToInt32(Request["CurrentPage"]);//获取当前页

             //上课时间-开始时间 
            search.timeStart = search.timeStart += " 00:00:00:000";//对日期做特殊处理,取第一个星期的最小值
        
             // 上课时间-结束时间 
             search.timeEnd = search.timeEnd += " 23:59:59.999";//对日期做特殊处理,取最后一个星期的最大值


            search.PageSize = model.search.PageSize * model.search.CurrentPage;//微信端显示数量
            search.CurrentPage = 1;

            Teachers s = TeacherData.GetTeachByMobilePhone(UserSessionWX.userid);//获取当前教师
            if (s != null)
            {
                model.search.teacherID = s.ID;
                model.TeacherClassList = TeacherClassData.GetAttendanceRecordList(search);
                model.search.TotalPageCount = model.TeacherClassList.TotalPageCount;
            }
            else
            { 
            }
            return View(model);
        }
        /// <summary>
        /// 教师考勤详细页
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherClassInfo()
        {
            string classid = Request["classid"];
            int ClassIndex = int.Parse(Request["classindex"].ToString());
            WX_TeacherClassInfoViewModel model = new WX_TeacherClassInfoViewModel();
            model.arlist = TeacherClassData.GetAttendanceRecordByClassID(classid,ClassIndex);
            model.acl = TeacherClassData.GetOneClassAttendanceList(classid, ClassIndex);
            return View(model);
        }
        /// <summary>
        /// 微信端学员考勤
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveStudentAttendance_WX()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存考勤失败！";//前台获取，用于显示提示信息
            AttendanceRecord ar = new AttendanceRecord();
            ar.StudentID = Request["StudentID"].ToString();//学员号
            ar.ClassID = Request["ClassID"].ToString();//班级号
            ar.ClassIndex = int.Parse(Request["ClassIndex"].ToString());//班级索引
            
            ar.AttendanceTypeID = int.Parse(Request["AttendanceTypeID"].ToString());//通过哪个按钮触发，正常2，缺勤3，请假4
            if (ar.AttendanceTypeID == 2)//正常考勤才赋值时间
            {
                ar.ClockTime = DateTime.Now;//考勤时间
            }
            else
            {
                ar.ClockTime = null;
            }
            List<AttendanceRecord> cls = new List<AttendanceRecord>();
            cls.Add(ar);
            if (AttendaceData.saveStudentAttendance(cls, UserSession.userid))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存考勤成功";

            }
            return Json(ajax);
        }
        /// <summary>
        /// 保存学员评价
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveStudentEvalute_WX()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存考勤失败！";//前台获取，用于显示提示信息
            AttendanceRecord ar = new AttendanceRecord();
            ar.StudentID = Request["StudentID"].ToString();//学员号
            ar.ClassID = Request["ClassID"].ToString();//班级号
            ar.ClassIndex = int.Parse(Request["ClassIndex"].ToString());//班级索引
            ar.Evaluate = Request["Evaluate"].ToString();//评价内容
            ar.UpdateTime = DateTime.Now;
            ar.UpdatorId = UserSessionWX.userid;
            if (AttendaceData.SaveStudentEvalute_WX(ar))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存考勤成功";
            }
            return Json(ajax);
        }








        /// <summary>
        /// 保存作业内容
        /// </summary>
        /// <returns></returns>
        public JsonResult savClassList()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            ClassList cla = (ClassList)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassList)));
            int obj = TeacherClassData.UpdateClassList(cla);
            if (obj > 0)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存成功";
                ajax.data = ajax.msg;
            }

            return Json(ajax);
        }



        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns> 
        public JsonResult Upload()
        {

            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化 
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            JObject jsonObj = JObject.Parse(data);
            Picture Picture = new Picture();
            var ret = Picture.DPUpLoadFile(jsonObj);

            ClassListJob cla = new ClassListJob();
            cla.Classid = jsonObj["Classid"].ToString();
            cla.Classindex = int.Parse(jsonObj["Classindex"].ToString());
            cla.CreatorId = UserSession.userid;
            cla.CreateTime = DateTime.Now;
            cla.FileName = ret["filename"];//文件名称
            cla.ContentType = ret["ContentType"];
            if (TeacherClassData.AddClassListJob(cla) > 0)//
            {
                ajax.msg = "上传成功！";
                ajax.status = EnumAjaxStatus.Success;
                ajax.data = ret;
            }


            return Json(ajax);

        }




        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        public JsonResult ClassListJob_Teacher()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            ClassListJob cls = (ClassListJob)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassListJob)));
            List<vw_ClassListJob> btn = TeacherClassData.ClassListJob(cls.Classid, cls.Classindex);//业务层获取底层方法，返回数据
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
