using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class Classes
    {
        /// <summary>
        /// ID
        /// </summary>			
        public string ID { get; set; }
        /// <summary>
        /// �༶����
        /// </summary>			
        public string ClassName { get; set; }
        /// <summary>
        /// �γ�ID���ֵ��
        /// </summary>			
        public Nullable<int> CourseID { get; set; }
        /// <summary>
        /// �ڿη�ʽ���ֵ��
        /// </summary>		
        public Nullable<int> TeachTypeID { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>	
        public Nullable<int> PlanEnroll { get; set; }
        /// <summary>
        /// Ŀǰ������ѧԱ
        /// </summary>	
        public Nullable<int> PresentEnroll { get; set; }
        /// <summary>
        /// �ܿ�ʱ
        /// </summary>	
        public Nullable<int> TotalLesson { get; set; }
        /// <summary>
        /// Ŀǰ���Ͽ�ʱ
        /// </summary>	
        public Nullable<int> PresentLesson { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>	
        public Nullable<System.DateTime> StartTime { get; set; }
          /// <summary>
        /// Ԥ�ƽ���ʱ��
        /// </summary>	
        public Nullable<System.DateTime> EndTime { get; set; }

         /// <summary>
        ///    ʵ�ʿ��Σ���ѡ�ſε�ʱ�����ɡ�
        /// </summary>	
        public Nullable<System.DateTime> ActualStartTime { get; set; }
     
         /// <summary>
        /// ʱ��Σ���9:00~11:00,�м��ò����߱�ʾ
        /// </summary>	
        public string TimePeriod { get; set; }
        /// <summary>
        /// �γ�״̬ID���ֵ��
        /// </summary>	
        public Nullable<int> StateID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>	
        public Nullable<int> RoomID { get; set; }
        /// <summary>
        /// ��ʦ
        /// </summary>	
        public string TeacherID { get; set; }

        public string CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdatorId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeletorId { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
    }
}
