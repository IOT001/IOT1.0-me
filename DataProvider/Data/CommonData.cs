﻿using Dapper;
using DataProvider.Entities;
using DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DataProvider.Data
{
    /// <summary>
    /// 数据库通用方法
    /// </summary>
   public class CommonData
    {
       public static readonly CommonData Instance = new CommonData();
        #region 获取字典列表
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetDictionaryList(int DicTypeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DicItemID id,DicItemName name");
            sb.Append(" FROM DictionaryItem a WITH(NOLOCK)");
            sb.Append(" INNER JOIN DictionaryType b WITH(NOLOCK) ON a.DicTypeID = b.DicTypeID");
            sb.Append(" WHERE b.DicTypeID = @DicTypeID and a.recordState <> '2'");
            sb.Append(" ORDER BY Sort");
            var parameters = new DynamicParameters();
            parameters.Add("@DicTypeID", DicTypeID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);
        }
        #endregion



        #region 获取字典列表
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>因为学员状态有重新的名称，所以单独写一个下拉的绑定</returns>
        public static List<CommonEntity> GetDictionary_StudentSource_List(int DicTypeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DicItemID id,DicItemName name");
            sb.Append(" FROM DictionaryItem a WITH(NOLOCK)");
            sb.Append(" INNER JOIN DictionaryType b WITH(NOLOCK) ON a.DicTypeID = b.DicTypeID");
            sb.Append(" WHERE b.DicTypeID = @DicTypeID and a.recordState <> '2' AND  a.DicItemID < > 0  AND  a.DicItemID <> 1");
            sb.Append(" ORDER BY Sort");
            var parameters = new DynamicParameters();
            parameters.Add("@DicTypeID", DicTypeID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);
        }
        #endregion




        public List<SelectListItem> GetBropDownListData(List<CommonEntity> list)
        {
            return GetBropDownListData(list, true);
        }

        public List<SelectListItem> GetBropDownListData(List<CommonEntity> list, bool needDefault, string selectedValueText = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var model in list)
            {
                if ((!string.IsNullOrWhiteSpace(selectedValueText)) && ((model.id == selectedValueText) || (model.name == selectedValueText)))
                    items.Add(new SelectListItem { Value = model.id, Text = model.name, Selected = true });
                else
                    items.Add(new SelectListItem { Value = model.id, Text = model.name });
            }
            if (needDefault)
                items.Insert(0, new SelectListItem { Value = string.Empty, Text = "--请选择--" });
            return items;
        }


        public List<SelectListItem> GetBropDownListData_Choice(List<CommonEntity> list)
        {
            return GetBropDownListData_Choice(list, true);
        }

        public List<SelectListItem> GetBropDownListData_Choice(List<CommonEntity> list, bool needDefault, string selectedValueText = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var model in list)
            {
                if ((!string.IsNullOrWhiteSpace(selectedValueText)) && ((model.id == selectedValueText) || (model.name == selectedValueText)))
                    items.Add(new SelectListItem { Value = model.id, Text = model.name, Selected = true });
                else
                    items.Add(new SelectListItem { Value = model.id, Text = model.name });
            }
            
            return items;
        }


        #region 获取字典老师表列表 
       
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetTeachersList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id,name from Teachers");
            sb.Append(" WHERE 1=@one");
            var parameters = new DynamicParameters();
            var one = 1;
            parameters.Add("@one", one);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);
           
        }
        #endregion

        #region 获取字典学生表列表

        /// <summary>
        /// 获取学生字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetStudentsList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id,name from Students");
            sb.Append(" WHERE 1=@one");
            var parameters = new DynamicParameters();
            var one = 1;
            parameters.Add("@one", one);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion

        #region 获取课程表列表

        /// <summary>
        /// 获取课程字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetCourseList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ID,CourseName name from Course");
            sb.Append(" WHERE StateID <> @StateID");
            var parameters = new DynamicParameters();
            var StateID = 2;
            parameters.Add("@StateID", StateID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion

        #region 获取分校

        /// <summary>
        /// 获取分校字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于分校下拉的绑定项目</returns>
        public static List<CommonEntity> Get_SYS_Company_List()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select COMP_Code as ID,COMP_Name as name from SYS_Company ");
            sb.Append(" WHERE 1 = @StateID");
            var parameters = new DynamicParameters();
            var StateID = 1;
            parameters.Add("@StateID", StateID);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion


        #region 根据登录人获取分校，值是1或者null查询全部，否则只查询当前校区信息
        /// <summary>
        /// 获取分校字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于分校下拉的绑定项目</returns>
        public static List<CommonEntity> Get_SYS_Company_COMP_Code(string COMP_Code)
        {
            StringBuilder sb = new StringBuilder();
           
            if (string.IsNullOrEmpty(COMP_Code) || COMP_Code == "1")
            {
                sb.Append("SELECT '' AS ID,'--请选择--' AS name UNION ALL  select COMP_Code as ID,COMP_Name as name from SYS_Company ");
                sb.Append(" WHERE 1 = 1 ");
            }
            else
            {
                sb.Append("select COMP_Code as ID,COMP_Name as name from SYS_Company ");
                sb.Append(" WHERE COMP_Code = @COMP_Code");
            } 
            var parameters = new DynamicParameters();
         
            parameters.Add("@COMP_Code", COMP_Code);
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion

        #region 获取班级字典信息

        /// <summary>
        /// 获取班级字典信息,不包括试听
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<CommonEntity> GetClassesItemList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ID,ClassName name from Classes");
            sb.Append(" WHERE TeachTypeID <> 1 and StateID = 1");
            var parameters = new DynamicParameters();
            return MsSqlMapperHepler.SqlWithParams<CommonEntity>(sb.ToString(), parameters, DBKeys.PRX);

        }
        #endregion



        //<summary>
        //获取dictionaryItem行数
        //</summary>
        //<param name="Stockid"></param>
        //<returns></returns>


        public static int Getnumber(int DicTypeID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(DicTypeID) from dictionaryItem   ");
            sb.Append(" where DicTypeID=@DicTypeID ");
            var parameters = new DynamicParameters();
            parameters.Add("@DicTypeID", DicTypeID);
            return MsSqlMapperHepler.SqlWithParamsSingle<int>(sb.ToString(), parameters, DBKeys.PRX);
        }


        public static string DPGetTableMaxId(string prefix, string field, string tablename, int digit, DBRepository db = null)
        {
            try
            {
                string retstr = null;
                var parameters = new DynamicParameters();
                parameters.Add("@title", prefix);//开头字母，前缀字母
                parameters.Add("@pkName", field);//要插入的表字段
                parameters.Add("@tableName", tablename);//所在表
                parameters.Add("@bitCount", digit);//不包括前缀的位数
                if (db == null)
                {


                    retstr = MsSqlMapperHepler.StoredProcWithParamsSingle<string>("sp_createKey", parameters, DBKeys.PRX);
                }
                else
                {
                    retstr = db.StoredProcWithParamsSingle<string>("sp_createKey", parameters).FirstOrDefault();
                }


                    return retstr;//返回值
   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "转换的过程中发生了错误!");
            }

        }


        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉框权限的绑定</returns>
        public static List<SYS_Role> GetSYS_SystemRole_IS(int ROLE_IsSuspended)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROLE_Id as id,ROLE_Name as name FROM SYS_SystemRole");
            sb.Append(" WHERE ROLE_IsSuspended = @ROLE_IsSuspended");
            var parameters = new DynamicParameters();
            parameters.Add("@ROLE_IsSuspended", ROLE_IsSuspended);
            return MsSqlMapperHepler.SqlWithParams<SYS_Role>(sb.ToString(), parameters, DBKeys.PRX);
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<SYS_Role> GetSYS_SystemRoleList(int ROLE_OrderIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROLE_Id as id,ROLE_Name as name FROM SYS_SystemRole");
            sb.Append(" WHERE ROLE_OrderIndex = @ROLE_OrderIndex");
            var parameters = new DynamicParameters();
            parameters.Add("@ROLE_OrderIndex", ROLE_OrderIndex);
            return MsSqlMapperHepler.SqlWithParams<SYS_Role>(sb.ToString(), parameters, DBKeys.PRX);
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicTypeID"></param>
        /// <returns>用于下拉的绑定项目</returns>
        public static List<string> GetSYS_SystemRoleList_ROLE_Id(int ROLE_Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROLE_Name as name FROM SYS_SystemRole");
            sb.Append(" WHERE ROLE_Id = @ROLE_Id");
            var parameters = new DynamicParameters();
            parameters.Add("@ROLE_Id", ROLE_Id);
            return MsSqlMapperHepler.SqlWithParams<string>(sb.ToString(), parameters, DBKeys.PRX);
        }

        public class SYS_Role
        {
            /// <summary>
            /// 值
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string name { get; set; }
        }
        /// <summary>
        /// 保存签名数据
        /// </summary>
        /// <returns></returns>
        public static int SaveSign(SignImage si)
        {
            return MsSqlMapperHepler.Insert(si,DBKeys.PRX);
        }
        /// <summary>
        /// 获取主键类型是字符串的签名对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static SignImage GetStringKeySign(string id, int typeid)
        {
            string strsql = "select top 1 * from SignImage where StringKey = '" + id + "' and TypeID = " + typeid + " order by CreateTime desc";
            SignImage si = MsSqlMapperHepler.SqlWithParamsSingle<SignImage>(strsql, null, DBKeys.PRX);
            return si;
        }


        /// <summary>
        /// 获取主键类型是整数的签名对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static SignImage GetIntKeySign(int id, int typeid)
        {
            string strsql = "select top 1 * from SignImage where IntKey = '" + id + "' and TypeID = " + typeid + " order by CreateTime desc";
            SignImage si = MsSqlMapperHepler.SqlWithParamsSingle<SignImage>(strsql, null, DBKeys.PRX);
            return si;
        }


    }
}
