using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Teach
{
    public partial class TeachController : Controller
    { 


        /// <summary>
        /// 教师列表查询
        /// </summary>
        public ActionResult TimeList(DictionaryItemSearchModel search)
        {
            DictionaryItemViewModel model = new DictionaryItemViewModel();//页面模型
            model.search = search;//页面的搜索模型
            model.search.PageSize = 15;//每页显示
            model.search.CurrentPage = Convert.ToInt32(Request["pageindex"]) <= 0 ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页
            //时间段下拉框
            List<CommonEntity> DicItemID = CommonData.GetDictionaryList(8);//1是字典类型值,仅供测试参考
            model.DicItemNameIL = CommonData.Instance.GetBropDownListData(DicItemID);




            model.DictionaryItemlist = TimeData.GetDictionaryItemList(search);//填充页面模型数据
            return View(model);//返回页面模型
        }





        /// <summary>
        /// 新增时钟（字典表）专属于时段管理功能的
        /// </summary>
        /// <returns></returns>
        public JsonResult add_Time_save()
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
            int DicItemID = CommonData.Getnumber(8);//获取行号

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
        /// 删除
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonResult Time_DELETE()
        {
            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败

            ajax.msg = "删除失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化

            DictionaryItem DIC = (DictionaryItem)(JsonConvert.DeserializeObject(data.ToString(), typeof(DictionaryItem)));
            if (TimeData.UpdateDictionaryItem(DIC)>0)//注意时间类型，而且需要在前台把所有的值
            {
                ajax.msg = "删除成功！";
                ajax.status = EnumAjaxStatus.Success;
            }
            return Json(ajax);
        }



        
    }
}




