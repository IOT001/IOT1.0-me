using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class TransferRecord
    {
        public TransferRecord()
        {
        }
        /// <summary>
        /// 转移流水记录ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// StudentID
        /// </summary>				
        public string StudentID { get; set; }
        /// <summary>
        /// 转之前的课时
        /// </summary>				
        public decimal BeforeHours { get; set; }
        /// <summary>
        /// 转之后课时
        /// </summary>				
        public decimal AfterHours { get; set; }
        /// <summary>
        /// 类型，1转让协议，2升班
        /// </summary>				
        public int TypeID { get; set; }
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
    }
}
