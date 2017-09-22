using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class AttendanceOriginal
    {
        public AttendanceOriginal()
        {
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>				
        public string UserID { get; set; }
        /// <summary>
        /// username
        /// </summary>				
        public string username { get; set; }
        /// <summary>
        /// iMachineNumber
        /// </summary>				
        public string iMachineNumber { get; set; }
        /// <summary>
        /// InputDate
        /// </summary>				
        public DateTime InputDate { get; set; }
        /// <summary>
        /// workDates
        /// </summary>				
        public DateTime workDates { get; set; }
        /// <summary>
        /// idwVerifyMode
        /// </summary>				
        public string idwVerifyMode { get; set; }
        /// <summary>
        /// 系统识别的结果，有效，无效，重复
        /// </summary>				
        public string Recognise { get; set; }
        /// <summary>
        /// 识别的时间
        /// </summary>				
        public DateTime RecogniseTime { get; set; }
        /// <summary>
        /// Classid
        /// </summary>				
        public string Classid { get; set; }
        /// <summary>
        /// 识别到的班级索引号
        /// </summary>				
        public int ClassIndex { get; set; }
        /// <summary>
        /// 识别备注
        /// </summary>
        public string Remark { get; set; }
    }
}
