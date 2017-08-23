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
    public class FileManageListViewModel
    {


        public long[] School { get; set; } //多选框
        /// <summary>
        /// 页面查询模型
        /// </summary>
        public FileManageListSearchModel search = new FileManageListSearchModel();
        /// <summary>
        /// 页面的列表数据
        /// </summary>
        public PagedList<Files> Fileslist { get; set; }
 
          /// <summary>
        /// 多选框的数据源
        /// </summary>
        public List<SelectListItem> SYS_SystemRoleIL { get; set; } 
        
    }

    /// <summary>
    /// 按钮查询模型对象
    /// </summary>
    public class FileManageListSearchModel : CommonPageEntity
    {
        /// <summary>
        ///文件名称
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        ///文件标题
        /// </summary>
        public string FileTitle { set; get; }

    }

}



