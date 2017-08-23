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

namespace IOT1._0.Controllers.Office
{
    public partial class OfficeController : Controller
    {
        //
        // GET: /FileManage/

        public ActionResult FileManageList(FileManageListSearchModel search)
        {
            FileManageListViewModel model = new FileManageListViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页 

            //多沟选框
            List<DataProvider.Data.CommonData.SYS_Role> SourceIL = CommonData.GetSYS_SystemRoleList(3);
            ViewData["SYS_Role"] = SourceIL;

            model.Fileslist = FileManageListData.GetFileseList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }




        /// <summary>
        /// 新增留言
        /// </summary>
        /// <returns></returns>
        public JsonResult AddFiles()
        {


            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "新增失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            Files files = (Files)(JsonConvert.DeserializeObject(data.ToString(), typeof(Files)));
            if (!string.IsNullOrWhiteSpace(files.ToRoles))
            {

                var ToRolese = files.ToRoles.TrimEnd(',');
                string[] ToRoles = ToRolese.Split(',');
                string ROLE_Names = "";
                foreach (var item in ToRoles)
                {
                    //多沟选框获取中文信息
                    var SourceIL = CommonData.GetSYS_SystemRoleList_ROLE_Id(int.Parse(item));
                    ROLE_Names += SourceIL[0] + ",";
                }
                var ROLE_Name = ROLE_Names.TrimEnd(',');//去除最后的一个逗号
                files.ToRolesName = ROLE_Name;//赋值给名称（方便查询，存的中文，比如人事,财务，市场） 
            }

            files.CreateTime = DateTime.Now; //创建时间
            files.CreatorId = UserSession.userid;//创建人

            var Files = Request["Files"];//获取前台传递的数据，主要序列化
            JObject jsonObj = JObject.Parse(Files);
            var fileTemp = jsonObj["fileTemp"].ToString(); //文件内容
            var FileName = jsonObj["FileName"].ToString(); //文件名称
            var fileExt = jsonObj["fileExt"].ToString();//文件后缀


            Picture.DPSaveOrderFile(string.Format(FileName + "{0}.xls", string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now)), "Files", fileTemp);

            if (FileManageListData.AddFiles(files) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        




    }
}
