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
                ret = true;
            }
            catch (Exception ex)
            {
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


    }
}
