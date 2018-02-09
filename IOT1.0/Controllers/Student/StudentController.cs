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
    public class StudentController : Controller
    {

        //
        // GET: /ButtonList/
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Student(StudentListSearchModel search)
        {

            StudentListViewModel model = new StudentListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页

            //下拉项
            List<CommonEntity> SourceIL = CommonData.GetDictionaryList(2);//1是字典类型值,仅供测试参考
            model.SourceIL = CommonData.Instance.GetBropDownListData(SourceIL);


            //分校下拉项
            List<CommonEntity> ComCodeIL = CommonData.Get_SYS_Company_List();//分校
            model.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);
            model.search.ComCodeIL = CommonData.Instance.GetBropDownListData(ComCodeIL);


            //默认选择获取 的值。。
            //string ComCode = TeacherData.GetTeachers_ComCode(UserSession.userid);
            //ViewData["ComCode"] = ComCodeIL.AsEnumerable();
             



            model.Studentlist = StudentData.GetStudentList(search);//填充页面模型数据
            return View(model);//返回页面模型


        }
        /// <summary>
        /// 根据学号ID获取详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetStudentByID(string id)
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "获取失败！";//前台获取，用于显示提示信息
            Students btn = StudentData.GetStudentsByID(id);//业务层获取底层方法，返回数据
            if (btn != null)
            {
                ajax.data = btn;//放入数据
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "获取成功！";
            }
            return Json(ajax);

        }


        /// <summary>
        /// 保存编辑学生
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveStudent()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Students Stu = (Students)(JsonConvert.DeserializeObject(data.ToString(), typeof(Students)));

            //判断手机号码是否唯一

            //int BindPhone_count = StudentData.BindPhone_update(Stu.ID, Stu.BindPhone);
            //if (BindPhone_count > 0)
            //{
            //    ajax.msg = "手机号码已存在，不能重复使用！";
            //    return Json(ajax);
            //}


            Stu.UpdateTime = DateTime.Now;
            Stu.UpdatorId = UserSession.userid;



            if (StudentData.UpdateStudent(Stu))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }


        /// <summary>
        /// 新增学生表
        /// </summary>
        /// <returns></returns>
        public JsonResult AddStudent()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }

            Students Stu = (Students)(JsonConvert.DeserializeObject(data.ToString(), typeof(Students)));

            //判断手机号码+学员号是否唯一


            string studid = StudentData.BindPhone_insert(Stu.BindPhone,Stu.Name);
            string apid = Request["apid"];//预约号
            if (!string.IsNullOrEmpty(apid))//如果是从预约/市场资源模块进来的，先判断是否存在学员，如果存在就绑定，如果不存在就新增记录    
            {
                if (!string.IsNullOrEmpty(studid))//之前已经存在学员，则做绑定操作
                {
                    StudentData.BindStudentforAP(studid, apid);
                    ajax.status = EnumAjaxStatus.Success;
                    ajax.msg = "已成功绑定到学员！";
                    return Json(ajax);
                }
            }
            if (!string.IsNullOrEmpty(studid))
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "手机号码已存在，不能重复使用！";
                return Json(ajax);
            }
            Stu.StateID = 1;//新增学员状态默认是未读。
            Stu.CreateTime = DateTime.Now;
            Stu.CreatorId = UserSession.userid;



            var year = DateTime.Now.Year.ToString();//获取年份
            var month = DateTime.Now.Month.ToString();//获取月份 
            if (month.ToString().Length == 1)
            {
                month = "0" + month.ToString();
            } 
            var Mosaic = year + month;



            Students query = StudentData.GetStudentsOne(Mosaic);//获取创建时间最新的一条数据
            string MAX_ID;
            if (query != null)
            {
                MAX_ID = query.ID;
            }
            else
            {
                MAX_ID = null;
            }
 
            if (!string.IsNullOrEmpty(MAX_ID))
            {
                Stu.ID = Mosaic + (Convert.ToInt32(MAX_ID.Substring(4)) + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                Stu.ID = Mosaic + "01";
            }
            SYSAccount sys = new SYSAccount();//用户信息
            SYSAccountRole sysR = new SYSAccountRole();//用户角色
            RandomOperate operate = new RandomOperate();
            //添加默认权限
            Stu.BindAccount = Stu.ID;//方便登陆
            sys.ACC_Account = Stu.BindAccount;
            sys.ACC_CreatedBy = UserSession.userid;
            sys.ACC_CreatedOn = DateTime.Now;
            sys.ACC_Password = operate.CreateMD5Hash("123");


            //添加SYS_AccountRole表数据
             
           // sysR.AR_AccountId = StudentData.Max_ACC_Id();
            sysR.AR_SystemRoleId = 9;


            string stuid = StudentData.AddStudent(Stu, sys, sysR);
            if (stuid != "")//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                if (!string.IsNullOrEmpty(apid))//如果是预约单过来的，重新绑定一下
                {
                    StudentData.BindStudentforAP(stuid, apid);
                    ajax.msg = "绑定成功！";
                }
                
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }







        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns> 
        public JsonResult Upload()
        {

            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            JObject jsonObj = JObject.Parse(data);
            Picture Picture = new Picture();
            var ret = Picture.DPUpLoadPhoto(jsonObj);              //.DPUpLoadPhotos(json);
            ajax.msg = "上传成功！";
            ajax.status = EnumAjaxStatus.Success;
            ajax.data = ret;
            return Json(ajax);

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
           
            Students Stu = (Students)(JsonConvert.DeserializeObject(data.ToString(), typeof(Students)));  
            Stu.UpdateTime = DateTime.Now;
            Stu.UpdatorId = UserSession.userid; 

            if (StudentData.UpdateStudent(Stu))//注意时间类型，而且需要在前台把所有的值
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

            Students Stu = (Students)(JsonConvert.DeserializeObject(data.ToString(), typeof(Students)));
            Stu.UpdateTime = DateTime.Now;
            Stu.UpdatorId = UserSession.userid; 
            if (StudentData.UpdateStudent(Stu))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "冻结成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }





    }
}
