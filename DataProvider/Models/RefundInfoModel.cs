using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class RefundInfoModel 
    {
       

       /// <summary>
       /// 学员报名单条记录
       /// </summary>
        public vw_Enroll vw_EnrollList { get; set; }

        /// <summary>
        /// 学员退款申请单条记录
        /// </summary>
        public vw_Refund  RefundList { get; set; }


        /// <summary>
        /// 页面类型
        /// </summary>
        public int typeID { set; get; }
       
    }
     

}
