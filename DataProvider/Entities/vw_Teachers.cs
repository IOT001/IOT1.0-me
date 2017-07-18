using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   
    //教师表
    public class vw_Teachers
	{
	
      	/// <summary>
		/// 教师号
        /// </summary>
        public   string ID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 教师姓名
        /// </summary>
        public   string name
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 性别，字典 1：男  2：女
        /// </summary>
        public   int sex
        {
            get; 
            set; 
        }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string sexName
        {
            get;
            set;
        }        
		/// <summary>
		/// MobilePhone
        /// </summary>
        public   string MobilePhone
        {
            get; 
            set; 
        }        
		/// <summary>
		/// EntryDate
        /// </summary>
        public   DateTime? EntryDate
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 离职日期
        /// </summary>
        public   DateTime? LeaveDate
        {
            get; 
            set; 
        }

        /// <summary>
        /// 是否离职
        /// </summary>
        public string LeaveDateName
        {
            get;
            set;
        }        
		/// <summary>
		/// 邮箱
        /// </summary>
        public   string Email
        {
            get; 
            set; 
        }        
		/// <summary>
		/// WeChat
        /// </summary>
        public   string WeChat
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 身份证号
        /// </summary>
        public   string IDNumber
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 联系地址
        /// </summary>
        public   string ContactAddress
        {
            get; 
            set; 
        }        
		/// <summary>
		/// 备注
        /// </summary>
        public   string Remark
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
		/// 绑定的账号，对应SYS_Account
        /// </summary>
        public  string BindAccountID
        {
            get; 
            set; 
        }        
		   	/// <summary>
		/// 权限组
        /// </summary>
        public string ROLE_Name
        {
            get; 
            set; 
        }  
      
           	/// <summary>
		/// 权限设置字段
        /// </summary>
        public int ACC_Id
        {
            get; 
            set; 
        }   
		   
        

	}

    
}
