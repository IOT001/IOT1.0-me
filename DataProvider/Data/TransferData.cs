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
    public class TransferData
    {
        /// <summary>
        /// 报销列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Transfer> GetTransferList(TransferListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Transfer ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.JName))//甲方
                sb.AppendFormat(" and TeacherID = '{0}' ", search.JName);
            if (!string.IsNullOrWhiteSpace(search.YName))//乙方
                sb.AppendFormat(" and TeacherID = '{0}' ", search.YName);

            if (!string.IsNullOrWhiteSpace(search.StateID))//状态
                sb.AppendFormat(" and StateID = '{0}' ", search.StateID);

            if (!string.IsNullOrWhiteSpace(search.CreateTime_start))//开班时间
                sb.AppendFormat(" and CreateTime > = '{0}' ", search.CreateTime_start);
            if (!string.IsNullOrWhiteSpace(search.CreateTime_end))//结束时间
                sb.AppendFormat(" and CreateTime <= '{0}' ", search.CreateTime_end);


            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Transfer>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Transfer>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Transfer GetTransferByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<Transfer>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddTransfer(Transfer Rb)
        {
            return MsSqlMapperHepler.Insert<Transfer>(Rb, DBKeys.PRX);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateTransfer(Transfer Rb)
        {
            Transfer Rbs = TransferData.GetTransferByID(Rb.ID);//获取对象
            Cloner<Transfer, Transfer>.CopyTo(Rb, Rbs);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Rbs, DBKeys.PRX);

        }




 


        #region 获取字典vw_Enroll列表

        /// <summary>
        /// 甲方获取所报班级的名称
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<vw_Enroll> Getvw_EnrollList(string StudentID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,ClassName as name,ClassHour,UsedHour  from vw_Enroll");
            sb.Append(" where TeachTypeID<>1 and  StudentID=@StudentID");
            var parameters = new DynamicParameters(); 
            parameters.Add("@StudentID", StudentID);
            return MsSqlMapperHepler.SqlWithParams<vw_Enroll>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion


        #region 获取字典vw_Enroll列表

        /// <summary>
        ///乙方获取所报班级的名称
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetClasses(string StudentID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ClassID as ID,ClassName+'-已报'as Name from vw_Enroll");
            sb.Append(" where   StudentID=@StudentID");
            sb.Append("  union ");
            sb.Append(" select ID,ClassName as Name from vw_Classes");
            sb.Append(" where StateID<>4 and ID not in ");
            sb.Append(" ( select ClassID  from vw_Enroll  ");
            sb.Append(" where     StudentID=@StudentID ) ");
            var parameters = new DynamicParameters();
            parameters.Add("@StudentID", StudentID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion


    }

}
