using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Data
{
    public class ClassListData
    {




        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool AddClassList(ClassList Clas, Date date, Classes Clss, Weekday weekday)
        {

            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);//事务

            int curriculum = Clss.TotalLesson;//总的新增记录条数
            DateTime thisdate = date.Start_Date;//循环到的日期
           
            
            try
            {
                ArrayList al = new ArrayList();
                if (!string.IsNullOrWhiteSpace(weekday.Monday))//判断有选择星期
                {
                    al.Add(2);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Tuesday))//判断有选择星期
                {
                    al.Add(3);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Wednesday))//判断有选择星期
                {
                    al.Add(4);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Thursday))//判断有选择星期
                {
                    al.Add(5);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Friday))//判断有选择星期
                {
                    al.Add(6);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Saturday))//判断有选择星期
                {
                    al.Add(7);
                }
                if (!string.IsNullOrWhiteSpace(weekday.Sunday))//判断有选择星期
                {
                    al.Add(1);
                }
                if (Convert.ToInt32(thisdate.DayOfWeek) > int.Parse(al[0].ToString()))//如果当前星期大于要循环的第一个星期
                {
                    thisdate = GetWeekFirstDaySun(thisdate).AddDays(7);//起始设为下一周第一天
                }
                else
                {
                    thisdate = GetWeekFirstDaySun(thisdate);//起始设为当前周第一天
                }
                db.BeginTransaction();//事务开始
                 for (int i = 1; i <= curriculum; )//根据总课时和开始时间来生成课程
                {        
                       foreach (int theweek in al)//循环星期
                       {
                           if (i <= curriculum)//如果课时还未生成完则继续
                           {
                               ClassList CL = new ClassList();
                               CL.ClassID = Clas.ClassID;
                               CL.ClassIndex = i;
                               CL.ClassDate = DateTime.Parse(thisdate.AddDays(theweek - 1).ToShortDateString() + GetStartTimePeriodByid(Clas.TimePeriod.Value));
                               CL.TimePeriod = Clas.TimePeriod;
                               CL.StateID = 1;
                               CL.TeacherID = Clas.TeacherID;
                               CL.Teacher2ID = Clas.Teacher2ID;
                               CL.RoomID = Clas.RoomID;
                               CL.CreateTIme = DateTime.Now;
                               CL.CreatorId = Clas.CreatorId;
                               CL.weekday = theweek;
                               db.Insert(CL);
                               List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
                               AttendanceRecord attend = new AttendanceRecord();
                               attend.CreateTime = DateTime.Now;  //创建时间
                               attend.CreatorId = Clas.CreatorId; //创建人
                               attend.ClassID = Clas.ClassID;//班级编号
                               attend.ClassIndex = i;//班次序号，也就是班级生成的集体上课记录 
                               attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤

                               for (int j = 0; j < Enroll.Count(); j++)
                               {
                                   attend.StudentID = Enroll[j].StudentID;
                                   if (Enroll[j].ClassHour - Enroll[j].UsedHour >= i)//如果剩余课时还够
                                       db.Insert<AttendanceRecord>(attend);//增加上课记录表数据

                               }
                               i = i + 1;
                           }
                       }
                       thisdate = thisdate.AddDays(7);//循环到下一周
                }

                if (UpdateClasses(Clas.ClassID, Clss.TotalLesson, db) > 0)//反写回Classes班级维护表的总课时字段
                {
                    db.Commit(); //事务提交
                    db.Dispose();  //资源释放
                    ret = true;//新增成功
                }           
              
            }
            catch (Exception ex)
            {

                db.Rollback();
                db.Dispose();//资源释放
                throw new Exception(ex.Message + "。" + ex.InnerException.Message);
            }

            return ret;
        }

        /// <summary>  
        /// 得到本周第一天(以星期天为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekFirstDaySun(DateTime datetime)
        {
            //星期天为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        } 
       /// <summary>
       /// 新增,返回的是主键
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
       // public static bool AddClassList(ClassList Clas,Date date,Classes Clss,Weekday weekday)
       //{
       //    var number = 0;//因为事务查询数据库会直接卡死
       //    bool ret = false;
       //    DBRepository db = new DBRepository(DBKeys.PRX);
       //    try
       //    {
            
       //        db.BeginTransaction();//事务开始

             
       //        var lenthed = date.End_Date.Subtract(date.Start_Date).Days; //取日期之间的差距天数
       //    for (int i = 0; i <= lenthed; i++)
       //    {
       //        var Date = date.Start_Date.AddDays(i);
       //        var week = Convert.ToInt32(Date.DayOfWeek);  //这个时间是星期几

       //        int Monday=-1;
       //        int Tuesday = -1;
       //        int Wednesday = -1;
       //        int Thursday = -1;
       //        int Friday = -1;
       //        int Saturday = -1;
       //        int Sunday = -1;
       //        if (!string.IsNullOrWhiteSpace(weekday.Monday))
       //        {
       //             Monday = week.ToString().IndexOf(weekday.Monday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Tuesday))
       //        {
       //             Tuesday = week.ToString().IndexOf(weekday.Tuesday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Wednesday))
       //        {
       //             Wednesday = week.ToString().IndexOf(weekday.Wednesday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Thursday))
       //        {
       //             Thursday = week.ToString().IndexOf(weekday.Thursday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Friday))
       //        {
       //             Friday = week.ToString().IndexOf(weekday.Friday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Saturday))
       //        {
       //             Saturday = week.ToString().IndexOf(weekday.Saturday);  //判断是否包含在星期里面
       //        }
       //        if (!string.IsNullOrWhiteSpace(weekday.Sunday))
       //        {
       //             Sunday = week.ToString().IndexOf(weekday.Sunday);  //判断是否包含在星期里面
       //        }
                
       //        if (Monday > -1 || Tuesday > -1 || Wednesday > -1 || Thursday > -1 || Friday > -1 || Saturday > -1 || Sunday > -1)
       //        {
                  
              
       //            if (number==0)
       //            {
       //                number = Getnumber(Clas.ClassID);//取行号
       //            }
       //            number = number + 1;
       //            Clas.weekday = week;//星期几
       //            Clas.ClassDate = Date;//上课日期
       //            Clas.ClassIndex = number;  //班次序号，也就是班级生成的集体上课记录 
       //            db.Insert<ClassList>(Clas); //增加排课表数据 
       //           // MsSqlMapperHepler.Insert<ClassList>(Clas, DBKeys.PRX);  //增加排课表数据 


       //            List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
       //            AttendanceRecord attend = new AttendanceRecord();
       //            attend.CreateTime = DateTime.Now;  //创建时间
       //            attend.CreatorId = Clas.CreatorId; //创建人
       //            attend.ClassID = Clas.ClassID;//班级编号
       //            attend.ClassIndex = number;//班次序号，也就是班级生成的集体上课记录 
       //            attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤
       //            attend.AttendanceWayID = 3;//AttendanceWayID默认3，教师操作的考勤。
       //            for (int j = 0; j < Enroll.Count(); j++)
       //            { 
       //                attend.StudentID = Enroll[j].StudentID;
       //                db.Insert<AttendanceRecord>(attend);//增加上课记录表数据
       //                //MsSqlMapperHepler.Insert<AttendanceRecord>(attend, DBKeys.PRX); //增加上课记录表数据
       //            }
                    
       //        }
       //    }
       //    if (UpdateClasses(Clas.ClassID, Clss.TotalLesson,db) > 0)//反写回Classes班级维护表的总课时字段
       //    {
       //        db.Commit(); //事务提交
               
       //        ret = true;//新增成功
       //    }

       //    db.Dispose();  //资源释放
       //    }
       //    catch (Exception ex)
       //    {

       //        db.Rollback();
       //        db.Dispose();//资源释放
       //        throw new Exception(ex.Message + "。" + ex.InnerException.Message);
       //    }
 
       //    return ret;

  
       //}
        /// <summary>
        /// 获取Enroll表数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        //public static Enroll GetEnrollByID(string ClassID)
        //{
        //    return MsSqlMapperHepler.GetOne<Enroll>(ClassID, DBKeys.PRX);
        //}
        public static List<Enroll> GetEnrollByID(string ClassID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select *  from   Enroll ");
            sb.Append(" where ClassID=@ClassID "); 
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", ClassID);
            return MsSqlMapperHepler.SqlWithParams<Enroll>(sb.ToString(), parameters, DBKeys.PRX);
        }


        /// <summary>
        /// 修改Classes表的课时总数
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateClasses(string ID, int TotalLesson, DBRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update Classes set StateID=2  ");//TotalLesson=@TotalLesson,
            sb.Append(" where ID=@ID ");
            var parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
           // parameters.Add("@TotalLesson", TotalLesson);
            return db.Execute(sb.ToString(), parameters);
           
        }


        /// <summary>
        /// 新增时钟（字典表）
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool AddDictionaryItemt(DictionaryItem item)
        {
            bool ret = false;
            try
            {
                 MsSqlMapperHepler.Insert<DictionaryItem>(item, DBKeys.PRX);
                 ret = true;
            }
            catch (Exception)
            {
                
                throw;
            }
            return ret;
        }


        //<summary>
        //获取ClassList行数
        //</summary>
        //<param name="Stockid"></param>
        //<returns></returns>


        public static int Getnumber(string ClassID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(ClassID) from ClassList  WITH(NOLOCK)  ");
            sb.Append(" where ClassID=@ClassID ");
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", ClassID);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sb.ToString(), parameters, DBKeys.PRX);
        }


 


        //<summary>
        //判断ClassList表是否已经存在相对应的数据，有就不能再新增了
        //</summary>
        //<param name="Stockid"></param>
        //<returns></returns>  
        public static int GetClassList_num(string ClassID, string TimePeriod, DateTime ClassDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select COUNT(Classid) from ClassList   ");
            sb.Append(" where Classid=@Classid and   TimePeriod=@TimePeriod ");
            sb.Append(" and ClassDate = @ClassDate");
            //sb.Append(" and weekday = @weekday");
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", ClassID);
            parameters.Add("@TimePeriod", TimePeriod);
            parameters.Add("@ClassDate", ClassDate);
          //  parameters.Add("@weekday", weekday);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sb.ToString(), parameters, DBKeys.PRX);
        }

        public static int UpdateClassList(ClassList cls)
        {

            ///INSERT INTO [PRX].[dbo].[ClassList] ([ClassID], [ClassIndex], [ClassDate], [TimePeriod], [StateID], [TeacherID], [RoomID], [CreateTIme], [CreatorId])
            ///VALUES (N'CL20170001', '1', '2017-06-15 23:24:28.000', N'2', '1', N'2AB45559B74BAA9962A189CD2D21F478', '1', '2017-06-15 23:25:42.000', N'1');
            StringBuilder sb = new StringBuilder();
            sb.Append(" update ClassList set ClassDate=@ClassDate  ");
            sb.Append(" , TimePeriod=@TimePeriod  ");
            sb.Append(" , TeacherID=@TeacherID  ");
            sb.Append(" , Teacher2ID=@Teacher2ID  ");
            sb.Append(" , RoomID=@RoomID  ");
            sb.Append(" where ClassID=@ClassID ");
            sb.Append(" AND ClassIndex=@ClassIndex ");
            var parameters = new DynamicParameters();
            parameters.Add("@ClassDate", cls.ClassDate);
            parameters.Add("@ClassID", cls.ClassID);
            parameters.Add("@TeacherID", cls.TeacherID);
            parameters.Add("@Teacher2ID", cls.Teacher2ID);
            parameters.Add("@RoomID", cls.RoomID);
            parameters.Add("@ClassIndex", cls.ClassIndex);
            parameters.Add("@TimePeriod", cls.TimePeriod);
            return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
        }
        /// <summary>
        /// 获取classlist对象
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="classindex"></param>
        /// <returns></returns>
        public static ClassList GetOneByid(string classid, int classindex)
        {
            string strsql = "select * from ClassList where ClassID =@ClassID and ClassIndex = @ClassIndex";
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classid);
            parameters.Add("@ClassIndex", classindex);
            return MsSqlMapperHepler.SqlWithParamsSingle<ClassList>(strsql.ToString(), parameters, DBKeys.PRX);
        }
        /// <summary>
        /// 根据TimePeriod的ID获取开始时间
        /// </summary>
        /// <param name="perid"></param>
        /// <returns></returns>
        public static string GetStartTimePeriodByid(int perid)
        {
            string ret = "";
            string strsql = "select DicItemName from DictionaryItem where DicTypeID = 8 and [DicItemID] = " + perid;
            ret =" " + MsSqlMapperHepler.SqlWithParamsSingle<string>(strsql, null, DBKeys.PRX).Substring(0, 5);
            return ret;
        }
        /// <summary>
        /// 刷新排课记录，主要用于补漏
        /// </summary>
        public static bool RefreshClassList(string classid, string loginid)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            string strsql = "SELECT * FROM dbo.Enroll WHERE StudentID NOT IN (SELECT StudentID FROM dbo.AttendanceRecord) AND ClassID IN (SELECT ID FROM dbo.Classes WHERE StateID =2 ) and ClassID = '" + classid + "'";
            List<Enroll> enlist = MsSqlMapperHepler.SqlWithParams<Enroll>(strsql, null, DBKeys.PRX);//找到未报名记录
            Classes cl = db.GetById<Classes>(classid);
            db.BeginTransaction();//事务开始
            try
            {
                
                foreach (var en in enlist)
                {
                    if (cl.StateID.Value == 2 || cl.StateID.Value == 3)//如果班级状态是已排课，或已上课，生成课程表AttendanceRecord
                    {
                        List<vw_ClassAttendanceList> clist = new List<vw_ClassAttendanceList>();
                        clist = db.Query<vw_ClassAttendanceList>("select * from vw_ClassAttendanceList where ClassID = '" + cl.ID + "'", null).ToList();
                        int aa = Convert.ToInt32(en.ClassHour) < clist.Count() ? Convert.ToInt32(en.ClassHour) : clist.Count();//取较小数做循环
                        for (int i = 0; i < aa; i++)
                        {
                            AttendanceRecord attend = new AttendanceRecord();
                            attend.CreateTime = DateTime.Now;  //创建时间
                            attend.CreatorId = loginid; //创建人
                            attend.ClassID = en.ClassID;//班级编号
                            attend.ClassIndex = i + 1;//班次序号，也就是班级生成的集体上课记录 
                            attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤
                            attend.StudentID = en.StudentID;//学员号
                            db.Insert<AttendanceRecord>(attend);//增加上课记录表数据
                        }
                    }

                }
                db.Commit();
                db.Dispose();
                ret = true;
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
