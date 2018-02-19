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
    public class AttendaceData
    {
        /// <summary>
        /// 学员考勤-列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_ClassAttendanceList> GetClassAttendanceList(AttendanceSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_ClassAttendanceList ";//表或者视图
            orderby = "ClassDate";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");



            if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
                sb.AppendFormat(" and [ComCode] = '{0}' ", search.ComCode);

            if (!string.IsNullOrWhiteSpace(search.className))//按钮中文名称
                sb.AppendFormat(" and ClassName like '%{0}%' ", search.className);
            //if (search.timeStart != null && search.timeEnd != null)//时间
            //    sb.AppendFormat(" and ClassDate between '{0}'  and  '{1}'", search.timeStart, search.timeEnd);

            if (!string.IsNullOrWhiteSpace(search.timeStart))//开班时间
                sb.AppendFormat(" and ClassDate > = '{0}' ", search.timeStart);
            if (!string.IsNullOrWhiteSpace(search.timeEnd))//结束时间
                sb.AppendFormat(" and ClassDate <= '{0}' ", search.timeEnd);

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
            DBRepository db = new DBRepository(DBKeys.PRX);
            try { 

          
            db.BeginTransaction();//事务开始

            foreach (vw_StudentEvaluate value in cls)
            {
                if (string.IsNullOrWhiteSpace(value.Evaluate)) continue;
                AttendanceRecord btnto = GetAttendanceRecordByID(value.ID);//获取对象
                btnto.Evaluate = value.Evaluate;
                //MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
                db.Update(btnto);
           }

            db.Commit(); //事务提交
            db.Dispose();  //资源释放
            ret = true;//新增成功
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

            //   
            //sb.Append("select a.*,c.Name as Name,c.BindPhone as Phone ,");
            //sb.Append(" (b.ClassHour-b.UsedHour) as LeftHour from AttendanceRecord a, Enroll b, Students c ");
            //sb.Append(" where a.StudentID=b.StudentID and b.StudentID = c.ID ");
            //

            sb.Append(@" select  a.[ID]
              ,b.[StudentID]
              ,a.[ClassID]
              ,a.[ClassIndex]
              ,a.[ClockTime]
              ,a.[AttendanceTypeID]
              ,a.[AttendanceWayID]
              ,a.[Evaluate]
              ,a.[Remark]
              ,a.[CreateTime]
              ,a.[CreatorId]
              ,a.[OutStatus]
              ,a.[UpdateTime]
              ,a.[UpdatorId],
              c.Name as Name,
              c.BindPhone as Phone, 
              (b.ClassHour-b.UsedHour) as LeftHour 
              FROM  Enroll b  
            INNER JOIN ClassList e ON b.ClassID =e.ClassID
            inner join AttendanceRecord a  on a.StudentID=b.StudentID  AND a.ClassID=b.ClassID  AND a.ClassIndex = e.ClassIndex
            LEFT join  Students c   on  b.StudentID = c.ID 
            WHERE 1=1 and b.StateID <> 6  and b.StateID <> 5 ");//去掉了冻结和完结的人数

            if (!string.IsNullOrWhiteSpace(classId))//按钮中文名称
                sb.Append(" and b.ClassID = @ClassID ");
            if (classIndex != 0)//按钮中文名称
                sb.Append(" and e.ClassIndex = @ClassIndex ");


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
            String sql = "select * from AttendanceRecord  WITH(NOLOCK)  where StudentID = @StudentID and ClassID = @ClassID and ClassIndex = @ClassIndex";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);
            dynamic.Add("@ClassIndex", classIndex);

            return MsSqlMapperHepler.SqlWithParamsSingle<AttendanceRecord>(sql, dynamic, DBKeys.PRX);
        }

        /// <summary>
        /// 保存考勤记录，正常考勤扣除课时
        /// </summary>
        /// <param name="ar"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool saveStudentAttendance(List<AttendanceRecord> ar, string userid)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            try{


               
                db.BeginTransaction();//事务开始

                foreach (AttendanceRecord value in ar)
                {
                    if (value == null) continue;
                    AttendanceRecord btnto = AttendaceData.GetAttendanceRecordByStudentClass( value.StudentID,value.ClassID, value.ClassIndex);//获取对象

                    if (value.ClockTime != null)//正常打卡扣除课时
                    {
                       
                        Enroll enroll= EnrollData.getEnrollByStudentClass(value.StudentID, value.ClassID);
                        if (enroll != null)
                        {
                            //-----添加课时变化日志记录 begin
                            TransferRecord tr = new TransferRecord();//添加课时变化日志记录
                            tr.StudentID = enroll.StudentID;
                            tr.BeforeHours = enroll.ClassHour - enroll.UsedHour;
                            tr.AfterHours = enroll.ClassHour - enroll.UsedHour - 1;
                            tr.TypeID = 5;//ERP考勤操作
                            tr.CreateTime = DateTime.Now;
                            tr.CreatorId = userid;
                            tr.ENID = enroll.ID;
                            tr.ClassID = enroll.ClassID;
                            db.Insert(tr);
                            //-----添加课时变化日志记录 end


                            enroll.UsedHour = enroll.UsedHour + 1;
                            db.Update(enroll);

                            

                            Students s = StudentData.GetStudentsByID(value.StudentID);//获取学员
                            if (s.StateID != null && (s.StateID.Value == 1 || s.StateID.Value == 3))//冻结和未读状态下
                            {
                                s.StateID = 2;//改成在读
                                db.Update<Students>(s);
                            }
                            ClassList cl = ClassListData.GetOneByid(value.ClassID, value.ClassIndex);
                            cl.StateID = 2;//课时状态变成已上
                            db.Update<ClassList>(cl);
                        }
                        else
                        {
                            throw new Exception("获取报名记录错误");
                        }


                        //插入考勤记录---------------------------------
                        if (btnto == null)
                        {
                            btnto = new AttendanceRecord();
                            btnto.OutStatus = 0;
                            btnto.ClockTime = value.ClockTime;
                            btnto.AttendanceTypeID = 2;//考勤正常
                            btnto.CreateTime = DateTime.Now;
                            btnto.CreatorId = userid;
                            btnto.AttendanceWayID = 3;//工作人员操作
                            btnto.ClassID = value.ClassID;
                            btnto.ClassIndex = value.ClassIndex;
                            btnto.StudentID = value.StudentID;
                            btnto.Remark = "更新考勤记录，识别成功,当前剩余课时：" + (enroll.ClassHour - enroll.UsedHour).ToString();
                            db.Insert(btnto);//新增考勤
                        }
                        else//如果之前有考勤记录了，且未打卡则更新打卡
                        {
                            if (btnto.ClockTime == null)//未打卡
                            {
                                btnto.OutStatus = 0;
                                btnto.ClockTime = value.ClockTime;
                                btnto.AttendanceTypeID = 2;//考勤正常
                                btnto.UpdateTime = DateTime.Now;
                                btnto.UpdatorId = userid;
                                btnto.AttendanceWayID = 3;//工作人员操作
                                btnto.ClassID = value.ClassID;
                                btnto.ClassIndex = value.ClassIndex;
                                btnto.StudentID = value.StudentID;
                                btnto.Remark = "更新考勤记录，识别成功,当前剩余课时：" + (enroll.ClassHour - enroll.UsedHour).ToString();
                                db.Update(btnto);//更新
                            }
                            else
                            {
                                throw new Exception("已有考勤记录！");
                            }
                        }

                    }
                    if (value.ClockTime == null)//未打卡
                    {
                        Enroll enroll = db.Query<Enroll>("select * from Enroll where StudentID = '" + value.StudentID + "' and ClassID = '" + value.ClassID + "'").FirstOrDefault();
             
                        if (enroll != null && value.AttendanceTypeID == 3)//缺勤状态还是要扣课时，同时更新报名记录的小号课时
                        {
                            //-----添加课时变化日志记录 begin
                            TransferRecord tr = new TransferRecord();//添加课时变化日志记录
                            tr.StudentID = enroll.StudentID;
                            tr.BeforeHours = enroll.ClassHour - enroll.UsedHour;
                            tr.AfterHours = enroll.ClassHour - enroll.UsedHour - 1;
                            tr.TypeID = 5;//ERP考勤操作
                            tr.CreateTime = DateTime.Now;
                            tr.CreatorId = userid;
                            tr.ENID = enroll.ID;
                            tr.ClassID = enroll.ClassID;
                            db.Insert(tr);
                            //-----添加课时变化日志记录 end

                            enroll.UsedHour = enroll.UsedHour + 1;
                            db.Update(enroll);

                            ClassList cl = ClassListData.GetOneByid(value.ClassID, value.ClassIndex);
                            cl.StateID = 2;//课时状态变成已上
                            db.Update<ClassList>(cl);
                        }

                        //新增考勤记录--------------------------------------------------------------
                        if (btnto == null)
                        {
                            btnto = new AttendanceRecord();
                            btnto.AttendanceTypeID = value.AttendanceTypeID;
                
                 
                            btnto.CreateTime = DateTime.Now;
                            btnto.CreatorId = userid;
                            btnto.AttendanceWayID = 3;//工作人员操作
                            btnto.ClassID = value.ClassID;
                            btnto.ClassIndex = value.ClassIndex;
                            btnto.StudentID = value.StudentID;
                            btnto.Remark = "更新考勤记录，识别成功,当前剩余课时：" + (enroll.ClassHour - enroll.UsedHour).ToString();
                            db.Insert(btnto);//新增考勤
                        }
                        else//如果之前有考勤记录了，且未打卡则更新打卡
                        {
                            if (btnto.ClockTime == null)//已打卡，不允许改成请假
                            {
                                btnto.AttendanceTypeID = value.AttendanceTypeID;

                                btnto.UpdateTime = DateTime.Now;
                                btnto.UpdatorId = userid;
                                btnto.AttendanceWayID = 3;//工作人员操作
                                btnto.ClassID = value.ClassID;
                                btnto.ClassIndex = value.ClassIndex;
                                btnto.StudentID = value.StudentID;
                                btnto.Remark = "更新考勤记录，识别成功,当前剩余课时：" + (enroll.ClassHour - enroll.UsedHour).ToString();
                                db.Update(btnto);//更新
                            }
                            else
                            {
                                throw new Exception("已有考勤记录！");
                            }
                        }
                    }
               }

                db.Commit(); //事务提交
                db.Dispose();  //资源释放
                ret = true;//新增成功
            }
            catch (Exception ex)
            {

                db.Rollback();
                db.Dispose();//资源释放
                throw new Exception(ex.Message + "。" + ex.InnerException.Message);
            }


            return ret ;
        }

        public static Enroll getEnrollByStudentClass(string studentID, string classId,DBRepository db)
        {
            String sql = "select * from Enroll  WITH(NOLOCK)  where StudentID = @StudentID and ClassID = @ClassID ";
            var dynamic = new DynamicParameters();
            dynamic.Add("@StudentID", studentID);
            dynamic.Add("@ClassID", classId);

            return MsSqlMapperHepler.SqlWithParamsSingle<Enroll>(sql, dynamic, DBKeys.PRX);
        }
        /// <summary>
        /// 修改enroll表的用户课时
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int UpdateEnroll(string ID, decimal UsedHour, DateTime UpdateTime, string UpdatorId, DBRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update enroll set UsedHour=@UsedHour,UpdatorId=@UpdatorId,UpdateTime=@UpdateTime  ");
            sb.Append(" where ID=@ID ");
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateTime", UpdateTime);
            parameters.Add("@UpdatorId", UpdatorId);
            parameters.Add("@UsedHour", UsedHour);
            parameters.Add("@ID", ID);
            // parameters.Add("@TotalLesson", TotalLesson);
            return db.Execute(sb.ToString(), parameters);

        }




        ///文件上传

        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddClassListJob(ClassListJob Clas)
        {
            return MsSqlMapperHepler.Insert<ClassListJob>(Clas, DBKeys.PRX);
        }




        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="classIndex"></param>
        /// <returns></returns>
        public static List<vw_ClassListJob> ClassListJob(string classid, int classindex)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_ClassListJob ";//表或者视图
            orderby = "id";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append("select * from vw_ClassListJob where ");
            sb.Append(" 1=1 ");


            if (!string.IsNullOrWhiteSpace(classid))//班级ID
                sb.Append(" and classid = @ClassID");
            if (classindex != 0)//班级行号
                sb.Append(" and classindex = @ClassIndex  ");
            sb.Append(" order by id ");

      
            var parameters = new DynamicParameters();
            parameters.Add("@classid", classid);
            parameters.Add("@classindex", classindex);
            return MsSqlMapperHepler.SqlWithParams<vw_ClassListJob>(sb.ToString(), parameters, DBKeys.PRX);
    
        }


        public static int UpdateClassList(ClassList cls)
        {
 
            StringBuilder sb = new StringBuilder();
            sb.Append(" update ClassList set JobTitle=@JobTitle  ");
            sb.Append(" , JobContent=@JobContent  "); 
            sb.Append(" where ClassID=@ClassID ");
            sb.Append(" AND ClassIndex=@ClassIndex ");
            var parameters = new DynamicParameters();
            parameters.Add("@JobTitle", cls.JobTitle);
            parameters.Add("@JobContent", cls.JobContent);
            parameters.Add("@ClassID", cls.ClassID);
            parameters.Add("@ClassIndex", cls.ClassIndex); 
            return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
        }





        /// <summary>
        /// 根据ID删除上传作业 ClassListJob表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DELETE_ClassListJob(String ID)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" delete  from ClassListJob ");
            sb.Append(" where ID = @ID"); 
            var parameters = new DynamicParameters();
            parameters.Add("@ID", ID); 
            return MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX);
        }
        /// <summary>
        /// 保存学员评价
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        public static bool SaveStudentEvalute_WX(AttendanceRecord ar)
        {
            bool ret = false;
            StringBuilder sb = new StringBuilder();
            sb.Append(" update AttendanceRecord set Evaluate=@Evaluate  ");
            sb.Append(" , UpdateTime=@UpdateTime  ");
            sb.Append(" , UpdatorId=@UpdatorId  "); 
            sb.Append(" where ClassID=@ClassID ");
            sb.Append(" AND ClassIndex=@ClassIndex ");
            sb.Append(" AND StudentID=@StudentID ");
            var parameters = new DynamicParameters();
            parameters.Add("@StudentID", ar.StudentID);
            parameters.Add("@Evaluate", ar.Evaluate);
            parameters.Add("@ClassID", ar.ClassID);
            parameters.Add("@ClassIndex", ar.ClassIndex);
            parameters.Add("@UpdateTime", ar.UpdateTime);
            parameters.Add("@UpdatorId", ar.UpdatorId);
            if (MsSqlMapperHepler.InsertUpdateOrDeleteSql(sb.ToString(), parameters, DBKeys.PRX) > 0)
            {
                ret = true;
            }
            return ret;
        }

    }

}
