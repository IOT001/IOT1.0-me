using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class Date
    {
        /// <summary>
        /// ��ʼʱ��
        /// </summary>			
        public System.DateTime Start_Date { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>			
        public System.DateTime End_Date { get; set; }


         /// <summary>
        /// ʱ�ӿ�ʼʱ��
        /// </summary>			
        public string addtime_start { get; set; }


         /// <summary>
        /// ʱ�ӽ���ʱ��
        /// </summary>			
        public string addtime_End { get; set; }

         
         
    }
}
