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
    public class EnrollAuditInfoData
    {
        /// <summary>
        /// 返回所有的市场资源/预约单信息   ,EnrollAuditList页面的 微信页面
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_EnrollAudit> GetEnrollAuditInfo(EnrollAuditInfoSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_EnrollAudit ";//表或者视图
            orderby = "CreateTime desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.APID)) //判断值。
                sb.AppendFormat(" and APID = '{0}' ", search.APID);
            if (!string.IsNullOrWhiteSpace(search.DiscountID)) //判断值。
                sb.AppendFormat(" and DiscountID = {0} ", search.DiscountID);
            if (!string.IsNullOrWhiteSpace(search.StateID)) //判断值。
                sb.AppendFormat(" and StateID = {0} ", search.StateID);
             
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_EnrollAudit>.GetPageList(
            out allcount, table, fields: fields, where: where.Trim(),
            orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_EnrollAudit>(list, search.CurrentPage, search.PageSize, allcount);
        }


        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int Audited(EnrollAudit erau)
        {
            int ret=0;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();//事务开始  
            try
            {
                UpdateEnrollAudit(erau.APID, erau.StateID,erau.UpdateTime, erau.UpdatorId, db);
            



                string sql = "select * from EnrollAudit where APID=@APID";
                var parameters = new DynamicParameters();
                parameters.Add("@APID", erau.APID); 
                List<EnrollAudit> List = db.Query<EnrollAudit>(sql, parameters).ToList();

                Enroll er = new Enroll();
                foreach (var en in List)
                {
                    er.ID = CommonData.DPGetTableMaxId("EN", "ID", "Enroll", 8,db);
                    er.APID = en.APID;
                    er.ApprovedBy = en.ApprovedBy;
                    er.ApprovedRemark = en.ApprovedRemark;
                    er.ApprovedTime = en.ApprovedTime;
                    er.ClassHour = en.ClassHour;
                    er.ClassID = en.ClassID;
                    er.CreateTime = en.CreateTime;
                    er.CreatorId = en.CreatorId;
                    er.DiscountID = en.DiscountID;
                    er.DiscountPrice = en.DiscountPrice; 
                    er.Paid = en.Paid;
                    er.Price = en.Price;
                    er.Remark = en.Remark;
                    er.StateID = en.StateID;
                    er.StudentID = en.StudentID;
                    er.UpdateTime = en.UpdateTime;
                    er.UpdatorId = en.UpdatorId;
                    er.UsedHour = en.UsedHour;
                   // er.CollectionRec = en.CollectionRec;
                    
                    db.Insert<Enroll>(er);  //复制 EnrollAudit表数据到报名表

                    FundsFlow fl = new FundsFlow();//资金流水
                    fl.TypeID = 1;//类型1报名
                    fl.Amount = er.Paid;
                    fl.KeyID = er.ID;
                    fl.CreateTime = DateTime.Now;
                    fl.CreatorId = er.CreatorId;
                    db.Insert(fl);
                }
                ret = UpdateAppointment(erau.APID, erau.StateID, erau.UpdateTime, erau.UpdatorId, db);  //最后修改Appointment状态为3，3为已报名


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
        /// 审核不通过
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AuditNotThrough(EnrollAudit erau, int AppointmentApStateID)
        {
            int ret = 0;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();//事务开始  
            try
            {
                UpdateEnrollAudit(erau.APID, erau.StateID, erau.UpdateTime, erau.UpdatorId, db);


                ret = UpdateAppointment(erau.APID, AppointmentApStateID, erau.UpdateTime, erau.UpdatorId, db);  //最后修改Appointment状态为3，3为已报名

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
        /// 修改EnrollAudit表的StateID
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateEnrollAudit(string APID,int StateID, DateTime ? UpdateTime, string UpdatorId, DBRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update EnrollAudit set StateID=@StateID,UpdateTime=@UpdateTime,UpdatorId=@UpdatorId ");
            sb.Append(" where APID=@APID  and DiscountID=-1 and StateID= 2 ");
            var parameters = new DynamicParameters();
            parameters.Add("@APID", APID);
            parameters.Add("@StateID", StateID); 
            parameters.Add("@UpdateTime", UpdateTime);
            parameters.Add("@UpdatorId", UpdatorId); 
            return db.Execute(sb.ToString(), parameters);

        }



        /// <summary>
        /// 修改Appointment表的ApStateID
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateAppointment(string ID,int ApStateID, DateTime? UpdateTime, string UpdatorId, DBRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update Appointment set ApStateID=@ApStateID,UpdateTime=@UpdateTime,UpdatorId=@UpdatorId ");
            sb.Append(" where ID=@ID ");
            var parameters = new DynamicParameters();
            parameters.Add("@ID", ID);
            parameters.Add("@ApStateID", ApStateID);
            parameters.Add("@UpdateTime", UpdateTime);
            parameters.Add("@UpdatorId", UpdatorId);
            return db.Execute(sb.ToString(), parameters);

        }



    }
}
