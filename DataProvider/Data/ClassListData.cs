using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using System;
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
            var number = 0;//因为事务查询数据库会直接卡死
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);

            int Multiple = 0;
            if (!string.IsNullOrWhiteSpace(weekday.Monday))
            {
               Multiple= Multiple+1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Tuesday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Wednesday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Thursday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Friday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Saturday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }
            if (!string.IsNullOrWhiteSpace(weekday.Sunday))
            {
                Multiple = Multiple + 1;  //判断是否包含在星期里面
            }

            int curriculum = 0;
            if ((Clss.TotalLesson % Multiple)!=0)
            {
                  curriculum = (Clss.TotalLesson / Multiple) + 2;
            }
            else
            {
                  curriculum = (Clss.TotalLesson / Multiple) + 1;
            }

           

            try
            {

                db.BeginTransaction();//事务开始
                 int Last=0;
                 for (int i = 1; i <= curriculum; i++)//根据总课时和开始时间来生成课程
                {
                    // var Date = date.Start_Date.AddDays(i);
                    //var week = Convert.ToInt32(Date.DayOfWeek);  //这个时间是星期几



                       int weeknow = Convert.ToInt32(date.Start_Date.DayOfWeek);
                    
                        //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
                        weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
                        int daydiff = (-1) * weeknow;

                        //本周第一天  
                        string FirstDay = date.Start_Date.AddDays(daydiff).ToString("yyyy-MM-dd");
                        DateTime timeStart = Convert.ToDateTime(FirstDay);

                        //星期天为最后一天  
                        int weeknow_sum = Convert.ToInt32(date.Start_Date.DayOfWeek);
                        weeknow_sum = (weeknow_sum == 0 ? 7 : weeknow_sum);
                        int daydiff_sum = (7 - weeknow_sum);

                        //本周最后一天  
                        string LastDay = date.Start_Date.AddDays(daydiff_sum).ToString("yyyy-MM-dd");
                        DateTime timeEnd = Convert.ToDateTime(LastDay);


                      
                       

                       

                       // search.timeStart = timeStart.ToString();//开始时间
                       // search.timeEnd = timeEnd.ToString();//结束时间

                       //取到第一次生成数据时的开始时间是星期几、
                       
                        if (i == 1)//有可能选择的是星期1，2。但是开始时间是从星期开始，所以不能七天来判断
                        {
                            Last = Convert.ToInt32(date.Start_Date.DayOfWeek);
                        }
                        //第一次生成
                        if (i==1)//有可能选择的是星期1，2。但是开始时间是从星期开始，所以不能七天来判断
                        {
                            int time_sum =timeEnd.DayOfYear- date.Start_Date.DayOfYear;
                            for (int m = 0; m <= time_sum; m++)
                            {
                                var Date = date.Start_Date.AddDays(m);
                                var week = Convert.ToInt32(Date.DayOfWeek);  //这个时间是星期几
                                
                                int Monday = -1;
                                int Tuesday = -1;
                                int Wednesday = -1;
                                int Thursday = -1;
                                int Friday = -1;
                                int Saturday = -1;
                                int Sunday = -1;

                                if (!string.IsNullOrWhiteSpace(weekday.Monday))
                                {
                                    Monday = week.ToString().IndexOf(weekday.Monday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Tuesday))
                                {
                                    Tuesday = week.ToString().IndexOf(weekday.Tuesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Wednesday))
                                {
                                    Wednesday = week.ToString().IndexOf(weekday.Wednesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Thursday))
                                {
                                    Thursday = week.ToString().IndexOf(weekday.Thursday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Friday))
                                {
                                    Friday = week.ToString().IndexOf(weekday.Friday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Saturday))
                                {
                                    Saturday = week.ToString().IndexOf(weekday.Saturday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Sunday))
                                {
                                    Sunday = week.ToString().IndexOf(weekday.Sunday);  //判断是否包含在星期里面
                                }

                                if (Monday > -1 || Tuesday > -1 || Wednesday > -1 || Thursday > -1 || Friday > -1 || Saturday > -1 || Sunday > -1)
                                {


                                    if (number == 0)
                                    {
                                        number = Getnumber(Clas.ClassID);//取行号
                                    }
                                    number = number + 1;
                                    Clas.weekday = week;//星期几
                                    Clas.ClassDate = Date;//上课日期
                                    Clas.ClassIndex = number;  //班次序号，也就是班级生成的集体上课记录 
                                    db.Insert<ClassList>(Clas); //增加排课表数据 
                                    // MsSqlMapperHepler.Insert<ClassList>(Clas, DBKeys.PRX);  //增加排课表数据 


                                    List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
                                    AttendanceRecord attend = new AttendanceRecord();
                                    attend.CreateTime = DateTime.Now;  //创建时间
                                    attend.CreatorId = Clas.CreatorId; //创建人
                                    attend.ClassID = Clas.ClassID;//班级编号
                                    attend.ClassIndex = number;//班次序号，也就是班级生成的集体上课记录 
                                    attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤
                                    attend.AttendanceWayID = 3;//AttendanceWayID默认3，教师操作的考勤。
                                    for (int j = 0; j < Enroll.Count(); j++)
                                    {
                                        attend.StudentID = Enroll[j].StudentID;
                                        db.Insert<AttendanceRecord>(attend);//增加上课记录表数据
                                        //MsSqlMapperHepler.Insert<AttendanceRecord>(attend, DBKeys.PRX); //增加上课记录表数据
                                    }

                                }
                            }


                        }    //最后一次生成
                        else if (i == curriculum)//有可能选择的是星期1，2。但是开始时间是从星期开始，所以不能七天来判断
                        {


                            for (int m = 0; m <= 6; m++)
                            {
                                //判断相应的星期是否已经生成了，如果生成了就去掉，不生成
                                if (Last <= Convert.ToInt32(weekday.Monday))
                                {
                                    weekday.Monday = null;
                                }
                                if (Last <= Convert.ToInt32(weekday.Tuesday))
                                {
                                    weekday.Tuesday = null;
                                }
                                if (Last <= Convert.ToInt32(weekday.Wednesday))
                                {
                                    weekday.Wednesday = null;
                                }
                                if (Last <= Convert.ToInt32(weekday.Thursday))
                                {
                                    weekday.Thursday = null;
                                }
                                if (Last <= Convert.ToInt32(weekday.Friday))
                                {
                                    weekday.Friday = null;
                                }
                                if (Last <= Convert.ToInt32(weekday.Saturday))
                                {
                                    weekday.Saturday = null;
                                }
                                //星期天特殊处理
                                weekday.Sunday = null;




                                var Date = timeStart.AddDays(m);
                                var week = Convert.ToInt32(Date.DayOfWeek);  //这个时间是星期几
                                int Monday = -1;
                                int Tuesday = -1;
                                int Wednesday = -1;
                                int Thursday = -1;
                                int Friday = -1;
                                int Saturday = -1;
                                int Sunday = -1;

                                if (!string.IsNullOrWhiteSpace(weekday.Monday))
                                {
                                    Monday = week.ToString().IndexOf(weekday.Monday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Tuesday))
                                {
                                    Tuesday = week.ToString().IndexOf(weekday.Tuesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Wednesday))
                                {
                                    Wednesday = week.ToString().IndexOf(weekday.Wednesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Thursday))
                                {
                                    Thursday = week.ToString().IndexOf(weekday.Thursday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Friday))
                                {
                                    Friday = week.ToString().IndexOf(weekday.Friday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Saturday))
                                {
                                    Saturday = week.ToString().IndexOf(weekday.Saturday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Sunday))
                                {
                                    Sunday = week.ToString().IndexOf(weekday.Sunday);  //判断是否包含在星期里面
                                }

                                if (Monday > -1 || Tuesday > -1 || Wednesday > -1 || Thursday > -1 || Friday > -1 || Saturday > -1 || Sunday > -1)
                                {

                                    int count_number = Getnumber(Clas.ClassID);//取行号
                                    if (count_number == Clss.TotalLesson)
                                    {
                                        //没有必要往下面执行了，因为已经执行完了
                                    }
                                    else
                                    {
                                        if (number == 0)
                                        {
                                            number = Getnumber(Clas.ClassID);//取行号
                                        }
                                        number = number + 1;
                                        Clas.weekday = week;//星期几
                                        Clas.ClassDate = Date;//上课日期
                                        Clas.ClassIndex = number;  //班次序号，也就是班级生成的集体上课记录 
                                        db.Insert<ClassList>(Clas); //增加排课表数据 
                                        // MsSqlMapperHepler.Insert<ClassList>(Clas, DBKeys.PRX);  //增加排课表数据 


                                        List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
                                        AttendanceRecord attend = new AttendanceRecord();
                                        attend.CreateTime = DateTime.Now;  //创建时间
                                        attend.CreatorId = Clas.CreatorId; //创建人
                                        attend.ClassID = Clas.ClassID;//班级编号
                                        attend.ClassIndex = number;//班次序号，也就是班级生成的集体上课记录 
                                        attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤
                                        attend.AttendanceWayID = 3;//AttendanceWayID默认3，教师操作的考勤。
                                        for (int j = 0; j < Enroll.Count(); j++)
                                        {
                                            attend.StudentID = Enroll[j].StudentID;
                                            db.Insert<AttendanceRecord>(attend);//增加上课记录表数据
                                            //MsSqlMapperHepler.Insert<AttendanceRecord>(attend, DBKeys.PRX); //增加上课记录表数据
                                        }

                                    }
                                }
                            }


                        }
                        else
                        {
                            for (int m = 0; m <= 6; m++)
                            {
                                var Date = timeStart.AddDays(m);
                                var week = Convert.ToInt32(Date.DayOfWeek);  //这个时间是星期几
                                int Monday = -1;
                                int Tuesday = -1;
                                int Wednesday = -1;
                                int Thursday = -1;
                                int Friday = -1;
                                int Saturday = -1;
                                int Sunday = -1;

                                if (!string.IsNullOrWhiteSpace(weekday.Monday))
                                {
                                    Monday = week.ToString().IndexOf(weekday.Monday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Tuesday))
                                {
                                    Tuesday = week.ToString().IndexOf(weekday.Tuesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Wednesday))
                                {
                                    Wednesday = week.ToString().IndexOf(weekday.Wednesday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Thursday))
                                {
                                    Thursday = week.ToString().IndexOf(weekday.Thursday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Friday))
                                {
                                    Friday = week.ToString().IndexOf(weekday.Friday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Saturday))
                                {
                                    Saturday = week.ToString().IndexOf(weekday.Saturday);  //判断是否包含在星期里面
                                }
                                if (!string.IsNullOrWhiteSpace(weekday.Sunday))
                                {
                                    Sunday = week.ToString().IndexOf(weekday.Sunday);  //判断是否包含在星期里面
                                }

                                if (Monday > -1 || Tuesday > -1 || Wednesday > -1 || Thursday > -1 || Friday > -1 || Saturday > -1 || Sunday > -1)
                                {


                                    if (number == 0)
                                    {
                                        number = Getnumber(Clas.ClassID);//取行号
                                    }
                                    number = number + 1;
                                    Clas.weekday = week;//星期几
                                    Clas.ClassDate = Date;//上课日期
                                    Clas.ClassIndex = number;  //班次序号，也就是班级生成的集体上课记录 
                                    db.Insert<ClassList>(Clas); //增加排课表数据 
                                    // MsSqlMapperHepler.Insert<ClassList>(Clas, DBKeys.PRX);  //增加排课表数据 


                                    List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
                                    AttendanceRecord attend = new AttendanceRecord();
                                    attend.CreateTime = DateTime.Now;  //创建时间
                                    attend.CreatorId = Clas.CreatorId; //创建人
                                    attend.ClassID = Clas.ClassID;//班级编号
                                    attend.ClassIndex = number;//班次序号，也就是班级生成的集体上课记录 
                                    attend.AttendanceTypeID = 1;//上课状态,默认为1，未考勤
                                    attend.AttendanceWayID = 3;//AttendanceWayID默认3，教师操作的考勤。
                                    for (int j = 0; j < Enroll.Count(); j++)
                                    {
                                        attend.StudentID = Enroll[j].StudentID;
                                        db.Insert<AttendanceRecord>(attend);//增加上课记录表数据
                                        //MsSqlMapperHepler.Insert<AttendanceRecord>(attend, DBKeys.PRX); //增加上课记录表数据
                                    }

                                }
                            }
                        }


                        date.Start_Date = timeEnd.AddDays(1);//改变最后时间，重新循环
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
            sb.Append("select StudentID  from   Enroll ");
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
            sb.Append(" , RoomID=@RoomID  ");
            sb.Append(" where ClassID=@ClassID ");
            sb.Append(" AND ClassIndex=@ClassIndex ");
            var parameters = new DynamicParameters();
            parameters.Add("@ClassDate", cls.ClassDate);
            parameters.Add("@ClassID", cls.ClassID);
            parameters.Add("@TeacherID", cls.TeacherID);
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
    }
}
