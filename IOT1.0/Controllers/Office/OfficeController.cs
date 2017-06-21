﻿using DataProvider;
using DataProvider.Data;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Office
{
    public class OfficeController : Controller
    {

        /// <summary>
        /// 优惠列表查询
        /// </summary>
        public ActionResult MessageList(MessageListSearchModel search)
        {
            MessageListViewModel model = new MessageListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页 

            //多沟选框
            List<DataProvider.Data.CommonData.SYS_Role> SourceIL = CommonData.GetSYS_SystemRoleList(3);//1是字典类型值,仅供测试参考 
            ViewData["SYS_Role"] = SourceIL;

            model.Messagelist = MessageData.GetMessageList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 根据 ID获取按钮详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetMessageID(int id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Discount dis = DiscountData.GetDiscountByID(id);//业务层获取底层方法，返回数据
            if (dis != null)
            {
                ajax.data = dis;//放入数据
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }

        /// <summary>
        /// 保存编辑按钮
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveMessage()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Message mes = (Message)(JsonConvert.DeserializeObject(data.ToString(), typeof(Message)));
            if (MessageData.UpdateMessage(mes))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public JsonResult AddDiscount(FormCollection form)
        {

            var winnars = from x in form.AllKeys

                        where form[x] != "false"

                           select x;//找到你在视图中选定的要删除的数据

               foreach (var id in winnars)

             {

                  if(id != "selectAll")

                  {

                       int number = int.Parse(id);

                     // var deleteData =_db.tb_askForLeave.First(m => m.employeeNumber == number);//找到要删除的数据

                      //_db.tb_askForLeave.DeleteObject(deleteData);

                  }

                 continue;

             }


            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Message mes = (Message)(JsonConvert.DeserializeObject(data.ToString(), typeof(Message)));

            mes.CreateTime = DateTime.Now; //创建时间
            mes.CreatorId = UserSession.userid;//创建人
            if (MessageData.AddMessage(mes) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        

 

    }
}