using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class vw_Refund
    {
        public vw_Refund()
        {
        }
        /// <summary>
        /// ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// 学员ID
        /// </summary>				
        public string StudentID { get; set; }
        /// <summary>
        /// 报名ID
        /// </summary>				
        public string EnrollID { get; set; }
        /// <summary>
        /// 审核状态，取字典表类型20，1待审核，2审核通过，3审核不通过
        /// </summary>				
        public int StateID { get; set; }
        /// <summary>
        /// 备注，退款理由，申请备注
        /// </summary>				
        public string ApplyRemark { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人，申请人
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>				
        public string AuditingID { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>				
        public DateTime AuditingTime { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>				
        public string AuditingRemark { get; set; }
        /// <summary>
        /// 申请人中文
        /// </summary>				
        public string CreateName { get; set; }
        /// <summary>
        /// 审核人中文
        /// </summary>				
        public string AuditingName { get; set; }
        /// <summary>
        /// 学生中文
        /// </summary>				
        public string SutdentName { get; set; }
        /// <summary>
        /// 状态中文
        /// </summary>				
        public string StateName { get; set; }
        /// <summary>
        /// 学员绑定手机号
        /// </summary>
        public string BindPhone { get; set; }
        /// <summary>
        /// 申请退款的金额
        /// </summary>
        public decimal ApplyPrice { get; set; }
       /// <summary>
        /// 财务最后支付，财务审核时，运行输入比申请金额小的值
       /// </summary>
        public decimal? CheckPrice { get; set; }

    }
}
