using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class Enroll
    {
        public Enroll()
        {


        }
        /// <summary>
        /// ID
        /// </summary>				
        public string ID { get; set; }
        /// <summary>
        /// 预约单号
        /// </summary>				
        public string APID { get; set; }
        /// <summary>
        /// 学员号
        /// </summary>				
        public string StudentID { get; set; }
        /// <summary>
        /// 班级号
        /// </summary>				
        public string ClassID { get; set; }
        /// <summary>
        /// 学员报名学时
        /// </summary>				
        public decimal ClassHour { get; set; }
        /// <summary>
        /// 已消耗课时
        /// </summary>				
        public decimal UsedHour { get; set; }
        /// <summary>
        /// 应缴费金额
        /// </summary>				
        public decimal Price { get; set; }
        /// <summary>
        /// 已交费
        /// </summary>				
        public decimal Paid { get; set; }
        /// <summary>
        /// DiscountID
        /// </summary>				
        public int DiscountID { get; set; }
        /// <summary>
        /// DiscountPrice
        /// </summary>				
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public Nullable<System.DateTime> CreateTime { get; set; }
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
        /// 状态
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

        /// <summary>
        /// 收款记录，用逗号隔开，1现金，2pos，3微信，4支付宝，5扣卡
        /// </summary>
        public string CollectionRec { get; set; }
        /// <summary>
        /// 升班产生的差价
        /// </summary>
        public decimal UpPrice { get; set; }
    }
}
