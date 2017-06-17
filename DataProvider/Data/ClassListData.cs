﻿using Dapper;
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
        public static bool AddClassList(ClassList Clas,Date date,Classes Clss)
       {
           bool ret = false;
           //判断ClassList表是否已经存在相对应的数据，有就不能再新增了
           if (GetClassList_num(Clas.ClassID,Clas.TimePeriod,date.Start_Date,date.End_Date)>0)
           {
                return ret;
           }
           DBRepository db = new DBRepository(DBKeys.PRX);
           try
           {
                db.BeginTransaction();//事务开始

               var lenthed=  date.End_Date.Subtract(date.Start_Date).Days; //取日期之间的差距天数
                    // var ClassDate = date.Start_Date;   //取日期
               var number = Getnumber(Clas.ClassID);//取行号

               List<Enroll> Enroll = GetEnrollByID(Clas.ClassID);//获取Enroll报名表的数据
               AttendanceRecord attend = new AttendanceRecord();
               attend.CreateTime = DateTime.Now;  //创建时间
               attend.CreatorId = Clas.CreatorId; //创建人
               attend.ClassID = Clas.ClassID;//班级编号
               
               for (int i = 0; i < lenthed; i++)
               {
                   Clas.ClassDate = date.Start_Date.AddDays(i);
                   Clas.ClassIndex = i + 1;  //班次序号，也就是班级生成的集体上课记录
                   attend.ClassIndex = i + 1;//班次序号，也就是班级生成的集体上课记录

                   MsSqlMapperHepler.Insert<ClassList>(Clas, DBKeys.PRX);  //增加排课表数据 
                   for (int j = 0; j < Enroll.Count(); j++)
                   {
                       attend.StudentID = Enroll[i].StudentID;
                       MsSqlMapperHepler.Insert<AttendanceRecord>(attend, DBKeys.PRX); //增加上课记录表数据
                   }
                 
               }
              
               UpdateClasses(Clas.ClassID, Clss.TotalLesson);//反写回Classes班级维护表的总课时字段
                 db.Commit(); //事务提交
                 db.Dispose();  //资源释放
           }
           catch (Exception )
           {

               db.Rollback();
               db.Dispose();

           }
           return ret;


  
       }
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
        public static bool UpdateClasses(string ID,int TotalLesson)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update Classes set TotalLesson=@TotalLesson  ");
            sb.Append(" where ID=@ID ");
            var parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            parameters.Add("@TotalLesson", TotalLesson);
            return MsSqlMapperHepler.SqlWithParamsSingle<bool>(sb.ToString(), parameters, DBKeys.PRX);
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
            sb.Append("select count(ClassID) from ClassList   ");
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
        public static int GetClassList_num(string ClassID, string TimePeriod, DateTime Start_Date, DateTime End_Date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select COUNT(Classid) from ClassList   ");
            sb.Append(" where Classid=@Classid and   TimePeriod=@TimePeriod ");
            sb.Append(" and ClassIndex between  @Start_Date and @End_Date");
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", ClassID);
            parameters.Add("@TimePeriod", TimePeriod);
            parameters.Add("@Start_Date", Start_Date);
            parameters.Add("@End_Date", End_Date);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sb.ToString(), parameters, DBKeys.PRX);
        }


         

    }
}
