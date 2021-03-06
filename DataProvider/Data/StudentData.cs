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
    public class StudentData
    {
       /// <summary>
       /// 分页获取学生列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_Students> GetStudentList(StudentListSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_Students ";//表或者视图
           orderby = "ID desc";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 ");
           if (!string.IsNullOrWhiteSpace(search.Name))//姓名
               sb.AppendFormat(" and (Name like '%{0}%' or  BindPhone like '%{0}%' )", search.Name);
           //if (search.CreateTime_end != null && search.CreateTime_start !=null)//时间
           //    sb.AppendFormat(" and CreateTime between '{0}'  and  '{1}'", search.CreateTime_start,search.CreateTime_end);

           if (!string.IsNullOrWhiteSpace(search.CreateTime_start))//开班时间
               sb.AppendFormat(" and CreateTime > = '{0}' ", search.CreateTime_start);
           if (!string.IsNullOrWhiteSpace(search.CreateTime_end))//结束时间
               sb.AppendFormat(" and CreateTime <= '{0}' ", search.CreateTime_end);


           if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
               sb.AppendFormat(" and [ComCode] = '{0}' ", search.ComCode);


           if (!string.IsNullOrWhiteSpace(search.StudentDicItemID))//学员状态
               sb.AppendFormat(" and  StateID = '{0}' ", search.StudentDicItemID);

           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_Students>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_Students>(list, search.CurrentPage, search.PageSize, allcount);
       }
       /// <summary>
       /// 获取单条数据
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static Students GetStudentsByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<Students>(ID, DBKeys.PRX);
        }
       /// <summary>
       /// 新增,返回的是主键
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static string AddStudent(Students Stu, SYSAccount sys, SYSAccountRole sysR)
       {
           string ret;
           DBRepository db = new DBRepository(DBKeys.PRX);
           db.BeginTransaction();//事务开始  
           try
           {
               ret = db.Insert<Students>(Stu);  //添加学生表
               int max = db.Insert<SYSAccount>(sys);  //添加用户表

               sysR.AR_AccountId =max;
               //sysR.AR_AccountId = max.ContainsValue(0);
               db.Insert<SYSAccountRole>(sysR);  //添加权限表
               db.Commit(); //事务提交  
               db.Dispose();  //资源释放

           }
           catch (Exception ex)
           {
               db.Rollback();
               db.Dispose();
               throw new Exception(ex.Message + "。" + ex.InnerException.Message);
           }
           return ret; 
       }


        /// <summary>
        /// 获取SYS_Account表最大的主键
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static int Max_ACC_Id()
        {

            string strsql = " select isnull(max(ACC_Id),1)as  ID from  SYS_Account";
            var parameters = new DynamicParameters();
            parameters.Add("@id"); 
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql.ToString(), parameters, DBKeys.PRX);



        }



       /// <summary>
       /// 保存
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static bool UpdateStudent(Students Stu)
       {
           Students Stuto = StudentData.GetStudentsByID(Stu.ID);//获取对象
           Cloner<Students, Students>.CopyTo(Stu, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
           return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

       }

 



           /// <summary>
        /// 修改判断手机号是唯一
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static int BindPhone_update(string id, string BindPhone)
        {

            string strsql = "select id from Students where id <> @id and BindPhone = @BindPhone";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@BindPhone", BindPhone);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql.ToString(), parameters, DBKeys.PRX);



        }

        /// <summary>
        /// 新增判断手机号+姓名是唯一
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static string BindPhone_insert(string BindPhone,string name)
        {

            string strsql = "select id from Students WITH(NOLOCK) where  BindPhone=@BindPhone and Name = @Name";
            var parameters = new DynamicParameters(); 
            parameters.Add("@BindPhone", BindPhone);
            parameters.Add("@Name", name);
            return MsSqlMapperHepler.SqlWithParamsSingle<string>(strsql.ToString(), parameters, DBKeys.PRX);



        }


        /// <summary>
        ///  根据预约单号ID的主键 获取列表vw_Appointment
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static vw_Appointment Getvw_AppointmentList(string ID)
        {

            string strsql = "select top 1 * from vw_Appointment WITH(NOLOCK) where  ID=@ID ";
            var parameters = new DynamicParameters();
            parameters.Add("@ID", ID); 
            return MsSqlMapperHepler.SqlWithParamsSingle<vw_Appointment>(strsql.ToString(), parameters, DBKeys.PRX);



        }




        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static Students GetStudentsOne(string Mosaic)
        {

            string strsql = "select top 1 * from Students  WHERE ID like  @Mosaic + '%'   order by CreateTime desc ";
            var parameters = new DynamicParameters();
            parameters.Add("@Mosaic", Mosaic);
            return MsSqlMapperHepler.SqlWithParamsSingle<Students>(strsql.ToString(), parameters, DBKeys.PRX);
             

        }

        /// <summary>
        /// 根据绑定的账号获取一名学员信息
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public static Students GetStudentByAccountID(string AccountID)
        {
            string strsql = "select TOP 1 * from Students where BindAccount = @AccountID ";
            var parameters = new DynamicParameters();
            parameters.Add("@AccountID", AccountID);
            return MsSqlMapperHepler.SqlWithParamsSingle<Students>(strsql.ToString(), parameters, DBKeys.PRX);
        }

        /// <summary>
        /// 学员信息和预约单绑定
        /// </summary>
        /// <param name="studi"></param>
        /// <param name="apid"></param>
        public static void BindStudentforAP(string studi, string apid)
        {
            Appointment ap = AppointmentData.GetOneByID(apid);
            ap.ApStudentID = studi;
            MsSqlMapperHepler.Update<Appointment>(ap, DBKeys.PRX);
        }
    }
}
