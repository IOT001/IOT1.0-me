using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{


    public partial class ClassListJob
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
<<<<<<< HEAD
        public string Size { get; set; }
=======
        public int Size { get; set; }
>>>>>>> 29b8f778205d047c73f822a825197896aac55be6
 
       /// <summary>
        /// �ļ�����
        /// </summary>	
        public string FileName { get; set; }
        /// <summary>
        ///�ļ�
        /// </summary>	
        public string FileTitle { get; set; }


        public string FileRoute
        {
            get
            {
                string route = ConfigurationManager.AppSettings["ClassJobPath"].ToString() + FileName;
                return route;
            }
            set;
        }

    }



    /// <summary>
    /// Deploy��ʵ�����ӳ���ϵ
    /// </summary>
    [Serializable]
    public sealed class ClassListJobORMMapper : ClassMapper<ClassListJob>
    {
        public ClassListJobORMMapper()
        {
            base.Table("ClassListJob");

            //Map(f => f.IsJoin).Ignore();//���ú��� 
            Map(f => f.ID).Key(KeyType.Identity);//��������  (����������Ʋ�������ĸ��ID����������)      
            AutoMap();
        }
    }
}
