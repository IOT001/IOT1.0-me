using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using EBiWeb.Common;
using IOT1._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {
        //
        // GET: /EnrollManageList/

        public ActionResult EnrollManageList(EnrollListSearchModel search)
        {
            EnrollManageListModel model = new EnrollManageListModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            search.islesson = "0";


               //分校下拉项
            List<CommonEntity> ComCodeIL = CommonData.Get_SYS_Company_List();//分校
            model.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);
            model.search.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);


            //学员状态下拉项
            List<CommonEntity> StudentSourceIL = CommonData.GetDictionaryList(19);//1是字典类型值,仅供测试参考
            model.search.StudentSourceIL = CommonData.Instance.GetBropDownListData(StudentSourceIL);


            model.EnrollManagelist = EnrollData.GeEnrollList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 调整剩余课时
        /// </summary>
        /// <returns></returns>

        public ActionResult AdjustLeftHour()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "调整课时失败，请联系管理员！";//前台获取，用于显示提示信息
            string ENID = Request["ENID"];//报名ID
            int AdjustNum = int.Parse(Request["AdjustNum"].ToString());//调整数
            if (string.IsNullOrEmpty(ENID))
            {
                return Json(ajax);
            }

            if (EnrollData.AdjustLeftHour(ENID, AdjustNum, UserSession.userid))
            {
                ajax.msg = "调整课时成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 根据报名ID返回课时消耗记录
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public JsonResult GetHoursLogByENID(string enid)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            List<TransferRecord> loglist = EnrollData.GetHoursLogByENID(enid);
            ajax.data = loglist;
            return Json(new { total = loglist.Count(), rows = loglist, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult Recovery()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "恢复成功！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化

            DataProvider.Entities.Enroll enl = (DataProvider.Entities.Enroll)(JsonConvert.DeserializeObject(data.ToString(), typeof(DataProvider.Entities.Enroll)));
            enl.UpdateTime = DateTime.Now;//添加修改时间
            enl.UpdatorId = UserSession.userid;//添加修改人

            if (EnrollData.UpdateEnroll_ed(enl)>0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "恢复成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult Frozen()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "冻结失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化

            DataProvider.Entities.Enroll enl = (DataProvider.Entities.Enroll)(JsonConvert.DeserializeObject(data.ToString(), typeof(DataProvider.Entities.Enroll)));
            enl.UpdateTime = DateTime.Now;  //添加修改时间
            enl.UpdatorId = UserSession.userid;//添加修改人
            if (EnrollData.UpdateEnroll_ed(enl) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "冻结成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 完结报名，会清空当前课时，记录课时变化日志
        /// </summary>
        /// <returns></returns>
        public ActionResult FinishEnroll()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "完结失败！";
            string enid = Request["enid"];//报名记录的ID

            bool ret = EnrollData.FinishEnroll(enid, UserSession.userid);
            if (ret)
            {
                ajax.msg = "完结成功！剩余课时已被清零！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }




        /// <summary>
        /// 导出到Excel的操作
        /// </summary>
        /// <param name="json"></param>
        public void ExportToExcel(string Name, string BindPhone, string timeStart, string timeEnd, string ComCode, string Large, string Small)
        {

            DataTable datasource = EnrollData.DPExportToExcel(Name, BindPhone, timeStart, timeEnd, ComCode, Large, Small,"0");
            ERP.Models.MyNPOIModel.ExportByWeb(datasource, "报名管理", "报名管理" + ".xls");

        }





        /// <summary>
        /// 调整报名课时
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult Ajustment()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "调整报名课时失败！";//前台获取，用于显示提示信息
            string ENID = Request["ENID"];//报名ID
            int ClassHour = int.Parse(Request["ClassHour"].ToString());//调整课时数
            decimal Price = decimal.Parse(Request["Price"].ToString());//调整金额

            if (EnrollData.AjustmentEnroll(ENID, ClassHour, Price, UserSession.userid))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "调整报名课时成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }




    }
}
