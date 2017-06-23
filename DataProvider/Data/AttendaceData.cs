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
    public class AttendaceData
    {
        /// <summary>
        /// 学员考勤-列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_ClassAttendanceList> GetButtonList(AttendanceSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_ClassAttendanceList ";//表或者视图
            orderby = "ClassDate";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
           

            if (!string.IsNullOrWhiteSpace(search.className))//按钮中文名称
                sb.AppendFormat(" and ClassName like '%{0}%' ", search.className);
            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_ClassAttendanceList>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_ClassAttendanceList>(list, search.CurrentPage, search.PageSize, allcount);
        }

        /// <summary>
        /// 获取对学生评价
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="classIndex"></param>
        /// <returns></returns>
        public static List<vw_StudentEvaluate> getStudentEvaluate(String classId, int classIndex)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_StudentEvaluate ";//表或者视图
            orderby = "StudentID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append("select * from vw_StudentEvaluate where ");
            sb.Append(" 1=1 ");
           

            if (!string.IsNullOrWhiteSpace(classId))//按钮中文名称
                sb.Append(" and ClassID = @ClassID");
            if (classIndex !=0)//按钮中文名称
                sb.Append(" and ClassIndex = @ClassIndex  ");
            sb.Append(" order by StudentID ");

            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);

            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classId);
            parameters.Add("@ClassIndex", classIndex);
            return MsSqlMapperHepler.SqlWithParams<vw_StudentEvaluate>(sb.ToString(), parameters, DBKeys.PRX);
    //        var list = CommonPage<vw_StudentEvaluate>.GetPageList(
    //out allcount, table, fields: fields, where: where.Trim(),
    //orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
    //        return new PagedList<vw_StudentEvaluate>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 保存学生的考勤
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static bool saveStudentEvalute(List<vw_StudentEvaluate> cls)
        {
            bool ret = false;

            try { 

            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();//事务开始

            foreach (vw_StudentEvaluate value in cls)
            {
                if (string.IsNullOrWhiteSpace(value.Evaluate)) continue;
                AttendanceRecord btnto = GetAttendanceRecordByID(value.ID);//获取对象
                btnto.Evaluate = value.Evaluate;
                MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
           }

            db.Commit(); //事务提交
            db.Dispose();  //资源释放
            ret = true;//新增成功
            }
            catch (Exception)
            {

                throw;
            }


            return ret;
        }

        /// <summary>
        /// 根据ID获取教师信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static AttendanceRecord GetAttendanceRecordByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<AttendanceRecord>(ID, DBKeys.PRX);
        }


        /// <summary>
        /// 获取学生的考勤情况
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="classIndex"></param>
        /// <returns></returns>
        public static List<AttendanceRecord> getStudentCheck(String classId, int classIndex)
        {

            /**
             *  string sql = @" select * from FollowRecord where APID = @APID";
            var parameters = new DynamicParameters();
            parameters.Add("@APID", apid);
             * 
             * */

           
           
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append("select a.*,c.Name as Name,c.BindPhone as Phone ,");
            sb.Append(" (b.ClassHour-b.UsedHour) as LeftHour from AttendanceRecord a, Enroll b, Students c ");
            sb.Append(" where a.StudentID=b.StudentID and b.StudentID = c.ID ");

            if (!string.IsNullOrWhiteSpace(classId))//按钮中文名称
                sb.Append(" and a.ClassID = @ClassID ");
            if (classIndex != 0)//按钮中文名称
                sb.Append(" and a.ClassIndex = @ClassIndex ");


            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classId);
            parameters.Add("@ClassIndex", classIndex);
            return MsSqlMapperHepler.SqlWithParams<AttendanceRecord>(sb.ToString(), parameters, DBKeys.PRX);
            //        var list = CommonPage<vw_StudentEvaluate>.GetPageList(
            //out allcount, table, fields: fields, where: where.Trim(),
            //orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            //        return new PagedList<vw_StudentEvaluate>(list, search.CurrentPage, search.PageSize, allcount);
        }

        /// <summary>
        /// 根据学生ID，classID,classIndex 获取学生单条考勤记录
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="classId"></param>
        /// <param name="classIndex"></param>
        /// <returns></returns>
        public static AttendanceRecord GetAttendanceRecordByStudentClass(string studentID, string classId, int classIndex)
        {
            String sql = "select * from AttendanceRecord where StudentID = @StudentID and ClassID = @ClassID and ClassIndex = @ClassIndex";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);
            dynamic.Add("@ClassIndex", classIndex);

            return MsSqlMapperHepler.SqlWithParamsSingle<AttendanceRecord>(sql, dynamic, DBKeys.PRX);
        }


        public static bool saveStudentAttendance(List<AttendanceRecord> ar)
        {
            bool ret = false;

            try{
                DBRepository db = new DBRepository(DBKeys.PRX);
                db.BeginTransaction();//事务开始

                foreach (AttendanceRecord value in ar)
                {
                    if (value == null) continue;
                    if (value.ClockTime == null && value.OutStatus < 1) continue;

                    AttendanceRecord btnto = AttendaceData.GetAttendanceRecordByStudentClass( value.StudentID,value.ClassID, value.ClassIndex);//获取对象
                    if (value.ClockTime != null)
                    {
                        btnto.ClockTime = value.ClockTime;
                        btnto.AttendanceTypeID = 2;//考勤正常
                       Enroll enroll= EnrollData.getEnrollByStudentClass(value.StudentID, value.ClassID);
                       if (enroll != null)
                       {
                           enroll.UsedHour++;
                           EnrollData.UpdateEnroll(enroll);
                       }
                    }
                    if (value.OutStatus > 0)
                    {
                        btnto.AttendanceTypeID = 3;//缺勤
                        btnto.OutStatus = value.OutStatus;
                    }
                    MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
               }

                db.Commit(); //事务提交
                db.Dispose();  //资源释放
                ret = true;//新增成功
            }
            catch (Exception)
            {

                throw;
            }


            return ret ;
        }
    }

}
