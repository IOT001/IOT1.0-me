﻿using DataProvider;
using DataProvider.Data;
using DataProvider.Entities;
using DataProvider.Models;
using IOT1._0.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT1._0.Controllers.Enroll
{
    public partial class EnrollController : Controller
    {


        public ActionResult Import()
        {
            return View();
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns> 
        public JsonResult Upload()
        {

            AjaxStatusModel ajax = new AjaxStatusModel();//功能操作类的返回类型都是AjaxStatusModel，数据放到AjaxStatusModel.data中，前台获取json后加载
            ajax.status = EnumAjaxStatus.Error;//默认失败
            ajax.msg = "导入失败！";//前台获取，用于显示提示信息
            var data = Request["data"];//获取前台传递的数据，主要序列化
            if (string.IsNullOrEmpty(data))
            {
                return Json(ajax);
            }
             JObject jsonObj = JObject.Parse(data);
             var filename = jsonObj["filename"].ToString();
           

             var fileExt = jsonObj["fileExt"].ToString();
             DataTable dt = GetData(filename, fileExt).Tables[0];
            

             string ApName = dt.Columns["学员姓名"].ToString(); 
             if (string.IsNullOrEmpty(ApName))
             {
                 ajax.msg = "列学员姓名不存在！";
                 return Json(ajax);
             }
             string ApTel = dt.Columns["绑定的手机"].ToString();
             if (string.IsNullOrEmpty(ApTel))
             {
                 ajax.msg = "列绑定的手机不存在！";
                 return Json(ajax);
             }
             string ComCode = dt.Columns["所属分校"].ToString();//所属分校,1舜浦，2吾悦
             if (string.IsNullOrEmpty(ComCode))
             {
                 ajax.msg = "列所属分校不存在！";
                 return Json(ajax);
             }
             string APRemark = dt.Columns["备注"].ToString();
             if (string.IsNullOrEmpty(APRemark))
             {
                 ajax.msg = "列备注不存在！";
                 return Json(ajax);
             }

             Appointment app = new Appointment();
             app.ApStateID = 0;//初始状态默认0
             app.CreateTime = DateTime.Now; //创建时间
             app.CreatorId = UserSession.userid;//创建人
             foreach (DataRow dr in dt.Rows)
             {
                 app.ApName = dr["学员姓名"].ToString();
                 app.ApTel = dr["绑定的手机"].ToString();
                 if (dr["所属分校"].ToString() == "舜浦")
                 {//所属分校,1舜浦，2吾悦
                     app.ComCode = "1";
                 }
                 else if (dr["所属分校"].ToString() == "吾悦")
                 {
                     app.ComCode = "2";
                 }
                 app.ID = CommonData.DPGetTableMaxId("AP", "ID", "Appointment", 8);
                 app.APRemark = dr["备注"].ToString();
                 AppointmentData.Add(app);
             }


            

            ajax.msg = "导入成功！";
            ajax.status = EnumAjaxStatus.Success;
            //ajax.data = ret;
            return Json(ajax);

        }

        public DataSet GetData(string path, string fileSuffix)
        {


            if (string.IsNullOrEmpty(fileSuffix))

                return null;


            using (DataSet ds = new DataSet())
            {

                //判断Excel文件是2003版本还是2007版本

                string connString = "";

                if (fileSuffix == ".xls")

                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";

                else

                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

                //读取文件

                string sql_select = " SELECT * FROM [Sheet1$]";

                using (OleDbConnection conn = new OleDbConnection(connString))

                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {

                    conn.Open();

                    cmd.Fill(ds);

                }

                if (ds == null || ds.Tables.Count <= 0) return null;

                return ds;

            }

        }




    }
}
