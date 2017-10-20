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
   public class EnrollData
    {
        /// <summary>
        /// 新增报名记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Add(Enroll obj)
        {
            bool ret = false;
            try
            {
                MsSqlMapperHepler.Insert<Enroll>(obj, DBKeys.PRX);
                //Classes ca = ClassesData.GetClassesByID(obj.ClassID);
                ////ca.PresentEnroll = ca.PresentEnroll + 1;
                //MsSqlMapperHepler.Update<Classes>(ca,DBKeys.PRX);//报名数加1
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
       /// <summary>
       /// 批量新增报名记录
       /// </summary>
       /// <param name="list"></param>
       /// <returns></returns>
        public static bool AddList(List<Enroll> list)
        {
            bool ret = false;
            foreach (var obj in list)
            {
                int haveenroll = EnrollData.getEnrollByStuidCalssid(obj.StudentID, obj.ClassID);
                if (haveenroll > 0)
                {
                    throw new Exception("该学员已报名，不允许重复报名！");
                }
            }
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {
                            
                foreach (var obj in list)
                {
                    obj.ID = CommonData.DPGetTableMaxId("EN", "ID", "Enroll", 8,db);//走同一个事务
                    db.Insert<Enroll>(obj);
                    Appointment ap = db.GetById<Appointment>(obj.APID);
                    ap.ApStateID = 3;//已报名
                    db.Update<Appointment>(ap);

                    FundsFlow fl = new FundsFlow();//资金流水
                    fl.TypeID = 1;//类型1报名
                    fl.Amount = obj.Paid;
                    fl.KeyID = obj.ID;
                    fl.CreateTime = DateTime.Now;
                    fl.CreatorId = obj.CreatorId;
                    db.Insert(fl);

                    //Classes ca = ClassesData.GetClassesByID(obj.ClassID);
                    //ca.PresentEnroll = ca.PresentEnroll + 1;
                    //MsSqlMapperHepler.Update<Classes>(ca, DBKeys.PRX);//报名数加1
                }
                ret = true;
                db.Commit();
            }          
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }
        public static Enroll getEnrollByStudentClass(string studentID, string classId)
        {
            String sql = "select * from Enroll where StudentID = @StudentID and ClassID = @ClassID ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);

            return MsSqlMapperHepler.SqlWithParamsSingle<Enroll>(sql, dynamic, DBKeys.PRX);            
        }
       /// <summary>
       /// 跟进学员ID和班级ID查看报名，防止重复报名
       /// </summary>
       /// <param name="studentID"></param>
       /// <param name="classId"></param>
       /// <returns></returns>
        public static int getEnrollByStuidCalssid(string studentID, string classId)
        {
            String sql = "select count(*) from Enroll where StudentID = @StudentID and ClassID = @ClassID ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);

            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sql, dynamic, DBKeys.PRX);
        }
        public static Enroll GetEnrollByID(String ID)
        {
            return MsSqlMapperHepler.GetOne<Enroll>(ID, DBKeys.PRX);
        }

        /// <summary>
        /// 更新 教师信息
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateEnroll(Enroll btn)
        {
            Enroll btnto = EnrollData.GetEnrollByID(btn.ID);//获取对象
            Cloner<Enroll, Enroll>.CopyTo(btn, btnto);
            //Cloner<Teachers, Teachers>.CopyTo(btn, btnto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
        }




        /// <summary>
        /// 分页获取报名列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Enroll> GeEnrollList(EnrollListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Enroll ";//表或者视图
            orderby = "ID desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.ApName))//姓名
                sb.AppendFormat(" and name like '%{0}%' ", search.ApName);
            if (!string.IsNullOrWhiteSpace(search.Enroll_StudentID))//学号
            {
                sb.AppendFormat(" and studentid like '%{0}%' ", search.Enroll_StudentID);
            }
            if (!string.IsNullOrWhiteSpace(search.ClassID))//班级号
            {
                sb.AppendFormat(" and ClassID = '{0}' ", search.ClassID);
            }
            if (search.TeachTypeID != null)//授课方式
            {
                sb.AppendFormat(" and TeachTypeID = '{0}' ", search.TeachTypeID);
            }
            if (!string.IsNullOrWhiteSpace(search.Name))//正式学员的姓名
            {
                sb.AppendFormat(" and Name like '%{0}%' ", search.Name);
            }
            if (!string.IsNullOrWhiteSpace(search.BindPhone))//正式学员的姓名
            {
                sb.AppendFormat(" and BindPhone like '%{0}%' ", search.BindPhone);
            }
            if (!string.IsNullOrWhiteSpace(search.islesson))//正式学员的姓名
            {
                if (search.islesson == "1")//试听
                {
                    sb.AppendFormat(" and TeachTypeID = 1 ");
                }
                else
                {
                    sb.AppendFormat(" and TeachTypeID > 1 ");
                }
            }


            if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
                sb.AppendFormat(" and ComCode = '{0}' ", search.ComCode);

            where = sb.ToString();

            int allcount = 0;
            var list = CommonPage<vw_Enroll>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Enroll>(list, search.CurrentPage, search.PageSize, allcount);

        }
       /// <summary>
       /// 跟进预约单获取报名记录，用于打印票据
       /// </summary>
       /// <param name="apid"></param>
       /// <returns></returns>
        public static List<vw_Enroll> GetEnrollPrintByApid(string apid)
        {
            String sql = "select * from vw_Enroll where APID = @APID  and TeachTypeID > 1";
            var dynamic = new DynamicParameters();
            dynamic.Add("@APID", apid);


            return MsSqlMapperHepler.SqlWithParams<vw_Enroll>(sql, dynamic, DBKeys.PRX);  
        }
       /// <summary>
       /// 获取单据配置信息
       /// </summary>
       /// <returns></returns>
        public static BillConfig GetOneBillConfig()
        {
            return MsSqlMapperHepler.GetOne<BillConfig>(1, DBKeys.PRX);
        }

        /// <summary>
        /// 批量新增报名审核记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool AddEnrollAuditList(List<EnrollAudit> list)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {

                foreach (var obj in list)
                {
                    
                    db.Insert<EnrollAudit>(obj);
                    Appointment ap = db.GetById<Appointment>(obj.APID);
                    ap.ApStateID = 5;//待审核
                    db.Update<Appointment>(ap);
                }
                ret = true;
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }
        /// <summary>
        /// 转让审核,乙方有报名记录
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool TransferAudit1(DataProvider.Entities.Enroll a, DataProvider.Entities.Enroll b, string loginid, Transfer rb)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {
                DataProvider.Entities.Enroll j = EnrollData.GetEnrollByID(a.ID);//甲方原始报名记录
                TransferRecord tr1 = new TransferRecord();//甲方的转移记录
                tr1.StudentID = j.StudentID;
                tr1.BeforeHours = j.ClassHour - j.UsedHour;
                tr1.AfterHours = a.ClassHour - a.UsedHour;
                tr1.TypeID = 1;//转让产生的
                tr1.CreateTime = DateTime.Now;
                tr1.CreatorId = loginid;
                tr1.Remark = "转班：原报名号:" + a.ID + " 原班级号：" + a.ClassID + " 转至班级：" + b.ClassID;
                db.Insert(tr1);
                db.Update(a);//扣除课时


                DataProvider.Entities.Enroll y = EnrollData.GetEnrollByID(b.ID);//乙方原始报名记录
                TransferRecord tr2 = new TransferRecord();//甲方的转移记录
                tr2.StudentID = y.StudentID;
                tr2.BeforeHours = y.ClassHour - y.UsedHour;
                tr2.AfterHours = y.ClassHour - y.UsedHour + rb.TranHour;
                tr2.TypeID = 1;//转让产生的
                tr2.CreateTime = DateTime.Now;
                tr2.CreatorId = loginid;
                tr2.Remark = "转班：来自报名号:" + a.ID + " 来自班级号：" + a.ClassID + " 转至班级：" + b.ClassID;
                db.Insert(tr2);
                y.ClassHour = y.ClassHour + rb.TranHour;//增加乙方的课时
                db.Update(y);

                rb.StateID = 2;
                rb.ApprovedBy = loginid;
                rb.ApprovedTime = DateTime.Now;
                db.Update(rb);
                ret = true;
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }

       /// <summary>
       /// 转让课程，乙方没有报过名
       /// </summary>
       /// <param name="a"></param>
       /// <param name="b"></param>
       /// <param name="loginid"></param>
       /// <returns></returns>
        public static bool TransferAudit2(DataProvider.Entities.Enroll a, DataProvider.Entities.Enroll b, string loginid, Transfer rb)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {
                DataProvider.Entities.Enroll j = EnrollData.GetEnrollByID(a.ID);//甲方原始报名记录
                TransferRecord tr1 = new TransferRecord();//甲方的转移记录
                tr1.StudentID = j.StudentID;
                tr1.BeforeHours = j.ClassHour - j.UsedHour;
                tr1.AfterHours = a.ClassHour - a.UsedHour;
                tr1.TypeID = 1;//转让产生的
                tr1.CreateTime = DateTime.Now;
                tr1.CreatorId = loginid;
                tr1.Remark = "转班：原报名号:" + a.ID + " 原班级号：" + a.ClassID + " 转至班级：" + b.ClassID;
                db.Insert(tr1);
                db.Update(a);//扣除课时


        
                TransferRecord tr2 = new TransferRecord();//乙方的转移记录
                tr2.StudentID = b.StudentID;
                tr2.BeforeHours = 0;
                tr2.AfterHours = b.ClassHour - b.UsedHour;
                tr2.TypeID = 1;//转让产生的
                tr2.CreateTime = DateTime.Now;
                tr2.CreatorId = loginid;
                tr2.Remark = "转班：来自报名号:" + a.ID + " 来自班级号：" + a.ClassID + " 转至班级：" + b.ClassID;

                db.Insert(tr2);
                db.Insert(b);

                rb.StateID = 2;
                rb.ApprovedBy = loginid;
                rb.ApprovedTime = DateTime.Now;
                db.Update(rb);

                ret = true;
                db.Commit();
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
