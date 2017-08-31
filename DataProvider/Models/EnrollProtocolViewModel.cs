using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class EnrollProtocolViewModel
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
        public BillConfig bill { set; get; }
        /// <summary>
        /// 签名对象
        /// </summary>
        public SignImage si { set; get; }
    }
}
