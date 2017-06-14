using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class Appointment
    {
        public Appointment()
        {

        }
        #region Model
        /// <summary>
        /// 预约登记号
        /// </summary>				
        public string ID { get; set; }
        /// <summary>
        /// 预约人姓名
        /// </summary>				
        public string ApName { get; set; }
        /// <summary>
        /// 预约人电话
        /// </summary>				
        public string ApTel { get; set; }
        /// <summary>
        /// 0：女，1：男，3：保密
        /// </summary>				
        public string ApSex { get; set; }
        /// <summary>
        /// 母亲姓名
        /// </summary>				
        public string ApMotherName { get; set; }
        /// <summary>
        /// 母亲联系电话
        /// </summary>				
        public string ApMotherTel { get; set; }
        /// <summary>
        /// 预约人父亲
        /// </summary>				
        public string ApFatherName { get; set; }
        /// <summary>
        /// 父亲联系电话
        /// </summary>				
        public string ApFatherTel { get; set; }
        /// <summary>
        /// 感兴趣的课程名称，逗号隔开
        /// </summary>				
        public string ApInterestedClasses { get; set; }
        /// <summary>
        /// 感兴趣的课程编号
        /// </summary>				
        public string ApInterestedClassNums { get; set; }
        /// <summary>
        /// 正式学员号
        /// </summary>				
        public string ApStudentID { get; set; }
        /// <summary>
        /// 状态ID，对应字典表
        /// </summary>				
        public int ApStateID { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>				
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>				
        public string CreatorId { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>				
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// UpdatorId
        /// </summary>				
        public string UpdatorId { get; set; }
        /// <summary>
        /// DeletorId
        /// </summary>				
        public string DeletorId { get; set; }
        /// <summary>
        /// DeleteTime
        /// </summary>				
        public DateTime DeleteTime { get; set; }
        /// <summary>
        /// 报名填写的年级
        /// </summary>				
        public string APGrade { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string APLinkMan { get; set; }
        /// <summary>
        /// 预约/市场资源备注
        /// </summary>
        public string APRemark { get; set; }
        #endregion Model
        /// <summary>
        /// 是否是正式学员
        /// </summary>
        public string IsJoin { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }
    }
}
