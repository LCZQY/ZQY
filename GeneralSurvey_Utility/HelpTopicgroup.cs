using System;
using System.Collections.Generic;
using System.Text;
using GeneralSurvey_Data.Model;
using GeneralSurvey_Data;
using Dapper;

using System.Data;
using System.Linq;

namespace GeneralSurvey_Utility
{
    public static class HelpTopicgroup
    {
        /// <summary>
        /// 单个插入，返回影响行数
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        public static int Insert(Topicgroups model)
        {
            using (var db = Databases.connect())
            {
                return db.Execute(@"insert into `qp.topicgroup`(id,TopicName,CharactersSize,SetsettingId,OptionText,Stide,FromName,FromID) values(@id,@TopicName,@CharactersSize,@SetsettingId ,@OptionText,@Stide,@FromName,@FromID);", model);
            }
        }

        /// <summary>
        ///   批量插入，返回影响行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(List<Topicgroups> model)
        {
            using (var db = Databases.connect())
            {
                return db.Execute(@"insert into `qp.topicgroup`(id,TopicName,CharactersSize,SetsettingId,OptionText,Stide,FromName,FromID) values(@id,@TopicName,@CharactersSize,@SetsettingId ,@OptionText,@Stide,@FromName,@FromID);", model);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            int remove = Databases.connect().Execute(@"delete from `qp.topicgroup` where id=@id", new { id = @id });
            if (remove > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        public static bool Update(Topicgroups model)
        {
            using (var db = Databases.connect())
            {
                int updateSet = db.Execute(@"update `qp.topicgroup` set TopicName=@TopicName,CharactersSize=@CharactersSize,SetsettingId=@SetsettingId,OptionText=@OptionText,Stide=@Stide,FromName=@FromName,FromID=@FromID where id=@id", new { model });
                if (updateSet > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(List<Topicgroups> model)
        {

            using (var db = Databases.connect())
            {
                int updateSet = db.Execute(@"update `qp.topicgroup` set TopicName=@TopicName,CharactersSize=@CharactersSize,SetsettingId=@SetsettingId,OptionText=@OptionText,Stide=@Stide ,FromName=@FromName,FromID=@FromIDwhere id=@id", new { model });
                if (updateSet > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static List<Topicgroups> GetList(string cond)
        {
            string sql = @"select id,TopicName,CharactersSize,SetsettingId,OptionText,Stide,FromName,FromID from `qp.topicgroup` where " + cond + "  ORDER BY Stide";
            using (var db = Databases.connect())
            {

                return db.Query<Topicgroups>(sql).ToList();
            }
        }

        /// <summary>
        /// 无条件查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static List<Topicgroups> GetList()
        {
            string sql = @"select id,TopicName,CharactersSize,SetsettingId,OptionText,Stide,FromName,FromID from `qp.topicgroup` where FromID='" + Seesion.FromIds + "'  ORDER BY Stide";
            using (var db = Databases.connect())
            {
                return db.Query<Topicgroups>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询in操作
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static List<Topicgroups> QueryIn(int[] ids, string cond = "id")
        {
            using (var db = Databases.connect())
            {
                var sql = "select * from Person where" + cond + " in @ids";
                //参数类型是Array的时候，dappper会自动将其转化
                return db.Query<Topicgroups>(sql, new { ids }).ToList();
            }
        }


    }
}
