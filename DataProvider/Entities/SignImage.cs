using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class SignImage
    {
        public SignImage()
        {
        }
        /// <summary>
        /// ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// int类主键
        /// </summary>				
        public int IntKey { get; set; }
        /// <summary>
        /// 字符串类主键
        /// </summary>				
        public string StringKey { get; set; }
        /// <summary>
        /// 1入学协议，2转让协议
        /// </summary>				
        public int TypeID { get; set; }
        /// <summary>
        /// 签名图像
        /// </summary>				
        public string ImageData { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>				
        public DateTime CreateTime { get; set; }
    }
}
