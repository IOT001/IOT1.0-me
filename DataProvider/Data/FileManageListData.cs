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
    public class FileManageListData
    {
        /// <summary>
        /// 查询文件管理
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static PagedList<Files> GetFileseList(FileManageListSearchModel search)
        {
            string table = string.Empty, fields = string.Empty, orderby = string.Empty, where = string.Empty;//定义结构
            fields = @"  * ";//输出字段
            table = @" Files ";//表或者视图
            orderby = "ID";//排序信息
            StringBuilder sb = new StringBuilder();//构建where条件
            sb.Append(" 1=1 ");
            if (!string.IsNullOrWhiteSpace(search.FileName))//名称
                sb.AppendFormat(" and FileName like '%{0}%' ", search.FileName);
            if (!string.IsNullOrWhiteSpace(search.FileTitle))//名称
                sb.AppendFormat(" and FileTitle like '%{0}%' ", search.FileTitle);


            //判断是否添加了管理员和校长权限，添加了就不查询全部
            int isnull=0;
            for (int i = 0; i < search.isnull.Count; i++)
            {
                if (search.isnull[i] == "1" || search.isnull[i] == "4")
                {
                    isnull = 1;
                }
            }
            if (search.isnull.Count==0)
            {
                 isnull = 2;
            }
            //根据获取的角色来判断是否是管理员和校长，不是就按角色本身来查询
            if (isnull != 1)
            { 
              for (int i = 0; i < search.isnull.Count; i++)
            {
                if (search.isnull[i] != "1" || search.isnull[i] != "4")
                {
                    sb.AppendFormat(" and ToRoles like '%{0}%' ", search.isnull[i]);  
                }
            }
            }
            
            if(isnull == 2)
            {
                sb.AppendFormat(" and 1<>1 ");  
            }
  

            where = sb.ToString();
            int allcount = 0;
            var list = CommonPage<Files>.GetPageList(
    out allcount, table, fields: fields, where: where.Trim(),
    orderby: orderby, pageindex: search.CurrentPage, pagesize: search.PageSize, connect: DBKeys.PRX);
            return new PagedList<Files>(list, search.CurrentPage, search.PageSize, allcount);
        }
    
        /// <summary>
        /// 新增,返回的是主键
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static int AddFiles(Files files)
        {
            return MsSqlMapperHepler.Insert<Files>(files, DBKeys.PRX);
        }
 


    }

}
