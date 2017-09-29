using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
   public class vw_Appointment
    {
        /// <summary>
        /// ID
        /// </summary>				
        public string ID { get; set; }
        /// <summary>
        /// ApName
        /// </summary>				
        public string ApName { get; set; }
        /// <summary>
        /// ApTel
        /// </summary>				
        public string ApTel { get; set; }
        /// <summary>
        /// ApSex
        /// </summary>				
        public string ApSex { get; set; }
        /// <summary>
        /// ApMotherName
        /// </summary>				
        public string ApMotherName { get; set; }
        /// <summary>
        /// ApMotherTel
        /// </summary>				
        public string ApMotherTel { get; set; }
        /// <summary>
        /// ApFatherName
        /// </summary>				
        public string ApFatherName { get; set; }
        /// <summary>
        /// ApFatherTel
        /// </summary>				
        public string ApFatherTel { get; set; }
        /// <summary>
        /// ApInterestedClasses
        /// </summary>				
        public string ApInterestedClasses { get; set; }
        /// <summary>
        /// ApInterestedClassNums
        /// </summary>				
        public string ApInterestedClassNums { get; set; }
        /// <summary>
        /// ApStudentID
        /// </summary>				
        public string ApStudentID { get; set; }
        /// <summary>
        /// ApStateID
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
        /// APGrade
        /// </summary>				
        public string APGrade { get; set; }

        public string APLinkMan { get; set; }

        public string APRemark { get; set; }
        /// <summary>
        /// IsJoin
        /// </summary>				
        public string IsJoin { get; set; }
        /// <summary>
        /// StateName
        /// </summary>				
        public string StateName { get; set; }
        /// <summary>
        /// 所属分校
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 查询是否有审核数据
        /// </summary>
        public int EnrollAudit_count { get; set; }
       

    }
}
