//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Enroll
    {   
        /// <summary>
        /// 报名单号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 预约号，资源号
        /// </summary>
        public string APID { get; set; }
        /// <summary>
        /// 学员ID
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        ///  班级ID
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 学员报名课时
        /// </summary>
        public decimal ClassHour { get; set; }
        /// <summary>
        /// 已消耗课时
        /// </summary>
        public decimal UsedHour { get; set; }
        /// <summary>
        /// 应缴费用
        /// </summary>
        public Nullable<decimal> Price { get; set; }
        /// <summary>
        /// 已付费用
        /// </summary>
        public Nullable<decimal> Paid { get; set; }
        /// <summary>
        /// 优惠ID
        /// </summary>
        public string DiscountID { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public Nullable<decimal> DiscountPrice { get; set; }
        public string CreatorId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string UpdatorId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeletorId { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public string Remark { get; set; }
        public int StateID { get; set; }

        /// <summary>
        /// 收款记录，用逗号隔开，1现金，2pos，3微信，4支付宝，5扣卡
        /// </summary>
        public string CollectionRec { get; set; }

        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedTime { get; set; }
        public string ApprovedRemark { get; set; }
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学员出生日期
        /// </summary>
        public Nullable<System.DateTime> Birthday { get; set; }
        /// <summary>
        /// 学员绑定手机号
        /// </summary>
        public string BindPhone { get; set; }
        /// <summary>
        /// 校区ID
        /// </summary>
        public string ComCode { get; set; }
        /// <summary>
        /// 校区名称
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 开课时间，试听课上课时间
        /// </summary>
        public DateTime? ClassStartTime { get; set; }
        /// <summary>
        /// 授课方式，字典表类型5，1是试听，2集体，3一对一
        /// </summary>
        public int TeachTypeID { get; set; }
        /// <summary>
        /// 预约人姓名
        /// </summary>
        public string ApName { get; set; }
        /// <summary>
        /// 预约人姓名
        /// </summary>
        public string ApTel { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public decimal RemainClassHour { get; set; }
        /// <summary>
        /// 授课方式中文
        /// </summary>
        public string TeachTypeName { get; set; }
        /// <summary>
        /// 报名状态
        /// </summary>
        public string StateName { get; set; }
    }
}
