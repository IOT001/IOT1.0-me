using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using IOT1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.WeiXin
{
    public partial class WeixinController : Controller
    {

 


        /// <summary>
        /// 查询ClasshourDetailedlist页面详细记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ClasshourDetailedlist(string StudentID, string ClassID)
        {
            WX_ClasshourDetailedlistModel model = new WX_ClasshourDetailedlistModel();
            ClasshourDetailedSearchModel search = new ClasshourDetailedSearchModel();
            search.StudentID = StudentID;
            search.ClassID = ClassID;
            model.ClasshourDetailedlist = ClasshourDetailedlistData.ClasshourDetailedlist(search);//查询学员课程表
            
            return View(model);
        }


 



    }
}
