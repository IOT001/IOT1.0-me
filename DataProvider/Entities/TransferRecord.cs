using DapperExtensions.Mapper;
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
        /// 类型，1转让协议，2升班，3手动调整，4考勤机打卡识别成功，5ERP考勤，6微信端教师考勤
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

        /// <summary>
        /// 报名单ID
        /// </summary>
        public string ENID { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 转换类型中文
        /// </summary>
        public string TypeName { get; set; }
    }

   /// <summary>
   /// Deploy：实体对象映射关系
   /// </summary>
   [Serializable]
   public sealed class TransferRecordORMMapper : ClassMapper<TransferRecord>
   {
       public TransferRecordORMMapper()
       {
           base.Table("TransferRecord");

           Map(f => f.TypeName).Ignore();//设置忽略
           //Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
           AutoMap();
       }
   }
}
