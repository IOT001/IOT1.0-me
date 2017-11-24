using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Finance
{
    public partial class FinanceController : Controller
    {


        public ActionResult RefundCheckList(RefundCheckListSearchModel search)
        {
            RefundCheckListModel model = new RefundCheckListModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //search.StateID = "1";
            model.RefundlList = RefundCheckData.GetRefundList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }


      

        /// <summary>
        /// 导出到Excel的操作
        /// </summary>
        /// <param name="json"></param>

        //public void ExportToExcel(string Name, string BindPhone, string timeStart, string timeEnd)
        //{

        //    DataTable datasource = DailyReportData.DPExportToExcel(Name, BindPhone, timeStart, timeEnd);
        //    ERP.Models.MyNPOIModel.ExportByWeb(datasource, "退款审核", "退款审核" + ".xls");

        //}



    }
}
