using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class Date
    {
        /// <summary>
        /// 开始时间
        /// </summary>			
        public System.DateTime Start_Date { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>			
        public System.DateTime End_Date { get; set; }


         /// <summary>
        /// 时钟开始时间
        /// </summary>			
        public string addtime_start { get; set; }


         /// <summary>
        /// 时钟结束时间
        /// </summary>			
        public string addtime_End { get; set; }

         
         
    }
}
