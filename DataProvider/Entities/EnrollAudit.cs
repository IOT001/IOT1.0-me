using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class EnrollAudit
    {
        public EnrollAudit()
        {
        }
        /// <summary>
        /// 报名审核ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// APID
        /// </summary>				
        public string APID { get; set; }
        /// <summary>
        /// StudentID
        /// </summary>				
        public string StudentID { get; set; }
        /// <summary>
        /// ClassID
        /// </summary>				
        public string ClassID { get; set; }
        /// <summary>
        /// ClassHour
        /// </summary>				
        public decimal ClassHour { get; set; }
        /// <summary>
        /// UsedHour
        /// </summary>				
        public decimal UsedHour { get; set; }
        /// <summary>
        /// Price
        /// </summary>				
        public decimal Price { get; set; }
        /// <summary>
        /// Paid
        /// </summary>				
        public decimal Paid { get; set; }
        /// <summary>
        /// 优惠ID
        /// </summary>				
        public int DiscountID { get; set; }
        /// <summary>
        /// 优惠的金额，如果是自定义优惠则就是申请的优惠金额
        /// </summary>				
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// UpdatorId
        /// </summary>				
        public string UpdatorId { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>				
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// DeletorId
        /// </summary>				
        public string DeletorId { get; set; }
        /// <summary>
        /// DeleteTime
        /// </summary>				
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// Remark
        /// </summary>				
        public string Remark { get; set; }
        /// <summary>
        /// 状态，对应字典表19，1无需审核，2待审核，3审核通过，4审核不通过
        /// </summary>				
        public int StateID { get; set; }
        /// <summary>
        /// ApprovedBy
        /// </summary>				
        public string ApprovedBy { get; set; }
        /// <summary>
        /// ApprovedTime
        /// </summary>				
        public DateTime? ApprovedTime { get; set; }
        /// <summary>
        /// ApprovedRemark
        /// </summary>				
        public string ApprovedRemark { get; set; }
    }
}
