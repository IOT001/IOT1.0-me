using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class Accounts_UpdateViewModel
    {
        /// <summary>
        /// 账号记录
        /// </summary>
        public List<SYS_Account> SYS_AccountList { get; set; }
        /// <summary>
        /// SYS_Account_ID
        /// </summary>
        public int ACC_id { set; get; }
        
    }
}
