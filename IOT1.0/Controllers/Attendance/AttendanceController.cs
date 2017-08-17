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
using Newtonsoft.Json.Linq;
using IOT1._0.Models;

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

        public JsonResult StudentEvaluate()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            vw_ClassAttendanceList cls = (vw_ClassAttendanceList)(JsonConvert.DeserializeObject(data.ToString(), typeof(vw_ClassAttendanceList)));
             List<vw_StudentEvaluate> btn =AttendaceData.getStudentEvaluate(cls.ClassID, cls.ClassIndex);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.status = EnumAjaxStatus.Success;
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

        /// <summary>
        /// 获取调课的相关信息
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 保存调课信息
        /// </summary>
        /// <returns></returns>
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
        public JsonResult getCheckStudentData()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取信息失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            vw_ClassAttendanceList cls = (vw_ClassAttendanceList)(JsonConvert.DeserializeObject(data.ToString(), typeof(vw_ClassAttendanceList)));

            List<AttendanceRecord> btn = AttendaceData.getStudentCheck(cls.ClassID, cls.ClassIndex);//业务层获取底层方法，返回数据


            if (btn !=null)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取信息成功";
                ajax.data = new Object[]{btn,CommonData.GetDictionaryList(18)};
            }

            return Json(ajax);

        }


        public JsonResult saveStudentAttendance()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存考勤失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            List<AttendanceRecord> cls = (List<AttendanceRecord>)(JsonConvert.DeserializeObject(data.ToString(), typeof(List<AttendanceRecord>)));


            foreach (AttendanceRecord value in cls)
            {
                DataProvider.Entities.Enroll enroll = EnrollData.getEnrollByStudentClass(value.StudentID, value.ClassID);

            }


            if (AttendaceData.saveStudentAttendance(cls,UserSession.userid))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存考勤成功";

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
            if (AttendaceData.AddClassListJob(cla) > 0)
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
            ClassListJob cls = (ClassListJob)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassListJob)));
            List<vw_ClassListJob> btn = AttendaceData.ClassListJob(cls.Classid, cls.Classindex);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
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
            int obj = AttendaceData.UpdateClassList(cla);
            if (obj > 0)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "保存成功";
                ajax.data = ajax.msg;
            }

            return Json(ajax);
        }




        /// <summary>
        /// 保存编辑按钮
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult DELETE()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "删除失败！";//前台获取，用于显示提示信息
            var data = Request["ID"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            if (AttendaceData.DELETE_ClassListJob(data) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "删除成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }






    }


}
