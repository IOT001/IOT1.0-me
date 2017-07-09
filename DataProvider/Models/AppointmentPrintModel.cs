using DataProvider.Entities;
using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class AppointmentPrintModel
    {
        /// <summary>
        /// 报名记录
        /// </summary>
        public List<vw_Enroll> EnrollList { get; set; }
        /// <summary>
        /// 资源单号
        /// </summary>
        public string apid { set; get; }
        /// <summary>
        /// 单据配置
        /// </summary>
        public BillConfig bill {set; get; }
    }
}
