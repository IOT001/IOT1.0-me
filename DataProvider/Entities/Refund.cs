using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;


namespace DataProvider.Entities
{
    public class Refund
    {
        public Refund()
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
        public Nullable<DateTime> CreateTime { get; set; }
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
        public Nullable<DateTime> AuditingTime { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>				
        public string AuditingRemark { get; set; }
        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal ? ApplyPrice { get; set; }
        /// <summary>
        /// 财务最后支付，财务审核时，运行输入比申请金额小的值
        /// </summary>
        public decimal? CheckPrice { get; set; }
        /// <summary>
        /// 收款单据号
        /// </summary>				
        public string ReceiptNum { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>				
        public string DepositBank { get; set; }
        /// <summary>
        /// 账号
        /// </summary>				
        public string AccountNumber { get; set; }
    }

    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class RefundORMMapper : ClassMapper<Refund>
    {
        public RefundORMMapper()
        {
            base.Table("Refund");

            //Map(f => f.socketouts).Ignore();//设置忽略
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }

}
