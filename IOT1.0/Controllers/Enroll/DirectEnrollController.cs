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

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {

        /// <summary>
        /// 直报页面绑定下拉框，直接报名
        /// </summary>
        /// <returns></returns>
        public ActionResult DirectEnroll()
        {
            EnrollListViewModel model = new EnrollListViewModel();//页面模型 
            //分校下拉项
            List<CommonEntity> ComCodeIL = CommonData.Get_SYS_Company_List();//分校
            model.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);
            model.search.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);


            //来源渠道
            List<CommonEntity> SourceIL = CommonData.GetDictionaryList(2);//1是字典类型值,仅供测试参考
            model.SourceIL = CommonData.Instance.GetBropDownListData(SourceIL);

            return View(model);//返回页面模型
        }





        /// <summary>
        /// 新增预约单,根据填写的姓名和手机号码查询是否已经存在相应的(Students表)学生信息
        /// </summary>
        /// <returns></returns>
        public JsonResult AddDirect()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Appointment obj = (Appointment)(JsonConvert.DeserializeObject(data.ToString(), typeof(Appointment)));
            obj.ID = CommonData.DPGetTableMaxId("AP", "ID", "Appointment", 8);
            obj.CreateTime = DateTime.Now;
            obj.CreatorId = UserSession.userid;
            obj.ApStateID = 1;//默认未跟进
       
            if (AppointmentData.Add(obj))//注意时间类型
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
                string studid = StudentData.BindPhone_insert(obj.ApTel, obj.ApName);
                if (string.IsNullOrEmpty(studid))// 判断是否已经有该学生信息
                {
                     
                }
                else
                {
                    StudentData.BindStudentforAP(studid,obj.ID);
                    ajax.msg = "您已经是正式学员！";
                }
                vw_Appointment vw_Appointment = StudentData.Getvw_AppointmentList(obj.ApTel, obj.ApName);
                ajax.data = vw_Appointment;//因为不是学员的话要取到相应的预约单号,直接根据是否已经有相应的学号来判断是否是学员

            }
            return Json(ajax);
        }





        /// <summary>
        /// 修改预约单,根据填写的姓名和手机号码查询是否已经存在相应的(Students表)学生信息
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateDirect()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Appointment obj = (Appointment)(JsonConvert.DeserializeObject(data.ToString(), typeof(Appointment)));

            obj.UpdateTime = DateTime.Now;
            obj.UpdatorId= UserSession.userid;

            if (AppointmentData.Update(obj))//注意时间类型
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
               
            }
            return Json(ajax);
        }







    }
}