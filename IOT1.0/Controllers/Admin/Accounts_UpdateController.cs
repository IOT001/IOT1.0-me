using CCSH.DataProvider.SqlServer;
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

namespace IOT1._0.Controllers.Admin
{

    public partial class AdminController : Controller
    {
        //
        // GET: /Accounts_Update/

        public ActionResult Accounts_Update()
        {
            Accounts_UpdateViewModel model = new Accounts_UpdateViewModel();
            string ACC_Account = UserSession.userid;
            
            model.SYS_AccountList = Accounts_UpdateData.GetAccounts_Update(ACC_Account);
            model.ACC_id = model.SYS_AccountList.FirstOrDefault().ACC_Id;
            return View(model);
        }


        /// <summary>
        /// 保存编辑账号
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult SaveSYS_Account()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "保存失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
            SYSAccount sys = (SYSAccount)(JsonConvert.DeserializeObject(data.ToString(), typeof(SYSAccount)));
            sys.ACC_Password = StringHelper.CreateMD5(sys.ACC_Password);//md5加密
             
            if (Accounts_UpdateData.UpdateSYS_Account(sys))//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "保存成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }


    }
}
