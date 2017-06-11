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
            switch (search.DicItemID)
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
            //Cloner<Teachers, Teachers>.CopyTo(btn, btnto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(btnto, DBKeys.PRX);
        }

        /// <summary>
        /// 根据ID获取教师信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Teachers GetTeachByID(String ID)
        {
            return MsSqlMapperHepler.GetOne<Teachers>(ID, DBKeys.PRX);
        }
    }
}
