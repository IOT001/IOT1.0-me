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
    public class MessageData
    {
        /// <summary>
        /// 分页优惠表列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<Message> GetMessageList(MessageListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" Message ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.Title))//名称
                sb.AppendFormat(" and Title like '%{0}%' ", search.Title); 
            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Message>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Message>(list, search.CurrentPage, search.PageSize, allcount);
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Message GetMessageByID(int ID)
        {
            return MsSqlMapperHepler.GetOne<Message>(ID, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddMessage(Message Mes)
        {
            return MsSqlMapperHepler.Insert<Message>(Mes, DBKeys.PRX);
        }
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddDiscount(Discount Dis)
        {
            return MsSqlMapperHepler.Insert<Discount>(Dis, DBKeys.PRX);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static bool UpdateMessage(Message Mes)
        {
            Message Stuto = MessageData.GetMessageByID(Mes.ID);//获取对象
            Cloner<Message, Message>.CopyTo(Mes, Stuto);//代码克隆，把前台或者的值也就是变更内容复制到目标对象，不做变更的数据不变
            return MsSqlMapperHepler.Update(Stuto, DBKeys.PRX);

        }



    }

}
