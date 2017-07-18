﻿using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Teach
{
    public partial class TeachController : Controller
    {
        public ActionResult Shift()
        {
            return View();
        }


        /// <summary>
        /// 教师列表查询
        /// </summary>
        public ActionResult Teacher(TeacherSearchModel search)
        {
            TeacherViewModel model = new TeacherViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //按钮下拉项
            List<CommonEntity> ButtonIL = CommonData.GetDictionaryList(4);//1是字典类型值,仅供测试参考
            model.buttonIL = CommonData.Instance.GetBropDownListData(ButtonIL);


            //多沟选框
            List<DataProvider.Data.CommonData.SYS_Role> SourceIL = CommonData.GetSYS_SystemRoleList(3);
            ViewData["SYS_Role"] = SourceIL;

            model.Teacherslist = TeacherData.GetTeachersList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 保存编辑按钮
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveButton()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Teachers btn = (Teachers)(JsonConvert.DeserializeObject(data.ToString(), typeof(Teachers)));

            if (TeacherData.UpdateTeacher(btn))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 新增教师
        /// </summary>
        /// <returns></returns>
        public JsonResult AddTeach()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Teachers teacher = (Teachers)(JsonConvert.DeserializeObject(data.ToString(), typeof(Teachers)));
            SYSAccount sys = (SYSAccount)(JsonConvert.DeserializeObject(data.ToString(), typeof(SYSAccount)));
            RandomOperate operate = new RandomOperate();

            teacher.CreateTime = DateTime.Now;
            teacher.CreatorId = UserSession.userid;
            teacher.ID = operate.GenerateCheckCode(36);

            
            teacher.BindAccountID = operate.GenerateCheckCode(30);
            sys.ACC_Account = teacher.BindAccountID;
            sys.ACC_CreatedBy = UserSession.userid;
            sys.ACC_CreatedOn = DateTime.Now;
            sys.ACC_Password = operate.CreateMD5Hash("123");

            if (!string.IsNullOrEmpty(TeacherData.AddTeachers(teacher, sys)))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }


        /// <summary>
        /// 根据按钮ID获取按钮详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetTeachByID(string id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Teachers btn = TeacherData.GetTeachByID(id);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }






        /// <summary>
        ///设置权限
        /// </summary>
        /// <returns></returns>
        public JsonResult AddSYS_SystemRole()
        {


            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            SYSAccountRole sys = (SYSAccountRole)(JsonConvert.DeserializeObject(data.ToString(), typeof(SYSAccountRole)));
            if (sys.AR_SystemRoleIdS != null)
            {
                if (!string.IsNullOrEmpty(TeacherData.AddSYS_SystemRole(sys)))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            } 
            } 
            return Json(ajax);
        }







        
    }
}




