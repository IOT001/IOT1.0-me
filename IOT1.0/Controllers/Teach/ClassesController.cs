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

            if (ClassesData.UpdateStudent(Clas))//注意时间类型，而且需要在前台把所有的值
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


            Clas.PresentEnroll = 0;//已报人数
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

            Classes Classes = (Classes)(JsonConvert.DeserializeObject(data.ToString(), typeof(Classes)));
            Date Date = (Date)(JsonConvert.DeserializeObject(data.ToString(), typeof(Date)));
            ClassList Clas = (ClassList)(JsonConvert.DeserializeObject(data.ToString(), typeof(ClassList)));
            var v= Date.Start_Date;

            Clas.StateID = 2;  //状态
            Clas.CreateTIme = DateTime.Now; //创建时间
            Clas.CreatorId = UserSession.userid; //创建人 

            if (ClassListData.AddClassList(Clas, Date,Classes))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }





    }
}
