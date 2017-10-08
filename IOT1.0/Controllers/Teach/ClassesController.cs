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

namespace IOT1._0.Controllers.Teach
{
    public partial class TeachController : Controller
    {



        /// <summary>
        /// 班级列表查询
        /// </summary>
        public ActionResult Classes(ClassesListSearchModel search)
        {
            ClassesListViewModel model = new ClassesListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

            

            //下拉项
            List<CommonEntity> SourceIL = CommonData.GetDictionaryList(5);//授课方式
            model.SourceIL = CommonData.Instance.GetBropDownListData(SourceIL);

            //下拉项
            List<CommonEntity> SourceIL1 = CommonData.GetCourseList();//课程
            model.SourceIL1 = CommonData.Instance.GetBropDownListData(SourceIL1);

            //下拉项
            List<CommonEntity> SourceIL2 = CommonData.GetTeachersList();//获取老师信息
            model.SourceIL2 = CommonData.Instance.GetBropDownListData(SourceIL2);


            //下拉项
            List<CommonEntity> SourceIL3 = CommonData.GetDictionaryList(11);//获取教室信息
            model.SourceIL3 = CommonData.Instance.GetBropDownListData(SourceIL3);


            //下拉项
            List<CommonEntity> SourceIL4 = CommonData.GetDictionaryList(8);//获取上课的时段信息
            model.SourceIL4 = CommonData.Instance.GetBropDownListData(SourceIL4);
            //获取班级列表下拉框
            List<CommonEntity> UpgradeClassesIL = CommonData.GetClassesItemList();
            model.UpgradeClassesIL = CommonData.Instance.GetBropDownListData(UpgradeClassesIL);

            model.Classeslist = ClassesData.GeClassesList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }





        /// <summary>
        /// 根据班级号ID获取详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetClassesByID(string id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Classes Clas = ClassesData.GetClassesByID(id);//业务层获取底层方法，返回数据
            if (Clas != null)
            {
                ajax.data = Clas;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }


        /// <summary>
        /// 保存编辑班级表
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveClasses()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Classes Clas = (Classes)(JsonConvert.DeserializeObject(data.ToString(), typeof(Classes)));

             

            Clas.UpdateTime = DateTime.Now;
            Clas.UpdatorId = UserSession.userid;

            if (ClassesData.Update(Clas))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }


        /// <summary>
        /// 新增班级表
        /// </summary>
        /// <returns></returns>
        public JsonResult AddClasses()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Classes Clas = (Classes)(JsonConvert.DeserializeObject(data.ToString(), typeof(Classes)));


            Classes query = ClassesData.GetClassesList();//获取创建时间最新的一条数据
            string MAX_ID;
            if (query != null)
            {
                MAX_ID = query.ID;
            }
            else
            {
                MAX_ID = null;
            }

            var year = DateTime.Now.Year.ToString();

            if (!string.IsNullOrEmpty(MAX_ID))
            {
                Clas.ID ="CL"+ year + (Convert.ToInt32(MAX_ID.Substring(6)) + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                Clas.ID ="CL"+ year + "0001";
            }


            Clas.PresentLesson = 0;//已上课时
            Clas.StateID = 1;  //状态
            Clas.CreateTime = DateTime.Now; //创建时间
            Clas.CreatorId = UserSession.userid; //创建人
     

            if (ClassesData.AddStudent(Clas) != "")//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        

             /// <summary>
        /// 新增时钟（字典表）
        /// </summary>
        /// <returns></returns>
        public JsonResult addTime_save()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Date Date = (Date)(JsonConvert.DeserializeObject(data.ToString(), typeof(Date)));

            DictionaryItem item = new DictionaryItem(); 
             int  DicItemID= CommonData.Getnumber(8);//获取行号
             
            item.DicTypeID = 8;//时钟类别
            item.DicItemID = DicItemID + 1;//行号
            item.CreateTime = DateTime.Now;//创建时间
            item.Sort = 0;//是否启用类别
            item.recordState = 0;//状态
            item.DicItemName = Date.addtime_start + "-" + Date.addtime_End;         //值
           
            if (ClassListData.AddDictionaryItemt(item))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";

          

                JObject jsonObj = new JObject();
                jsonObj.Add("DicItemID", item.DicItemID);
                jsonObj.Add("DicItemName", item.DicItemName);

                string aa = JsonConvert.SerializeObject(jsonObj);//序列化，不序列化前台获取不到值

                ajax.data = aa; //返回新增加的时钟
                ajax.status = EnumAjaxStatus.Success;
               
            }
          
            return Json(ajax);
        }


        


        /// <summary>
        /// 新增排班表 ClassList
        /// </summary>
        /// <returns></returns>
        public JsonResult ClassListSave()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            //var TIme = Request["Date"];//获取前台传递的数据，主要序列化
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }

            Weekday Weekday = (Weekday)(JsonConvert.DeserializeObject(data.ToString(), typeof(Weekday))); //星期
            Classes Classes = (Classes)(JsonConvert.DeserializeObject(data.ToString(), typeof(Classes))); //班级
            Date Date = (Date)(JsonConvert.DeserializeObject(data.ToString(), typeof(Date)));//时间
            ClassList Clas = (ClassList)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassList)));//排课表
            var v= Date.Start_Date;

            Clas.StateID = 1;  //状态
            Clas.CreateTIme = DateTime.Now; //创建时间
            Clas.CreatorId = UserSession.userid; //创建人 

            if (ClassListData.AddClassList(Clas, Date,Classes,Weekday))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }



        /// <summary>
        ///  根据班级信息获取学员的报名信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetClassesByClassID()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            EnrollListSearchModel search = new EnrollListSearchModel();
            string Enroll_Name = Request["Enroll_Name"];
            string Enroll_StudentID = Request["Enroll_StudentID"];
            string ClassID = Request["ClassID"];
            search.ClassID = ClassID;
            if (!string.IsNullOrEmpty(Enroll_Name))
                search.ApName = Enroll_Name;
            if (!string.IsNullOrEmpty(Enroll_StudentID))
                search.Enroll_StudentID = Enroll_StudentID;

            search.CurrentPage = 1;//当前页
            search.PageSize = 15;//不想分页就设置成一个较大的值,比如99999
            List<vw_Enroll> vw_Enroll = EnrollData.GeEnrollList(search);
            ajax.data = vw_Enroll;
            return Json(new { total = 1, rows = vw_Enroll, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }





        /// <summary>
        ///  按条件查询试听课
        /// </summary>
        /// <returns></returns>
        public ActionResult GeClass_transfe_List()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            ClassesListSearchModel search = new ClassesListSearchModel();//页面模型   
            string ClassName = Request["ClassName"];
            if (!string.IsNullOrEmpty(ClassName))
                search.ClassName = ClassName;
            search.CurrentPage = 1;//当前页
            search.PageSize = 15;//不想分页就设置成一个较大的值,比如99999 
            List<vw_Classes> vw_Enroll = ClassesData.GeClass_transfe_List(search);
            ajax.data = vw_Enroll;
            return Json(new { total = 1, rows = vw_Enroll, state = true, msg = "加载成功" }, JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// 保存编辑转班
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveClass_transfe()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            string Classes_ID = Request["Classes_ID"];//班级ID，班级号
            string Enroll_ID = Request["Enroll_ID"];//报名表主键
            string Enroll_StudentID = Request["Enroll_StudentID"];//报名表学员号
            string Enroll_ClassID = Request["Enroll_ClassID"];//报名表班级ID
            if (string.IsNullOrEmpty(Classes_ID))
            {
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(Enroll_ID))
            {
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(Enroll_StudentID))
            {
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(Enroll_ClassID))
            {
                return Json(ajax);
            }
           
            DataProvider.Entities.Enroll en = new DataProvider.Entities.Enroll();
            ClassesTrans ct = new ClassesTrans();

            en.ClassID = Classes_ID;
            en.ID = Enroll_ID;
            en.UpdateTime = DateTime.Now;
            en.UpdatorId = UserSession.userid;


            ct.CreateTime = DateTime.Now;
            ct.CreatorId = UserSession.userid;
            ct.StudentID = Enroll_StudentID;
            ct.ClassFrom = Enroll_ClassID;
            ct.ClassTo = Classes_ID;
            if (ClassesData.SaveClass_transfe(en,ct))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        /// <summary>
        /// 升班的操作，将原来的报名记录的剩余课重新计算，然后修改报名记录的班级ID，然后生成转班记录记录当时发生情况,然后还要添加差价记录
        /// </summary>
        /// <returns></returns>
        public ActionResult UpgradeClass()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            string oldclassid = Request["oldclassid"]; //旧的班级号
            string newclassid = Request["newclassid"];//新的班级号
            string did = Request["did"];//升班的学员记录
            JArray ja = (JArray)JsonConvert.DeserializeObject(did);
            if (ClassesData.UpClass(oldclassid, newclassid, ja, UserSession.userid))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "升班成功！";
            }
            return Json(ajax);
        }


    }
}
