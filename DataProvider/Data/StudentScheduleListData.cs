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
    public class StudentScheduleListData
    {
       /// <summary>
       /// 分页获取学生列表
       /// </summary>
       /// <param name="search"></param>
       /// <returns></returns>
        public static PagedList<vw_AttendanceRecord> GetAttendanceRecordList(StudentScheduleSearchModel search)
       {
           string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
           fields = @"  * ";//输出字段
           table = @" vw_AttendanceRecord ";//表或者视图
           orderby = "ID";//排序信息
           StringBuilder sb = new StringBuilder();//构建where条件
           sb.Append(" 1=1 ");
           if (!string.IsNullOrWhiteSpace(search.Name)) //学员姓名
               sb.AppendFormat(" and Name like '%{0}%' ", search.Name);
           if (search.timeStart != null && search.timeEnd != null)//时间
               sb.AppendFormat(" and ClassDate between '{0}'  and  '{1}'", search.timeStart, search.timeEnd);
           where = sb.ToString();
           int allcount = 0;
           var list = CommonPage<vw_AttendanceRecord>.GetPageList(
   out allcount, table, fields: fields, where: where.Trim(),
   orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
           return new PagedList<vw_AttendanceRecord>(list, search.CurrentPage, search.PageSize, allcount);
       }
       /// <summary>
       /// 获取单条数据
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static AttendanceRecord GetAttendanceRecordByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<AttendanceRecord>(ID, DBKeys.PRX);
        }
      
       /// <summary>
       /// 保存
       /// </summary>
       /// <param name="btn"></param>
       /// <returns></returns>
        public static bool UpdateAttendanceRecord(AttendanceRecord att)
       {
           AttendanceRecord Stuto = StudentScheduleListData.GetAttendanceRecordByID(att.ID);//获取对象
           Cloner<AttendanceRecord, AttendanceRecord>.CopyTo(att, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
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
        /// 新增判断手机号是唯一
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static int BindPhone_insert(string BindPhone)
        {

            string strsql = "select id from Students where  BindPhone=@BindPhone";
            var parameters = new DynamicParameters(); 
            parameters.Add("@BindPhone", BindPhone);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(strsql.ToString(), parameters, DBKeys.PRX);



        }



        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static Students GetStudentsList()
        {

            string strsql = "select *from Students  order by CreateTime desc     ";
            var parameters = new DynamicParameters(); 
            return MsSqlMapperHepler.SqlWithParamsSingle<Students>(strsql.ToString(), parameters, DBKeys.PRX);
             

        }



       

    }
}
