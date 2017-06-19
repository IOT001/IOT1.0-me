using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Entities
{
  public  class vw_StudentEvaluate
    {
      /// <summary>
      /// 课程ID
      /// </summary>
      public string ClassID{get;set;}
      /// <summary>
      /// 课程索引
      /// </summary>
      public int ClassIndex { get; set; }
      /// <summary>
      /// 评价内容
      /// </summary>
      public string  Evaluate{get;set;}
      /// <summary>
      /// 学生姓名
      /// </summary>
      public string Name{get;set;}
      /// <summary>
      /// 电话
      /// </summary>
      public string Phone{get;set;}
      // 学生ID
      public string StudentID{get;set;}
      public int ID { get; set; }

    }
  /// <summary>
  /// Deploy：实体对象映射关系
  /// </summary>
  [Serializable]
  public sealed class vw_StudentEvaluateORMMapper : ClassMapper<vw_StudentEvaluate>
  {
      public vw_StudentEvaluateORMMapper()
      {
          base.Table("vw_StudentEvaluate");

          //Map(f => f.socketouts).Ignore();//设置忽略
          //Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
          AutoMap();
      }
  }
}
