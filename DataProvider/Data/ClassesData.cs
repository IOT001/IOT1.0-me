using Dapper;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.Paging;
using DataProvider.SqlServer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Data
{
    public class ClassesData
    {
        /// <summary>
        /// 分页获取班级列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Classes> GeClassesList(ClassesListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Classes ";//表或者视图
            orderby = "ID desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");


            if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
                sb.AppendFormat(" and [ComCode] = '{0}' ", search.ComCode);

            if (!string.IsNullOrWhiteSpace(search.ClassName))//班级名称
                sb.AppendFormat(" and ClassName like '%{0}%' ", search.ClassName);
            if (!string.IsNullOrWhiteSpace(search.CourseID))//课程名称
                sb.AppendFormat(" and CourseID like '%{0}%' ", search.CourseID);
   

            if (!string.IsNullOrWhiteSpace(search.StartTime_start))//开班时间
                sb.AppendFormat(" and StartTime > = '{0}' ", search.StartTime_start);
            if (!string.IsNullOrWhiteSpace(search.StartTime_end))//开班时间
                sb.AppendFormat(" and StartTime <= '{0}' ", search.StartTime_end);

            if (!string.IsNullOrWhiteSpace(search.EndTime_start))//结班时间
                sb.AppendFormat(" and EndTime > = '{0}' ", search.EndTime_start);
            if (!string.IsNullOrWhiteSpace(search.EndTime_end))//结班时间
                sb.AppendFormat(" and EndTime <= '{0}' ", search.EndTime_end);



            if (!string.IsNullOrWhiteSpace(search.TeacherID))//当前讲师
                sb.AppendFormat(" and TeacherID = '{0}' ", search.TeacherID);

            if (search.islisten == "1")//如果是试听班
            {
                sb.AppendFormat(" and TeachTypeID =1 ");
                
            }
            else if (search.islisten == "0")//不是试听班，报名班为了保证有学员课程表，只取未排班数据，以后根据实际情况改
            {
                sb.AppendFormat(" and TeachTypeID > 1 and StateID = 1");
            }
            else
            {
 
            }
            if (search.TeachTypeID != 0 && search.TeachTypeID != null)//授课方式TeachTypeID
                sb.AppendFormat(" and TeachTypeID = {0} ", search.TeachTypeID);

            where = sb.ToString();

            int allcount = 0;
            var list = CommonPage<vw_Classes>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Classes>(list, search.CurrentPage, search.PageSize, allcount);

        }





        /// <summary>
        /// 分页获取班级列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Classes> GeClass_transfe_List(ClassesListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Classes ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件 
            sb.Append("   1=1 and StateID <> 4  ");
            if (!string.IsNullOrWhiteSpace(search.ClassName))//当前讲师
                sb.AppendFormat(" and ClassName like '%{0}%' ", search.ClassName);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Classes>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Classes>(list, search.CurrentPage, search.PageSize, allcount);

        }



        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Classes GetClassesByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<Classes>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static string AddStudent(Classes Clas)
        {
            return MsSqlMapperHepler.Insert<Classes>(Clas, DBKeys.PRX);
        }
        /// <summary>
        /// 保存班级信息
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool Update(Classes Stu)
        {
            Classes Stuto = ClassesData.GetClassesByID(Stu.ID);//获取对象
            Cloner<Classes, Classes>.CopyTo(Stu, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

        }





        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="Stockid"></param>
        /// <returns></returns>
        public static Classes GetClassesList()
        {

            string strsql = "select id from Classes  order by CreateTime desc     ";
            var parameters = new DynamicParameters();
            return MsSqlMapperHepler.SqlWithParamsSingle<Classes>(strsql.ToString(), parameters, DBKeys.PRX);


        }




        //有问题


        /// <summary>
        /// 根据ClassID或者姓名获取报名表数据
        /// </summary>
        /// <param name="apid"></param>
        /// <returns></returns>
        public static List<FollowRecord> GetClassesByClassID(string StudentID, string name)
        {
            string sql = @" select * from FollowRecord where 1=1 and StudentID = @StudentID and name=@name";
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(StudentID))//班级名称
                parameters.Add("@StudentID", StudentID);

            if (!string.IsNullOrWhiteSpace(name))//班级名称
                parameters.Add("@name", name);

            return MsSqlMapperHepler.SqlWithParams<FollowRecord>(sql, parameters, DBKeys.PRX);
        }









        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool SaveClass_transfe(Enroll en,ClassesTrans ct)
        {

            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            try
            {
                
                db.BeginTransaction();//事务开始

                UpdateEnroll(en, db);
                if (AddClassesTrans(ct,db)>0)
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
        public static int AddClassesTrans(ClassesTrans ct, DBRepository db)
        {
            return db.Insert<ClassesTrans>(ct);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateEnroll(Enroll en, DBRepository db)
        {
            Enroll Stuto = ClassesData.GetEnrollByID(en.ID);//获取对象
            Cloner<Enroll, Enroll>.CopyTo(en, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return db.Update(Stuto);

        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Enroll GetEnrollByID(string ID)
        {
            return MsSqlMapperHepler.GetOne<Enroll>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 升班的操作
        /// </summary>
        /// <param name="oldclassid"></param>
        /// <param name="newclassid"></param>
        /// <param name="ja"></param>
        /// <param name="operateid"></param>
        /// <returns></returns>
        public static bool UpClass(string oldclassid,string newclassid,JArray ja,string operateid)
        {
            bool ret = false;
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();
            try
            {
                foreach (var item in ja)
                {
                    string enid = ((JObject)item)["enid"].ToString();//报名记录id
                    decimal newclasshour = decimal.Parse(((JObject)item)["newclasshour"].ToString());//转换后的课时
                    decimal upprice = decimal.Parse(((JObject)item)["upprice"].ToString()); ;//升班差价
                    Enroll en = db.GetById<Enroll>(enid);//获取学员的报名记录
                    TransferRecord tf = new TransferRecord();//转移记录
                    tf.StudentID = en.StudentID;
                    tf.TypeID = 2;//升班
                    tf.BeforeHours = en.ClassHour - en.UsedHour;
                    tf.AfterHours = newclasshour;
                    tf.CreateTime = DateTime.Now;
                    tf.CreatorId = operateid;
                    tf.Remark = "升班：原报名号:" + enid + " 原班级号：" + oldclassid + " 新班级：" + newclassid;
                    db.Insert(tf);
                    //生成新的报名记录
                    Enroll upen = new Enroll();
                    upen.ID = CommonData.DPGetTableMaxId("EN", "ID", "Enroll", 8, db);
                    upen.APID = "";
                    upen.StudentID = en.StudentID;
                    upen.ClassID = newclassid;
                    upen.ClassHour = newclasshour;
                    upen.UsedHour = 0;//改变所消耗课时
                    upen.Price = 0;
                    upen.Paid = 0;
                    upen.CreatorId = operateid;
                    upen.CreateTime = DateTime.Now;
                    upen.StateID = 1;//无需审核

                    upen.UpPrice = upprice;

                    db.Insert(upen);

                    FundsFlow fl = new FundsFlow();//资金流水
                    fl.TypeID = 4;//类型为升班
                    fl.Amount = upprice;
                    fl.KeyID = upen.ID;
                    fl.CreateTime = DateTime.Now;
                    fl.CreatorId = operateid;
                    db.Insert(fl);

                    Classes ca = db.GetById<Classes>(oldclassid);
                    ca.StateID = 4;//原来班级状态
                    db.Update(ca); 
                }
                ret = true;
                db.Commit();
                db.Dispose();
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();//资源释放
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
