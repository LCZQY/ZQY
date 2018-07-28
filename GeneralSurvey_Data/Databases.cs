using System;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;
using System.Text;

namespace GeneralSurvey_Data
{

    public static class Databases
    {
        
        private static MySqlConnection mySqlction = null;
        //private static string server = System.Configuration.ConfigurationManager.AppSettings["MySQLServer"];
        private static  readonly string ConnectString = "datasource=127.0.0.1;port=3306;Database=zqy;characterset=utf8;user=root;pwd='';SslMode=None;";

        public static MySqlConnection connect()
        {
            //MySqlConnectionStringBuilder sqlbulider = new MySqlConnectionStringBuilder();
            //sqlbulider.Server = "127.0.0.1";
            //sqlbulider.UserID = "root";
            //sqlbulider.Password = "root";
            //sqlbulider.CharacterSet = "utf8";
            //sqlbulider.Database = "qp.databases";
            //sqlbulider.AllowZeroDateTime = true;
            //sqlbulider.ConvertZeroDateTime = true;
            //sqlbulider.IntegratedSecurity = true;

            if (mySqlction == null)
            {
                mySqlction = new MySqlConnection(ConnectString);
            }
            
            return mySqlction;
        }

    }
}

        ///// <summary>
        ///// 分页列表
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="model"></param>
        ///// <param name="count"></param>
        ///// <param name="context"></param>
        ///// <param name="transaction"></param>
        ///// <returns></returns>
        //public List<T> GetQueryManyForPage(SelectBuilder data, T model, out int count, SQLiteConnection context = null, IDbTransaction transaction = null)
        //{
        //    if (context == null) context = Db.GetInstance().Context();
        //    string sqlStr = Db.GetInstance().GetSqlForSelectBuilder(data);
        //    string sqlStrCount = Db.GetInstance().GetSqlForTotalBuilder(data);
        //    List<T> list = context.Query<T>(sqlStr, model, transaction).ToList();
        //    count = ZConvert.StrToInt(context.ExecuteScalar(sqlStrCount, model, transaction), 0);
        //    return list;
        //}


//        /// <summary>
//        /// 添加
//        /// </summary>
//        /// <param name="query"></param>
//        /// <param name="model"></param>
//        /// <param name="context"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
        

//        public int Add(string query, string parameter)
//        {                                  
//            int id = mySqlction.Execute(query,new {i parameter });
//            return id;
//        }


//        /// <summary>
//        /// 修改
//        /// </summary>
//        /// <param name="query"></param>
//        /// <param name="model"></param>
//        /// <param name="context"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public int Update(string query, T model, SQLiteConnection context = null, IDbTransaction transaction = null)
//        {
//            if (context == null) context = Db.GetInstance().Context();
//            int id = context.Execute(query, model, transaction);
//            return id;
//        }
//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="tablename"></param>
//        /// <param name="wheresql"></param>
//        /// <param name="model"></param>
//        /// <param name="context"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public int Delete(string tablename, string wheresql, T model, SQLiteConnection context = null, IDbTransaction transaction = null)
//        {
//            if (context == null) context = Db.GetInstance().Context();
//            string query = "DELETE FROM  " + tablename + "  WHERE " + wheresql;
//            int id = context.Execute(query, model, transaction);
//            return id;
//        }
//        /// <summary>
//        /// 获取单个实体
//        /// </summary>
//        /// <param name="tablename"></param>
//        /// <param name="wheresql"></param>
//        /// <param name="model"></param>
//        /// <param name="context"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public T GetModel(string tablename, string wheresql, T model, SQLiteConnection context = null, IDbTransaction transaction = null)
//        {
//            if (context == null) context = Db.GetInstance().Context();
//            string query = "SELECT  * FROM " + tablename + " WHERE " + wheresql + "  LIMIT 0,1";
//            T obj = context.Query<T>(query, model, transaction).SingleOrDefault();
//            return obj;
//        }

//        /// <summary>
//        /// 实体列表
//        /// </summary>
//        /// <param name="query"></param>
//        /// <param name="model"></param>
//        /// <param name="context"></param>
//        /// <param name="transaction"></param>
//        /// <returns></returns>
//        public List<T> GetModelList(string query, T model, SQLiteConnection context = null, IDbTransaction transaction = null)
//        {
//            if (context == null) context = Db.GetInstance().Context();
//            List<T> list = context.Query<T>(query, model, transaction).ToList();
//            return list;
//        }

//        #region 分页语句拼接
//        public string GetSqlForSelectBuilder(SelectBuilder data)
//        {
//            var sql = "";
//            sql = "select " + data.Select;
//            sql += " from " + data.From;
//            if (data.WhereSql.Length > 0)
//                sql += " where " + data.WhereSql;
//            if (data.GroupBy.Length > 0)
//                sql += " group by " + data.GroupBy;
//            if (data.Having.Length > 0)
//                sql += " having " + data.Having;
//            if (data.OrderBy.Length > 0)
//                sql += " order by " + data.OrderBy;
//            if (data.PagingItemsPerPage > 0
//                && data.PagingCurrentPage > 0
//                )
//            {
//                sql += string.Format(" limit {0} offset {1}", data.PagingItemsPerPage, ((data.PagingCurrentPage * data.PagingItemsPerPage) - data.PagingItemsPerPage + 1) - 1);
//            }
//            return sql;
//        }

//        public string GetSqlForTotalBuilder(SelectBuilder data)
//        {
//            var sql = "";
//            sql = "select count(*)";
//            sql += " from " + data.From;
//            if (data.WhereSql.Length > 0)
//                sql += " where " + data.WhereSql;
//            return sql;
//        }
//    }



//}

//#region SelectBuilder
//public class SelectBuilder
//{
//    public int PagingCurrentPage { get; set; }
//    public int PagingItemsPerPage { get; set; }


//    private string _Having = "";
//    public string Having
//    {
//        set { _Having = value; }
//        get { return _Having; }
//    }



//    private string _GroupBy = "";
//    public string GroupBy
//    {
//        set { _GroupBy = value; }
//        get { return _GroupBy; }
//    }



//    private string _OrderBy = "";
//    public string OrderBy
//    {
//        set { _OrderBy = value; }
//        get { return _OrderBy; }
//    }


//    private string _From = "";
//    public string From
//    {
//        set { _From = value; }
//        get { return _From; }
//    }



//    private string _Select = "";
//    public string Select
//    {
//        set { _Select = value; }
//        get { return _Select; }
//    }

//    private string _WhereSql = "";
//    public string WhereSql
//    {
//        set { _WhereSql = value; }
//        get { return _WhereSql; }
//    }




//}


//}



