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
    public class ClassesListViewModel
    {

        /// <summary>
        /// 页面查询模型
        /// </summary>
        public ClassesListSearchModel search = new ClassesListSearchModel();
        /// <summary>
        /// 页面的列表数据tudents
        /// </summary>
        public PagedList<vw_Classes> Classeslist { get; set; }

        /// <summary>
        /// 下拉框，授课方式
        /// </summary>
        public List<SelectListItem> SourceIL { get; set; }

        /// <summary>
        /// 下拉框，所属课程
        /// </summary>
        public List<SelectListItem> SourceIL1 { get; set; }

        /// <summary>
        /// 下拉框，当前讲师
        /// </summary>
        public List<SelectListItem> SourceIL2 { get; set; }



        /// <summary>
        /// 按钮下拉框，当前教室
        /// </summary>
        public List<SelectListItem> SourceIL3 { get; set; }

        /// <summary>
        /// 按钮下拉框，当前上课的时段
        /// </summary>
        public List<SelectListItem> SourceIL4 { get; set; }
        /// <summary>
        /// 升班用的班级下拉框
        /// </summary>
        public List<SelectListItem> UpgradeClassesIL { get; set; }
        /// <summary>
        /// 升班所选择的班级ID
        /// </summary>
        public int UpgradeClassID { get; set; }




        /// <summary>
        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; }

    }

    /// <summary>
    /// 班级查询模型对象
    /// </summary>
    public class ClassesListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///班级名称
        /// </summary>
        public string ClassName { set; get; }


         /// <summary>
        ///课程名称
        /// </summary>
        public string CourseID { set; get; }

        
        /// <summary>
        /// 开班开始时间 
        /// </summary>
        public string StartTime_start
        { set; get; }
         

       
        /// <summary>
        /// 开班结束时间
        /// </summary>
        public string StartTime_end
        { set; get; }


        /// <summary>
        /// 结班开始时间 
        /// </summary>
        public string EndTime_start
        { set; get; }



        /// <summary>
        /// 结班结束时间
        /// </summary>
        public string EndTime_end
        { set; get; }


        /// <summary>
        /// 当前讲师
        /// </summary>
        public string TeacherID
        { set; get; }




         
         /// <summary>
        /// 授课方式，字典表类型5，1是试听，2集体，3一对一
        /// </summary>
        public int? TeachTypeID
        { set; get; }
        

           /// <summary>
        /// 课程
        /// </summary>
        public int CourseIDS
        { set; get; }


        /// <summary>
        /// 课程
        /// </summary>
        public int RoomID
        { set; get; }
        
        /// <summary>
        /// 上课的时段
        /// </summary>
        public string TimePeriod
        { set; get; }

        /// <summary>
        /// 是否查询试听
        /// </summary>
        public string islisten { set; get; }


        /// <summary>
        /// 所属分校下拉
        /// </summary>
        public List<SelectListItem> ComCodeIL { get; set; }

        /// <summary>
        /// 所属分校的选中值
        /// </summary>
        public string ComCode { get; set; }


    }

}



