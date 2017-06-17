using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class FollowRecord
    {
        public FollowRecord()
        {

            FollowTime = DateTime.Now;

        }
        /// <summary>
        /// ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// 预约号
        /// </summary>				
        public string APID { get; set; }
        /// <summary>
        /// 跟进方式，字典表
        /// </summary>				
        public int FollowTypeID { get; set; }
        /// <summary>
        /// 意向ID，字典表
        /// </summary>				
        public int IntenID { get; set; }
        /// <summary>
        /// Remark
        /// </summary>				
        public string Remark { get; set; }
        /// <summary>
        /// FollowTime
        /// </summary>				
        public DateTime FollowTime { get; set; }
        /// <summary>
        /// 跟进人
        /// </summary>				
        public string FollowPersonID { get; set; }
    }
}
