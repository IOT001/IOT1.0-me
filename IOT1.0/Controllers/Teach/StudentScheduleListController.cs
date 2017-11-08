using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataProvider.Models;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider;
using Newtonsoft.Json;
using IOT1._0.Models;

namespace IOT1._0.Controllers.Teach
{
    public partial class TeachController : Controller
    {
        //
        // GET: /Curriculum/

        public ActionResult StudentScheduleList(StudentScheduleSearchModel search)
        {
            StudentScheduleListViewModel model = new StudentScheduleListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

           
            model.AttendanceRecordlist = StudentScheduleListData.GetAttendanceRecordList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 获取课程相同的班级,用于调班
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClassesItems()
        {
           int CourseID1= int.Parse(Request["CourseID"]);//预约单
           string ClassID = Request["ClassID"].ToString();//班级ID
            //根据课程ID获取值
           List<CommonEntity> ClassesList = StudentScheduleListData.GetClassesList(CourseID1, ClassID); 
            return Json(ClassesList);
        }

         


        /// <summary>
        /// 保存AttendanceRecord
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveAttendanceRecord()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            AttendanceRecord att = (AttendanceRecord)(JsonConvert.DeserializeObject(data.ToString(), typeof(AttendanceRecord)));
 

            string[] ClassID= att.ClassID.Split(',');
            att.ClassID = ClassID[0].ToString();
            att.ClassIndex =int.Parse(ClassID[1]); 
            att.UpdateTime = DateTime.Now;
            att.UpdatorId = UserSession.userid;
            if (StudentScheduleListData.UpdateAttendanceRecord(att))
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }

            return Json(ajax);
        }


         

    }
}
