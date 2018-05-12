using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public static int AddList(List<Enroll> list)
        {
            int ret = 0;//初始
            foreach (var obj in list)
            {
                int haveenroll = EnrollData.getEnrollByStuidCalssid(obj.StudentID, obj.ClassID);
                if (haveenroll > 0)
                {
                    ret = -1;//重复报名
                    return ret;
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
                ret = 1;//成功
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
        /// 更新 Enroll信息
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool Update(Enroll obj)
        {
            bool ret = false;
            try
            {
                MsSqlMapperHepler.Update<Enroll>(obj, DBKeys.PRX);
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
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
            if (!string.IsNullOrWhiteSpace(search.Name))//正式学员的姓名或学号
            {
                sb.AppendFormat(" and (Name like '%{0}%' or studentid like '%{0}%')", search.Name);
            }
            if (!string.IsNullOrWhiteSpace(search.BindPhone))//手机号码
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
            {
                sb.AppendFormat(" and ComCode = '{0}' ", search.ComCode);
            }


            if (!string.IsNullOrWhiteSpace(search.Large))//剩余课时大于
            {
                sb.AppendFormat(" and RemainClassHour >= '{0}' ", search.Large);


            }
            if (!string.IsNullOrWhiteSpace(search.Small))//剩余课时小于
            {
                sb.AppendFormat(" and RemainClassHour <= '{0}' ", search.Small);

            }

            if (!string.IsNullOrWhiteSpace(search.timeStart))//开班时间
            {
                sb.AppendFormat(" and CreateTime >= '{0}' ", search.timeStart);  
            }

            if (!string.IsNullOrWhiteSpace(search.timeEnd)) //结束时间
            {
                sb.AppendFormat(" and CreateTime <= '{0}' ", search.timeEnd); 
            }

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
       /// 获取获取试听，判断是否有重复试听记录
       /// </summary>
       /// <param name="apid"></param>
       /// <returns></returns>
        public static List<vw_Enroll> GetEnrollPrintByApidAndClassid(string apid,string classid)
        {
            String sql = "select * from vw_Enroll where APID = @APID  and ClassID = @ClassID";
            var dynamic = new DynamicParameters();
            dynamic.Add("@APID", apid);
            dynamic.Add("@ClassID", classid);

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
        /// <summary>
        /// 手动调整剩余课时
        /// </summary>
        /// <param name="ENID"></param>
        /// <param name="AdjustNum"></param>
        /// <returns></returns>
        public static bool AdjustLeftHour(string ENID, int AdjustNum,string loginid)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            try
            {
                db.BeginTransaction();
                Enroll en = db.GetById<Enroll>(ENID);

                TransferRecord tr = new TransferRecord();//添加日志记录
                tr.StudentID = en.StudentID;
                tr.BeforeHours = en.ClassHour - en.UsedHour;
                tr.AfterHours = en.ClassHour - en.UsedHour + +decimal.Parse(AdjustNum.ToString());
                tr.TypeID = 3;//自定义调整课时
                tr.CreateTime = DateTime.Now;
                tr.CreatorId = loginid;
                tr.ENID = en.ID;
                tr.ClassID = en.ClassID;
                db.Insert(tr);

                en.UsedHour = en.UsedHour - decimal.Parse(AdjustNum.ToString());//调整剩余课时
                en.UpdateTime = DateTime.Now;
                en.UpdatorId = loginid;
                db.Update(en);

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

        /// <summary>
        /// 根据报名号，返回课时消耗记录
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static List<TransferRecord> GetHoursLogByENID(string enid)
        {
            string strsql = "select a.*,dbo.getDicNameByID(23,a.TypeID) AS TypeName from [TransferRecord] a where a.ENID = '" + enid + "'";
            return MsSqlMapperHepler.SqlWithParams<TransferRecord>(strsql, null, DBKeys.PRX);
        }



        /// <summary>
        /// 修改enroll表的状态 
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateEnroll_ed(Enroll enl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update enroll set StateID=@StateID,UpdatorId=@UpdatorId,UpdateTime=@UpdateTime  ");
            sb.Append(" where ID=@ID ");
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateTime", enl.UpdateTime);
            parameters.Add("@UpdatorId", enl.UpdatorId);
            parameters.Add("@StateID", enl.StateID);
            parameters.Add("@ID", enl.ID); 
            return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
        }

        /// <summary>
        /// 完结报名，清空课时
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static bool FinishEnroll(string enid, string loginid)
        {
            if (string.IsNullOrEmpty(enid))
            {
                return false;
            }
            DBRepository db = new DBRepository(DBKeys.PRX);
            try
            {
                db.BeginTransaction();
                Enroll en = db.GetById<Enroll>(enid);
                if (en == null)
                {
                    return false;
                }
                TransferRecord tr = new TransferRecord();//添加日志记录
                tr.StudentID = en.StudentID;
                tr.BeforeHours = en.ClassHour - en.UsedHour;
                tr.AfterHours = 0;//清空剩余课时
                tr.TypeID = 7;//ERP点完成按钮
                tr.CreateTime = DateTime.Now;
                tr.CreatorId = loginid;
                tr.ENID = en.ID;
                tr.ClassID = en.ClassID;
                db.Insert(tr);

                en.StateID = 6;//状态6是已完成报名，需要清空剩余课时
                en.UsedHour = en.ClassHour;//清空课时
                en.UpdateTime = DateTime.Now;
                en.UpdatorId = loginid;
                db.Update(en);
                db.Commit();
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
        }





        /// <summary>
        /// 导出到Excel表格
        /// </summary>
        /// <returns></returns>
        public static DataTable DPExportToExcel(string Name, string BindPhone, string timeStart, string timeEnd, string ComCode, string Large, string Small, string islesson)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PrxConnectionString"].ToString()))//创建连接字符串，因为连接的库不同
                {
                    string sql = "";
                    sql = " SELECT CompName as '校区名称',CreateTime as '报名日期',Name as '学员姓名',StudentID as '学号',BindPhone as '学员电话',ClassName as '报名班级',ClassID as '班级编号',ClassHour as '报名课时',UsedHour as '消耗课时',RemainClassHour as '剩余课时',Paid as '报名费用',StateName as '状态' FROM [vw_Enroll] where 1=1";


                    if (!string.IsNullOrWhiteSpace(islesson))//正式学员的姓名
                    {
                        if (islesson == "1")//试听
                        {
                            sql += " and TeachTypeID = 1";  
                        }
                        else
                        {
                            sql += " and TeachTypeID > 1  ";  
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Name))//正式学员的姓名或学号
                    {
                        sql += " and (Name like '%" + Name + "%' or studentid like  '%" + Name + "%')"; 
                    }

                    if (!string.IsNullOrWhiteSpace(BindPhone))//联系电话
                    {
                        sql += " and BindPhone like  '%" + BindPhone + "%'";
                    }

                    if (!string.IsNullOrWhiteSpace(timeStart))//开班时间
                    {
                        sql += "and CreateTime >=  '" + timeStart + "'";
                    }

                    if (!string.IsNullOrWhiteSpace(timeEnd)) //结束时间
                    {
                        sql += "and CreateTime <=  '" + timeEnd + "'";
                    }


                    if (!string.IsNullOrWhiteSpace(ComCode))//校区
                    {
                        sql += "and ComCode =  '" + ComCode + "'";  
                    }


                    if (!string.IsNullOrWhiteSpace(Large))//剩余课时大于
                    {
                        sql += "and RemainClassHour >=  '" + Large + "'";  

                    }

                    if (!string.IsNullOrWhiteSpace(Small))//剩余课时小于
                    {
                        sql += "and RemainClassHour <=  '" + Small + "'"; 
                    }




                    sql += "  order by ID desc ";


                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "转换的过程中发生了错误!");
            }
        }












        /// <summary>
        /// 调整报名课时
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static bool AjustmentEnroll(string ENID, int ClassHour, decimal Price, string userid)
        {
       
            DBRepository db = new DBRepository(DBKeys.PRX);
            try
            {
                db.BeginTransaction();
                Enroll en = db.GetById<Enroll>(ENID);
                if (en == null)
                {
                    return false;
                }
                //增加调整课时日志
                TransferRecord tr = new TransferRecord();//添加日志记录
                tr.StudentID = en.StudentID;
                tr.BeforeHours = en.ClassHour - en.UsedHour;
                tr.AfterHours = en.ClassHour - en.UsedHour + +decimal.Parse(ClassHour.ToString());
                tr.TypeID = 8;//调整报名课时
                tr.CreateTime = DateTime.Now;
                tr.CreatorId = userid;
                tr.ENID = en.ID;
                tr.ClassID = en.ClassID;
                db.Insert(tr);

                //增加资金日志
                FundsFlow fundsflow = new FundsFlow();//添加日志记录
                fundsflow.TypeID=5;
                fundsflow.Amount = Price;
                fundsflow.KeyID=ENID;
                fundsflow.CreatorId=userid;
                fundsflow.CreateTime = DateTime.Now;
                fundsflow.StateID = 0;
                db.Insert(fundsflow);

                //增加报名费用
                en.Price =en.Price + Price;//金额
                en.Paid = en.Paid + Price;
                en.ClassHour =en.ClassHour + ClassHour;//课时
                en.UpdateTime = DateTime.Now;
                en.UpdatorId = userid;
                db.Update(en);


                db.Commit();
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
        }






    }
}
