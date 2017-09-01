using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
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
    public partial class OfficeController : Controller
    {


        /// <summary>
        /// 报销查询
        /// </summary>
        public ActionResult ReimburseList(ReimbursetListSearchModel search)
        {
            ReimbursetListViewModel model = new ReimbursetListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //按钮下拉项
            List<CommonEntity> StateIDIL = CommonData.GetDictionaryList(20);//1是字典类型值,仅供测试参考
            model.StateIDIL = CommonData.Instance.GetBropDownListData(StateIDIL);


            List<CommonEntity> TeacherIDIL = CommonData.GetTeachersList();//1是字典类型值,仅供测试参考
            model.TeacherIDIL = CommonData.Instance.GetBropDownListData(TeacherIDIL);


            string SYS_Role = "0";
            List<string> roles = UserSession.roles;//取账号角色 
            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i] == "1" || roles[i] == "4")
                {
                    SYS_Role = "1";
                }
            }

            ViewData["SYS_Role"] = SYS_Role;


            model.Reimbursetlist = ReimburseData.GetReimburseList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 根据 ID获取按钮详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetReimburse(int id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Reimburse btn = ReimburseData.GetReimburseByID(id);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }

        /// <summary>
        /// 保存编辑按钮
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveReimburse()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Reimburse rb = (Reimburse)(JsonConvert.DeserializeObject(data.ToString(), typeof(Reimburse)));

            if (ReimburseData.UpdateReimburse(rb))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 新增优惠
        /// </summary>
        /// <returns></returns>
        public JsonResult AddReimburse()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Reimburse rb = (Reimburse)(JsonConvert.DeserializeObject(data.ToString(), typeof(Reimburse)));

            rb.StateID = "1";//状态1为待审核，2为审核，3为不通过
            rb.CreateTime = DateTime.Now; //创建时间
            rb.CreatorId = UserSession.userid;//创建人
            if (ReimburseData.AddReimburse(rb) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }



        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult To_Examine_save()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Reimburse rb = (Reimburse)(JsonConvert.DeserializeObject(data.ToString(), typeof(Reimburse)));

            rb.StateID = "2";//状态1为待审核，2为审核，3为不通过
            rb.AuditingTime = DateTime.Now; //创建时间
            rb.AuditingID = UserSession.userid;//创建人
            if (ReimburseData.UpdateReimburse(rb))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

    }
}