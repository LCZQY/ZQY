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
        private static readonly string ConnectString = "datasource=127.0.0.1;port=3306;Database=qp.databases;characterset=utf8;user=root;pwd='root';SslMode=None;";
        //private static  readonly string ConnectString = "datasource=127.0.0.1;port=3306;Database=zqy;characterset=utf8;user=root;pwd='';SslMode=None;";

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
