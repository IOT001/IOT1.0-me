using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class vw_Transfer
    {
        
        /// <summary>
        /// ID
        /// </summary>				
        public int ID { get; set; }
        /// <summary>
        /// 甲方姓名
        /// </summary>				
        public string JName { get; set; }
        /// <summary>
        /// 甲方身份证
        /// </summary>				
        public string JIDCode { get; set; }
        /// <summary>
        /// 甲方联系地址
        /// </summary>				
        public string JAddress { get; set; }
        /// <summary>
        /// 甲方联系方式
        /// </summary>				
        public string JLinkType { get; set; }
        /// <summary>
        /// JStudentID
        /// </summary>				
        public string JStudentID { get; set; }
        /// <summary>
        /// 甲方的报名记录
        /// </summary>				
        public string JENID { get; set; }
        /// <summary>
        /// 甲方的课程名称
        /// </summary>				
        public string JClassName { get; set; }
        /// <summary>
        /// 甲方当时剩余课时
        /// </summary>				
        public decimal JClassHour { get; set; }
        /// <summary>
        /// 甲方当时剩余课时对应的价格
        /// </summary>				
        public decimal JClassHourPrice { get; set; }
        /// <summary>
        /// 乙方姓名
        /// </summary>				
        public string YName { get; set; }
        /// <summary>
        /// 乙方身份证
        /// </summary>				
        public string YIDCode { get; set; }
        /// <summary>
        /// 乙方联系地址
        /// </summary>				
        public string YAddress { get; set; }
        /// <summary>
        /// 乙方联系方式
        /// </summary>				
        public string YLinkType { get; set; }
        /// <summary>
        /// 乙方学员id，也是被转至的学员id
        /// </summary>				
        public string YStudentID { get; set; }
        /// <summary>
        /// 乙方的报名记录，可能没有，在审核转让协议的时候新增
        /// </summary>				
        public string YENID { get; set; }
        /// <summary>
        /// 乙方所选择的班级号
        /// </summary>				
        public string YClassid { get; set; }
        /// <summary>
        /// 转让价格
        /// </summary>				
        public decimal TranPrice { get; set; }
        /// <summary>
        /// 折合课程
        /// </summary>				
        public string TranCourse { get; set; }
        /// <summary>
        /// 折合课时数
        /// </summary>				
        public decimal TranHour { get; set; }
        /// <summary>
        /// 转账天数
        /// </summary>				
        public decimal TranDays { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// UpdatorId
        /// </summary>				
        public string UpdatorId { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>				
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// DeletorId
        /// </summary>				
        public string DeletorId { get; set; }
        /// <summary>
        /// DeleteTime
        /// </summary>				
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// ApprovedBy
        /// </summary>				
        public string ApprovedBy { get; set; }
        /// <summary>
        /// ApprovedTime
        /// </summary>				
        public DateTime ? ApprovedTime { get; set; }
        /// <summary>
        /// 对应字典表20,1待审核，2审核通过，3审核不通过
        /// </summary>				
        public int StateID { get; set; }
         /// <summary>
        /// 状态中文
        /// </summary>				
        public string StateIDName { get; set; }
        /// <summary>
        /// 甲方名称
        /// </summary>				
        public string JStudentIDName { get; set; }
        /// <summary>
        /// 乙方名称
        /// </summary>				
        public string YStudentIDName { get; set; }


        /// <summary>
        /// ID
        /// </summary>				
        public int SignImageID { get; set; }
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
        public DateTime SignImageCreateTime { get; set; }

         /// <summary>
        ///乙方的签名
        /// </summary>				
        public string siB_ImageData { get; set; }
           /// <summary>
        /// 乙方申请时间
        /// </summary>				
        public DateTime siB_CreateTime { get; set; }
       
       
    }
}
