using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class vw_UserProgress
    {
        /// <summary>
        /// ѧ��
        /// </summary>			
        public string StudentID { get; set; }
        /// <summary>
        /// �༶ID
        /// </summary>			
        public string ClassID { get; set; }
        /// <summary>
        /// �༶����
        /// </summary>			
        public string ClassName { get; set; }
        /// <summary>
        /// �γ�����
        /// </summary>		
        public string CourseName { get; set; }
         
        /// <summary>
        /// �γ̽���
        /// </summary>	
        public string Introduce { get; set; }
        /// <summary>
        /// ����δ�ÿ�ʱ
        /// </summary>
        public Nullable<int> ClassHour { get; set; }
        /// <summary>
        /// �������ÿ�ʱ
        /// </summary>
        public Nullable<int> UsedHour { get; set; }

        public int Progress_Bar { get; set; } 

    }
}
