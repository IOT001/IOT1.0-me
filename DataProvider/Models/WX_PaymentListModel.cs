using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
   public class WX_PaymentListModel
    {
       /// <summary>
       /// 学员报名记录
       /// </summary>
       public PagedList<vw_Enroll> StudentEnrollList { get; set; }
       /// <summary>
       /// 页面的搜索条件
       /// </summary>
       public EnrollListSearchModel search = new EnrollListSearchModel();
    }
}
