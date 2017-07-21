using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class vw_ClassListJob
    {
        /// <summary>
        /// ID
        /// </summary>			
        public int ID { get; set; }
        /// <summary>
        /// �༶ID
        /// </summary>			
        public string Classid { get; set; }
        /// <summary>
        /// �γ̱��к�
        /// </summary>			
        public int Classindex { get; set; }
        /// <summary>
        /// �ļ�����
        /// </summary>		
        public string ContentType { get; set; }
        /// <summary>
        /// ������
        /// </summary>	
        public string CreatorId { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>	
        public Nullable<DateTime> CreateTime { get; set; }
        /// <summary>
        /// �ļ���С
        /// </summary>	
 
        public string Size { get; set; } 
 
       /// <summary>
        /// �ļ�����
        /// </summary>	
        public string FileName { get; set; }
        /// <summary>
        ///�ļ�
        /// </summary>	
        public string FileTitle { get; set; }

         /// <summary>
        ///��ҵ����
        /// </summary>	
        public string JobTitle { get; set; }
        /// <summary>
        ///��ҵ����
        /// </summary>	
        public string JobContent { get; set; }
      

        public string FileRoute
        {
            
            get
            {
                string route = ConfigurationManager.AppSettings["ClassJobPath"].ToString() + FileName;
                return route;
            }
            set { FileRoute = value; }
       
        }

    }

 
}
