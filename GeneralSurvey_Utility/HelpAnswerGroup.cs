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
    public static class HelpAnswerGroup
    {
        /// <summary>
        /// 单个插入，返回影响行数
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        public static int Insert(AnswerGroup model)
        {
            using (var db = Databases.connect())
            {
                return db.Execute(@"insert into `qp.answergroup`(id,Answer,FromID) values(@id,@Answer,@FromID);", model);
            }
        }

        /// <summary>
        ///   批量插入，返回影响行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(List<AnswerGroup> model)
        {
            using (var db = Databases.connect())
            {
                return db.Execute(@"insert into `qp.answergroup`(id,Answer,FromID) values(@id,@Answer,@FromID);", model);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            int remove = Databases.connect().Execute("delete from `qp.answergroup` where id =@id", new { id = id });
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
        public static bool Update(AnswerGroup model)
        {
            using (var db = Databases.connect())
            {
                int updateSet = db.Execute(@"update `qp.answergroup` set Answer=@Answer,FromID=@FromID where id=@id", new { model });
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
        public static bool Update(List<AnswerGroup> model)
        {

            using (var db = Databases.connect())
            {
                int updateSet = db.Execute(@"update `qp.answergroup` set Answer=@Answer,FromID=@FromID where id=@id", new { model });
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
        public static List<AnswerGroup> GetList(string cond)
        {
            string sql = @"select id,Answer,FromID from `qp.answergroup` where " + cond + " and FromID='" + Seesion.FromIds + "'";
            using (var db = Databases.connect())
            {
                return db.Query<AnswerGroup>(sql).ToList();
            }
        }


        /// <summary>
        /// 无条件查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static List<AnswerGroup> GetList()
        {
            string sql = @"select id,Answer,FromID  from `qp.answergroup`  where FromID='" + Seesion.FromIds + "'";
            using (var db = Databases.connect())
            {
                return db.Query<AnswerGroup>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询in操作
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static List<AnswerGroup> QueryIn(int[] ids, string cond = "id")
        {
            using (var db = Databases.connect())
            {
                var sql = "select * from `qp.answergroup` where" + cond + " in @ids";
                //参数类型是Array的时候，dappper会自动将其转化
                return db.Query<AnswerGroup>(sql, new { ids }).ToList();
            }
        }


    }
}
