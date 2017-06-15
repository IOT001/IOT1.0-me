using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{

    
    public partial class vw_Classes
    {
        public string ID { get; set; }
        public string ClassName { get; set; }
        public string CourseID { get; set; }
        public string TeachTypeID { get; set; }
        public string Enroll { get; set; } 
        public string Lesson { get; set; } 
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string TimePeriod { get; set; }
        public string StateID { get; set; }
        public string RoomID { get; set; }
        public string TeacherID { get; set; }
        public string name { get; set; } 
        public string CreatorId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string UpdatorId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeletorId { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
    }
}
