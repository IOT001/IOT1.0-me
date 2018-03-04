using DataProvider.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using DataProvider.Models;
using DataProvider.SqlServer;
using DataProvider;
using Dapper;

namespace DataProvider.Data
{
    public class TeacherData
    {
        /// <summary>
        /// 分页获取按钮列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<Teachers> GetButtonList(TeacherSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" Teachers ";//表或者视图
            orderby = "CreateTime";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            switch (search.LeaveDate)
            {
                case 1:
                    sb.Append(" and LeaveDate is null ");
                    break;
                case 2:
                    sb.Append(" and LeaveDate <> '' ");
                    break;
            }

            if (!string.IsNullOrWhiteSpace(search.TeacherName))//按钮中文名称
                sb.AppendFormat(" and name like '%{0}%' ", search.TeacherName);
            //if (!string.IsNullOrWhiteSpace(search.BTN_Name_En))//城市
            //    sb.AppendFormat(" and BTN_Name_En like '%{0}%' ", search.BTN_Name_En);
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Teachers>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Teachers>(list, search.CurrentPage, search.PageSize, allcount);
        }

        /// <summary>
        /// 获取所有在职的教师
        /// </summary>
        /// <returns></returns>
        public static List<Teachers> getOnWorkTeachers()
        {
            string strsql = "select * from Teachers where LeaveDate is null";
            return MsSqlMapperHepler.SqlWithParams<Teachers>(strsql, null, DBKeys.PRX);
        }
        /// <summary>
        /// 添加新的教师
        /// </summary>
        /// <returns></returns>
        public static string AddTeach(Teachers teacher)
        {
            if (string.IsNullOrEmpty(teacher.ID))
            {

                RandomOperate operate = new RandomOperate();
                teacher.ID = operate.CreateMD5Hash(operate.GenerateCheckCode(30));
            }
            return MsSqlMapperHepler.Insert<Teachers>(teacher, DBKeys.PRX);
        }

        /// <summary>
        /// 更新 教师信息
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateTeacher(Teachers btn)
        {
            Teachers btnto = TeacherData.GetTeachByID(btn.ID);//获取对象
            btnto.IDNumber = btn.IDNumber;
            btnto.sex = btn.sex;
            btnto.name = btn.name;
            btnto.MobilePhone = btn.MobilePhone;
            btnto.LeaveDate = btn.LeaveDate;
            btnto.EntryDate = btn.EntryDate;
            btnto.WeChat = btn.WeChat;
            btnto.Remark = btn.Remark;
            btnto.Email = btn.Email;
            btnto.ContactAddress = btn.ContactAddress;
            btnto.ComCode = btn.ComCode;
            //Cloner<Teachers, Teachers>.CopyTo(btn, btnto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
        }

        /// <summary>
        /// 根据登录ID获取教师信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Teachers GetTeachByID(String loginid)
        {
            string strsql = "select * from Teachers where MobilePhone = '" + loginid + "'";
            return MsSqlMapperHepler.SqlWithParamsSingle<Teachers>(strsql, null, DBKeys.PRX);
        }


        /// <summary>
        /// 分页获取教师列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<vw_Teachers> GetTeachersList(TeacherSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" vw_Teachers ";//表或者视图
            orderby = "CreateTime desc";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");

            if (!string.IsNullOrWhiteSpace(search.ComCode))//校区
                sb.AppendFormat(" and [ComCode] = '{0}' ", search.ComCode);


            if (search.LeaveDate == 1)//离职
            {
                sb.AppendFormat(" and LeaveDate is null ", search.LeaveDate);
            }
            else if (search.LeaveDate == 2)
            {
                sb.AppendFormat(" and LeaveDate is not null ", search.LeaveDate);
            }

            if (!string.IsNullOrWhiteSpace(search.TeacherName))//是否在职
                sb.AppendFormat(" and name like '%{0}%' ", search.TeacherName);

            if (!string.IsNullOrWhiteSpace(search.MobilePhone))//手机号码
                sb.AppendFormat(" and MobilePhone like '%{0}%' ", search.MobilePhone);


            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<vw_Teachers>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<vw_Teachers>(list, search.CurrentPage, search.PageSize, allcount);
        }



        /// <summary>
        /// 添加新的教师
        /// </summary>
        /// <returns></returns>
        public static string AddTeachers(Teachers teacher, SYSAccount sys)
        {
            DBRepository db = new DBRepository(DBKeys.PRX);
            string ret = "0";
            try
            {
                
                db.BeginTransaction();//事务开始 
                db.Insert<Teachers>(teacher);
                // MsSqlMapperHepler.Insert<Teachers>(teacher, DBKeys.PRX);
                db.Insert<SYSAccount>(sys);
                db.Commit(); //事务提交 
                
                db.Dispose();  //资源释放
                ret = "1";//新增成功 
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
        /// 添加新的教师
        /// </summary>
        /// <returns></returns>
        public static string AddSYS_SystemRole(SYSAccountRole sys)
        {
            DBRepository db = new DBRepository(DBKeys.PRX);
            db.BeginTransaction();//事务开始 
            string ret = "0";
            try
            {
                var number = Getnumber(sys.AR_AccountId);//判断是否存在
                if (number > 0)
                {
                    db.Execute("Delete From SYS_AccountRole where AR_AccountId = " + sys.AR_AccountId);
                }


                for (int i = 0; i < sys.AR_SystemRoleIdS.Length; i++)
                {
                    sys.AR_SystemRoleId = int.Parse(sys.AR_SystemRoleIdS[i]);
                    db.Insert<SYSAccountRole>(sys);
                }

                db.Commit(); //事务提交 
                ret = "1";//新增成功 
                db.Dispose();  //资源释放
            }
            catch (Exception ex)
            {
                db.Rollback();
                db.Dispose();
                throw new Exception(ex.Message);
            }
            return ret;
        }


        //<summary>
        //获取ClassList行数
        //</summary>
        //<param name="Stockid"></param>
        //<returns></returns> 
        public static int Getnumber(int AR_AccountId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(AR_ID) from SYS_AccountRole   ");
            sb.Append(" where AR_AccountId=@AR_AccountId ");
            var parameters = new DynamicParameters();
            parameters.Add("@AR_AccountId", AR_AccountId);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sb.ToString(), parameters, DBKeys.PRX);
        }





        //<summary>
        //获取Teachers表的分校CODE
        //</summary>
        //<param name="Stockid"></param>
        //<returns></returns> 
        public static string GetTeachers_ComCode(string MobilePhone)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ComCode from Teachers   ");
            sb.Append(" where MobilePhone=@MobilePhone ");
            var parameters = new DynamicParameters();
            parameters.Add("@MobilePhone", MobilePhone);
            return MsSqlMapperHepler.SqlWithParamsSingle<string>(sb.ToString(), parameters, DBKeys.PRX);
        }




    }
}
