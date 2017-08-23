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

            List<string> roles = UserSession.roles;//取账号角色
            search.isnull = roles;

             
            model.Fileslist = FileManageListData.GetFileseList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }




        /// <summary>
        /// 新增文件
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



            List<string> roles = UserSession.roles;//取账号角色
            //判断是否添加了管理员和校长权限。添加了才可以新增
            int isnull = 0;
            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i] == "1" || roles[i] == "4")
                {
                    isnull = 1;
                }
            }
            if (isnull != 1)
            {
                ajax.status = EnumAjaxStatus.Success;
                ajax.msg = "不是管理员权限,不能上传文件！";
                return Json(ajax);
            }



            Files files = (Files)(JsonConvert.DeserializeObject(data.ToString(), typeof(Files)));
            if (!string.IsNullOrWhiteSpace(files.ToRoles))//判断是否是空值
            {

                var ToRolese = files.ToRoles.TrimEnd(',');//因为获取的值最后有一个，所以最后的，去掉
                string[] ToRoles = ToRolese.Split(',');//根据,号分割
                string ROLE_Names = "";
                foreach (var item in ToRoles)
                {
                    //多沟选框获取中文信息
                    var SourceIL = CommonData.GetSYS_SystemRoleList_ROLE_Id(int.Parse(item));//根据ID获取中文名称
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
          //  var fileExt = jsonObj["fileExt"].ToString();//文件后缀

            string FileName_Format = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + FileName;

            Picture.DPSaveOrderFile(FileName_Format, "Files", fileTemp);


            files.FileName = FileName_Format;
            if (FileManageListData.AddFiles(files) > 0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "新增成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }

        




    }
}
