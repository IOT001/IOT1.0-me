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

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {
        /// <summary>
        /// 报名记录
        /// </summary>
        /// <returns></returns>
        public ActionResult EnrollList(EnrollListSearchModel search )
        {
            EnrollListViewModel model = new EnrollListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //根据方式拉项
            List<CommonEntity> FollowTypeList = CommonData.GetDictionaryList(13);//13跟进方式
            model.FollowTypeIL = CommonData.Instance.GetBropDownListData(FollowTypeList);

            //根据方式拉项
            List<CommonEntity> IntentTypeList = CommonData.GetDictionaryList(14);//13跟进方式
            model.IntentTypeIL = CommonData.Instance.GetBropDownListData(IntentTypeList);

            List<CommonEntity> SourceIL = CommonData.GetDictionaryList(2);//1是字典类型值,仅供测试参考
            model.SourceIL = CommonData.Instance.GetBropDownListData(SourceIL);

            //分校下拉项
            List<CommonEntity> ComCodeIL = CommonData.Get_SYS_Company_List();//分校
            model.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);
            model.search.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);

            model.AppointmentList = AppointmentData.GetAPList(search); //填充页面模型数据
            return View(model);//返回页面模型
        }

        /// <summary>
        /// 保存编辑预约单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveAppointment()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Appointment obj = (Appointment)(JsonConvert.DeserializeObject(data.ToString(), typeof(Appointment)));//序列化成对象
            obj.UpdateTime = DateTime.Now;
            obj.UpdatorId = UserSession.userid;

            if (AppointmentData.Update(obj))//注意时间类型，而且需要在前台把所有的值，也能在后台复制
            {
                ajax.msg = "保存成功！";//前台会安装这个信息弹出信息
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 根据ID获取资源单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAppointmentByID(string id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Appointment ap = AppointmentData.GetOneByID(id);//业务层获取底层方法，返回数据
            if (ap != null)
            {
                ajax.data = ap;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }
        /// <summary>
        /// 新增预约单
        /// </summary>
        /// <returns></returns>
        public JsonResult AddAppointment()
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
            if (string.IsNullOrEmpty(obj.ComCode))
            {
                ajax.msg = "请选择所属分校！";
                return Json(ajax);
            }
            if (AppointmentData.Add(obj))//注意时间类型
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 根据预约号返回跟进记录
        /// </summary>
        /// <param name="apid"></param>
        /// <returns></returns>
        public JsonResult GetFollowListByAPID(string apid)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            List<FollowRecord> FollowList = AppointmentData.GetFollowListByAPID(apid);
            ajax.data = FollowList;
            return Json(new { total = 1, rows = FollowList, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增跟进记录
        /// </summary>
        /// <returns></returns>
        public JsonResult AddFollow()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "跟进失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            FollowRecord obj = (FollowRecord)(JsonConvert.DeserializeObject(data.ToString(), typeof(FollowRecord)));
            obj.FollowTime = DateTime.Now;
            obj.FollowPersonID = UserSession.userid;
            if (AppointmentData.AddFollow(obj))//注意时间类型
            {
                ajax.msg = "跟进成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

   
        /// <summary>
        /// 票据打印
        /// </summary>
        /// <returns></returns>
        public ActionResult InvoicePrint()
        {
            AppointmentPrintModel m = new AppointmentPrintModel();
            string apid = Request["apid"].ToString();
            m.EnrollList = EnrollData.GetEnrollPrintByApid(apid);
            m.bill = EnrollData.GetOneBillConfig();
            return View(m);
        }

        /// <summary>
        ///  按条件查询试听课
        /// </summary>
        /// <returns></returns>
        public ActionResult STClassList()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            ClassesListSearchModel search = new ClassesListSearchModel();
            string CourseName = Request["CourseName"];
            string StartTime_start = Request["StartTime_start"];
            string StartTime_end = Request["StartTime_end"];
            string islisten = Request["islisten"];//是否查询试听列表
            search.CourseID = CourseName;
            if (!string.IsNullOrEmpty(StartTime_start))
                search.StartTime_start = StartTime_start;
            if (!string.IsNullOrEmpty(StartTime_end))
                search.StartTime_end = StartTime_end;

            search.CurrentPage = 1;//当前页
            search.PageSize = 99999;//不想分页就设置成一个较大的值
            search.islisten = islisten;
            List<vw_Classes> vw_Classes = ClassesData.GeClassesList(search);
            ajax.data = vw_Classes;
            return Json(new { total = 1, rows = vw_Classes, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 安排试听
        /// </summary>
        /// <returns></returns>
        public ActionResult AddST()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            string apid = Request["apid"];//预约单
            string classid = Request["classid"];//班级号
            if (string.IsNullOrEmpty(apid))
            {
                return Json(ajax);
            }
            DataProvider.Entities.Enroll obj = new DataProvider.Entities.Enroll();
            Appointment ap = AppointmentData.GetOneByID(apid);

            obj.ID = CommonData.DPGetTableMaxId("EN", "ID", "Enroll", 8);
            obj.APID = apid;
            obj.StudentID = ap.ApStudentID;
            obj.ClassID = classid;
            obj.CreatorId = UserSession.userid;
            obj.CreateTime = DateTime.Now;
            if (EnrollData.Add(obj))//注意时间类型
            {
                ajax.msg = "预约试听报名成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 获取可用的优惠下拉信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDiscountItems()
        {
            //根据方式拉项
            List<Discount> DisCountList = AppointmentData.GetDiscountItems();
     
            return Json(DisCountList);
        }
        /// <summary>
        /// 报名结算
        /// </summary>
        /// <returns></returns>
        public JsonResult AddEnroll(string _did)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "结算失败！";//前台获取，用于显示提示信息
            if (string.IsNullOrEmpty(_did))
            {
                return Json(ajax);
            }
            JArray ja = (JArray)JsonConvert.DeserializeObject(_did);//序列化前台获取的列表数据，因为报名数据可能存在多笔
            
            int submittype = 1;//结算，直接完成报名
            foreach (var item in ja)
            {
                int discountid = int.Parse(((JObject)item)["discountid"].ToString() == "" ? "0" : ((JObject)item)["discountid"].ToString());//选择的优惠ID
                if (discountid == -1)//存在自定义优惠项
                {
                    submittype = 2;
                }
            }
            if (submittype == 1)
            {
                List<DataProvider.Entities.Enroll> ENList = new List<DataProvider.Entities.Enroll>();
                foreach (var item in ja)
                {
                    string classid = ((JObject)item)["classid"].ToString();//报名的班级ID
                    decimal payment = decimal.Parse(((JObject)item)["payment"].ToString());//本次付款
                    string apid = ((JObject)item)["apid"].ToString();//预约号
                    string studentid = ((JObject)item)["studentid"].ToString();//学员号
                    if (string.IsNullOrEmpty(((JObject)item)["classhour"].ToString()))
                    {
                        ajax.msg = "报名课时不能为空！";
                        return Json(ajax);
                    }
                    int classhour = int.Parse(((JObject)item)["classhour"].ToString());//报名课时
                    int discountid = int.Parse(((JObject)item)["discountid"].ToString() == "" ? "0" : ((JObject)item)["discountid"].ToString());//选择的优惠ID
                    decimal discountprice = decimal.Parse(((JObject)item)["discountprice"].ToString());//本次优惠的金额
                    string[] collectionrec = ((JObject)item)["collectionrec"].ToString().TrimEnd(',').Split(',');//自己明细，需要验证总和等于付款金额
                    decimal paymentrec = 0M;
                    foreach (string i in collectionrec)
                    {
                        if(!string.IsNullOrEmpty(i))
                        paymentrec += decimal.Parse(i);
                    }
                    if (payment != paymentrec)
                    {
                        ajax.msg = "资金明细与付款金额不同，请重新填写";
                        return Json(ajax);
                    }
                    if (string.IsNullOrEmpty(studentid))
                    {
                        ajax.msg = "不是正式学员不允许结算！清先绑定学员或者成为正式学员。";
                        return Json(ajax);
                    }
                    DataProvider.Entities.Enroll en = new DataProvider.Entities.Enroll();
                    
                    en.APID = apid;
                    en.StudentID = studentid;
                    en.ClassID = classid;
                    en.ClassHour = classhour;
                    en.UsedHour = 0;
                    en.Price = payment;
                    en.Paid = payment;
                    en.DiscountID = discountid;
                    en.DiscountPrice = discountprice;
                    en.CreateTime = DateTime.Now;
                    en.CreatorId = UserSession.userid;
                    en.CollectionRec = ((JObject)item)["collectionrec"].ToString().TrimEnd(',');
                    ENList.Add(en);
                }
                if (EnrollData.AddList(ENList))
                {

                    ajax.status = EnumAjaxStatus.Success;
                    ajax.msg = "结算成功，完成报名！";
                }
            }
            else//提交到审核
            {
                List<EnrollAudit> ENList = new List<EnrollAudit>();
                foreach (var item in ja)
                {
                    string classid = ((JObject)item)["classid"].ToString();//报名的班级ID
                    decimal payment = decimal.Parse(((JObject)item)["payment"].ToString());//报名的班级ID
                    string apid = ((JObject)item)["apid"].ToString();//预约号
                    string studentid = ((JObject)item)["studentid"].ToString();//学员号
                    int classhour = int.Parse(((JObject)item)["classhour"].ToString());//报名课时
                    int discountid = int.Parse(((JObject)item)["discountid"].ToString() == "" ? "0" : ((JObject)item)["discountid"].ToString());//选择的优惠ID
                    if(discountid != -1)
                    {
                        ajax.msg = "自定义优惠与普通优惠不允许同时存在！";
                        return Json(ajax);
                    }
                    decimal discountprice = decimal.Parse(((JObject)item)["discountprice"].ToString());//本次优惠的金额
                    //验证资金明细
                    string[] collectionrec = ((JObject)item)["collectionrec"].ToString().Split(',');//自己明细，需要验证总和等于付款金额
                    decimal paymentrec = 0M;
                    foreach (string i in collectionrec)
                    {
                        if (!string.IsNullOrEmpty(i))
                        paymentrec += decimal.Parse(i);
                    }
                    if (payment != paymentrec)
                    {
                        ajax.msg = "资金明细与付款金额不同，请重新填写";
                        return Json(ajax);
                    }

                    if (string.IsNullOrEmpty(studentid))
                    {
                        ajax.msg = "不是正式学员不允许结算！清先绑定学员或者成为正式学员。";
                        return Json(ajax);
                    }
                    EnrollAudit en = new EnrollAudit();
                    en.APID = apid;
                    en.StudentID = studentid;
                    en.ClassID = classid;
                    en.ClassHour = classhour;
                    en.UsedHour = 0;
                    en.Price = payment;
                    en.Paid = payment;
                    en.DiscountID = discountid;
                    en.DiscountPrice = discountprice;
                    en.StateID = 2;//待审核数据
                    en.CreateTime = DateTime.Now;
                    en.CreatorId = UserSession.userid;
                    en.CollectionRec = ((JObject)item)["collectionrec"].ToString().TrimEnd(',');
                    ENList.Add(en);
                }
                if (EnrollData.AddEnrollAuditList(ENList))
                {

                    ajax.status = EnumAjaxStatus.Success;
                    ajax.msg = "已提交审核！";
                }
            }
            
            return Json(ajax);
        }
        /// <summary>
        /// 查询试听记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult ListenList(EnrollListSearchModel search)
        {
            ListenListViewModel model = new ListenListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            search.TeachTypeID = 1;//授课方式是试听的
            model.ListenList = EnrollData.GeEnrollList(search); //填充页面模型数据
            return View(model);//返回页面模型
        }

    }
}
