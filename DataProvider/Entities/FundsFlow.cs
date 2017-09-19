using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class FundsFlow
    {
        public FundsFlow()
        {
        }
        /// <summary>
        /// 自己流水ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// 资金流水的类别1：报名，2退款，3报销，4升班补差价
        /// </summary>				
        public int TypeID { get; set; }
        /// <summary>
        /// 金额数量
        /// </summary>				
        public decimal Amount { get; set; }
        /// <summary>
        /// 引起金额变化的表单ID
        /// </summary>				
        public string KeyID { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>				
        public string Remark { get; set; }
        /// <summary>
        /// 0正常，2已删除
        /// </summary>				
        public int StateID { get; set; }
    }
}
