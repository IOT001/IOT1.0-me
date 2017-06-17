using Dapper;
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



        #region 获取课程表列表

        /// <summary>
        /// 获取字典列表
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


        public static string DPGetTableMaxId(string prefix, string field, string tablename, int digit)
        {
            try
            {

                    string retstr;
                    var parameters = new DynamicParameters();
                    parameters.Add("@title", prefix);//开头字母，前缀字母
                    parameters.Add("@pkName", field);//要插入的表字段
                    parameters.Add("@tableName", tablename);//所在表
                    parameters.Add("@bitCount", digit);//不包括前缀的位数
                    retstr = MsSqlMapperHepler.StoredProcWithParamsSingle<string>("sp_createKey", parameters, DBKeys.PRX);
                    return retstr;//返回值
   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "转换的过程中发生了错误!");
            }

        }
    }
}
