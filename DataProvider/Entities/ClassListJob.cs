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
        /// Ŀǰ������ѧԱ
        /// </summary>	
        public Nullable<DateTime> CreateTime { get; set; }
        /// <summary>
        /// �ļ�·��
        /// </summary>	
        public int Size { get; set; }
 
       /// <summary>
        /// ��ҵ����
        /// </summary>	
        public string FileName { get; set; }
        /// <summary>
        /// ��ҵ����
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
