using System;
using System.Linq;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using GeneralSurvey_Data;

namespace GeneralSurvey_Data
{


    /// <summary>    
    ///  读取appsetting 配置文件   
    /// </summary>
    public static class ConfiguraionManger
    {
        public static IConfiguration _Configuration;
        public static IConfiguration ReadAppsettings()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            return _Configuration = configurationBuilder
                //获取当前项目的根目录
                .SetBasePath(Directory.GetCurrentDirectory())
                //得到配置文件
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

    }
}

public sealed class Databases
{
    private static MySqlConnection mySqlction = null;
    private static string ConnectString { get; } = ConfiguraionManger.ReadAppsettings().GetConnectionString("mySqlConnction");
    private static volatile Databases _instance;
    private static readonly object obj = new object();
    public static MySqlConnection Instance
    {
        //MySqlConnectionStringBuilder sqlbulider = new MySqlConnectionStringBuilder(); ？？        
        get
        {
            ///保证对象实例化后不需要对待
            if (_instance == null)
            {
                /// 当外部是以多线程访问的的时候，因为是多个任务同时进行，连接串会被多次实例化，加上一个锁保证对像只能被实例化一次，但是
                ///在多任务同时进去时候，会造成排队现象！造成资源浪费                
                lock (obj)
                {
                    if (mySqlction == null)
                    {
                        mySqlction = new MySqlConnection(ConnectString);
                    }
                }
            }
            return mySqlction;
        }
    }

}




