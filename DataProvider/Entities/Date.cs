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
        /// ID
        /// </summary>			
        public Nullable<System.DateTime> Start_Date { get; set; }
        /// <summary>
        /// °à¼¶Ãû³Æ
        /// </summary>			
        public Nullable<System.DateTime> End_Date { get; set; }
         
    }
}
