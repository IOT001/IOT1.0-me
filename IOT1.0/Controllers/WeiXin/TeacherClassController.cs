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
        // GET: /TeacherClass/
        /// <summary>
        /// 教师对应的上课记录
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherClassList(TeacherClassListSearch search)
        {
            WX_TeacherClassListViewModel model = new WX_TeacherClassListViewModel();
            model.search = search;
            model.search.PageSize = 9999;//每页显示1000条数据
            model.search.CurrentPage = model.search.CurrentPage <= 0 ? 1 : model.search.CurrentPage;//获取当前页
            Teachers s = TeacherData.GetTeachByID(UserSessionWX.userid);//获取当前教师
            if (s != null)
            {
                model.search.teacherID = s.ID;
                model.TeacherClassList = TeacherClassData.GetAttendanceRecordList(search);
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
            ar.ClockTime = DateTime.Now;//考勤时间
            ar.AttendanceTypeID = int.Parse(Request["AttendanceTypeID"].ToString());//通过哪个按钮触发，正常2，缺勤3，请假4
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
    }
}
