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
    public static class HelpFormsettings
    {

        /// <summary>
        /// 单个插入，返回影响行数
        /// </summary>
        /// <param name="topicgroups"></param>
        /// <returns></returns>
        public static int Insert(Formsettings model)
        {
            using (var db = Databases.Instance)
            {
                return db.Execute(@"insert into `qp.formsettings`(FormID,FormNote,FormTitle,FormCopyright,FormCreateDate,FormStatus,FormCreater) values(@FormID,@FormNote,@FormTitle,@FormCopyright,@FormCreateDate,@FormStatus,@FormCreater);", model);
            }
        }

        /// <summary>
        ///   批量插入，返回影响行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(List<Formsettings> model)
        {
            using (var db = Databases.Instance)
            {
                return db.Execute(@"insert into `qp.formsettings`(FormID,FormNote,FormTitle,FormCopyright,FormCreateDate,FormStatus,FormCreater) values(@FormID,@FormNote,@FormTitle,@FormCopyright,@FormCreateDate,@FormStatus,@FormCreater);", model);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public static bool Delete(string FormID)
        {
            int remove = Databases.Instance.Execute("delete from `qp.formsettings` where FormID =@FormID", new { FormID = FormID });
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
        public static bool Update(Formsettings model)
        {
            using (var db = Databases.Instance)
            {
                int updateSet = db.Execute(@"update `qp.formsettings` set Answer=@Answer,FromID=@FromID where FormID=@FormID", new { model });
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
        public static bool Update(List<Formsettings> model)
        {

            using (var db = Databases.Instance)
            {
                int updateSet = db.Execute(@"update `qp.formsettings` set Answer=@Answer,FromID=@FromID where FormID=@FormID", new { model });
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
        public static List<Formsettings> GetList(string cond)
        {
            string sql = @"select FormID,FormNote,FormTitle,FormCopyright,FormCreateDate,FormStatus,FormCreater from `qp.formsettings` where " + cond + "";
            using (var db = Databases.Instance)
            {
                return db.Query<Formsettings>(sql).ToList();
            }
        }


        /// <summary>
        /// 无条件查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static List<Formsettings> GetList()
        {
            string sql = @"select FormID,FormNote,FormTitle,FormCopyright,FormCreateDate,FormStatus,FormCreater  from `qp.formsettings` where FormCreater='" + Seesion.UserName + "'";
            using (var db = Databases.Instance)
            {
                return db.Query<Formsettings>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询in操作
        /// </summary>
        /// <param name="FormIDs"></param>
        /// <returns></returns>
        public static List<Formsettings> QueryIn(int[] FormIDs, string cond = "FormID")
        {
            using (var db = Databases.Instance)
            {
                var sql = "select * from `qp.formsettings` where" + cond + " in @FormIDs";
                //参数类型是Array的时候，dappper会自动将其转化
                return db.Query<Formsettings>(sql, new { FormIDs }).ToList();
            }
        }


    }
}
