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

        public ActionResult Curriculum(CurriculumSearchModel search)
        {
            CurriculumViewModel model = new CurriculumViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

            //分校下拉项
            List<CommonEntity> ComCodeIL = CommonData.Get_SYS_Company_COMP_Code(UserSession.comcode);//分校
            model.ComCodeIL = CommonData.Instance.GetBropDownListData_Choice(ComCodeIL);
            model.search.ComCodeIL1 = CommonData.Instance.GetBropDownListData_Choice(ComCodeIL);



            if (UserSession.comcode != null && UserSession.comcode != "1")
            { 
                search.ComCode = UserSession.comcode;//默认查询当前分校的人员 
            }


            model.buttonlist = CourseData.GetButtonList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 根据按钮ID获取按钮详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetCourseByID(int id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            
                Course btn = CourseData.getCourseById(id);//业务层获取底层方法，返回数据
                if (btn != null)
                {
                    ajax.data = new Object[]{btn,CommonData.GetDictionaryList(5)};//放入数据
                    ajax.status = EnumAjaxStatus.Success;
                    ajax.msg = "获取成功！";
                }
           
            return Json(ajax);

        }
        public JsonResult SaveCourse()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Course btn = (Course)(JsonConvert.DeserializeObject(data.ToString(), typeof(Course)));
            if (CourseData.updateCourse(btn))
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }

            return Json(ajax);
        }
        public JsonResult AddCourse()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Course btn = (Course)(JsonConvert.DeserializeObject(data.ToString(), typeof(Course)));
            if (CourseData.addCourse(btn) > 0)
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }

            return Json(ajax);

        }


    }
}
