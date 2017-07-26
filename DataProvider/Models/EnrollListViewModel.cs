using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataProvider.Models
{
   public  class EnrollListViewModel
    {
       /// <summary>
       /// 页面的搜索条件
       /// </summary>
       public EnrollListSearchModel search = new EnrollListSearchModel();

       /// <summary>
       /// 页面的列表数据
       /// </summary>
       public PagedList<vw_Appointment> AppointmentList { get; set; }
       /// <summary>
       /// 跟进方式下拉框
       /// </summary>
       public List<SelectListItem> FollowTypeIL { get; set; }
       /// <summary>
       /// 意向分类下拉框
       /// </summary>
       public List<SelectListItem> IntentTypeIL { get; set; }

       /// <summary>
       /// 待添加的跟进纪录
       /// </summary>
       public FollowRecord FollowRecordForADD { get; set; }
       /// <summary>
       /// 试听查询模型
       /// </summary>
       public ClassesListSearchModel STSearch { get; set; }
       /// <summary>
       /// 授课方式
       /// </summary>
       public List<SelectListItem> SourceIL { get; set; }
    }

   public class EnrollListSearchModel : CommonPageEntity
   {
       /// <summary>
       /// 学员姓名
       /// </summary>
       public string ApName { set; get; }
       /// <summary>
       /// 联系电话
       /// </summary>
       public string ApTel { set; get; }

       /// <summary>
       /// 学号
       /// </summary>
       public string Enroll_StudentID { set; get; }

        /// <summary>
       /// 班级ID
       /// </summary>
       public string ClassID { set; get; }




       /// <summary>
       /// 下拉框按钮的选中值
       /// </summary>
       public int DicItemID { set; get; }
       /// <summary>
       /// 报名日期-开始时间
       /// </summary>
       public string timeStart { set; get; }
       /// <summary>
       /// 报名日期-结束时间
       /// </summary>
       public string timeEnd { set; get; }
       /// <summary>
       /// 授课方式，字典表类型5，1是试听，2集体，3一对一
       /// </summary>
       public int? TeachTypeID { set; get; }
       /// <summary>
       /// 正式学员姓名
       /// </summary>
       public string Name { set; get; }
       /// <summary>
       /// 正式学员绑定手机号
       /// </summary>
       public string BindPhone { set; get; }
   }

}
