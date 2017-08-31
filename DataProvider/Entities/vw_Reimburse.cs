using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    //报销表
    public class vw_Reimburse
	{
	
      	/// <summary>
		/// ID
        /// </summary>
        public   string ID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 教师姓名
        /// </summary>
        public string Teachername
        {
            get; 
            set; 
        }        
		/// <summary>
        /// Reimburse表老师ID
        /// </summary>
        public string TeacherID
        {
            get; 
            set; 
        }
        /// <summary>
        /// 状态ID
        /// </summary>
        public string StateID
        {
            get;
            set;
        }        
		/// <summary>
        /// 状态名称
        /// </summary>
        public string StateIDname
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 申请用途
        /// </summary>
        public string ApplyRemark
        {
            get; 
            set; 
        }        
	 
		/// <summary>
		/// CreateTime
        /// </summary>
        public   DateTime? CreateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreatorId
        /// </summary>
        public   string CreatorId
        {
            get; 
            set; 
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public Nullable<DateTime> AuditingTime
        {
            get;
            set;
        }
        /// <summary>
        ///  审核人ID
        /// </summary>
        public string AuditingID
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string AuditingIDname
        {
            get;
            set;
        }        
           	/// <summary>
        /// 审核备注
        /// </summary>
        public string AuditingRemark
        {
            get; 
            set; 
        }


        /// <summary>
        /// 申请退款的金额
        /// </summary>
        public decimal ? ApplyPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 财务支付，财务审核时，运行输入比申请金额小的值
        /// </summary>
        public decimal? CheckPrice
        {
            get;
            set;
        }   
		   

        

	}

    
}
