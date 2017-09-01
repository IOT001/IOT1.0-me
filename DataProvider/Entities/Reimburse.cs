using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    //报销表
    public class Reimburse
	{
	
      	/// <summary>
		/// ID
        /// </summary>
        public   int ID
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
        public Nullable<decimal>  ApplyPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 财务支付，财务审核时，运行输入比申请金额小的值
        /// </summary>
        public Nullable<decimal> CheckPrice
        {
            get;
            set;
        }   
		   

        

	}


    /// <summary>
    /// Deploy：实体对象映射关系
    /// </summary>
    [Serializable]
    public sealed class ReimburseORMMapper : ClassMapper<Reimburse>
    {
        public ReimburseORMMapper()
        {
            base.Table("Reimburse");
             
            Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }


    
}
