using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class TransferProtocolViewModel
    {
        /// <summary>
        /// 报名记录
        /// </summary>
        public List<vw_Transfer> TransferList { get; set; }
        /// <summary>
        /// 转让协议ID主键
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 单据配置
        /// </summary>
        public BillConfig bill { set; get; }
        /// <summary>
        /// 签名对象
        /// </summary>
        public vw_Transfer si { set; get; }
    }
}
