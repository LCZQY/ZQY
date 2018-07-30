using System;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using Dapper;
using GeneralSurvey_Data.Model;

namespace MysqlConnstions
{
    class Program
    {
        static void Main(string[] args)
        {
            connstionMysql connstion = new connstionMysql();

        }
    }
  
    public class connstionMysql
    {
        private readonly string ConnectString = "datasource=127.0.0.1;port=3306;Database=zqy;characterset=utf8;user=root;pwd='';SslMode=None;";

        public connstionMysql()
        {
            using (IDbConnection mySqlction = new MySqlConnection(ConnectString))
            {
                try                                
                {

                    //// 新增数据
                    //mySqlction.Execute("insert into `qp.setsetting` values(@id,@TopicClass)",new {  id="0002", TopicClass= "新增数据"});
                    //// 修改数据
                    //mySqlction.Execute("update `qp.setsetting`  set id='0001' where id=@id", new { id = "001" });

                    // 查询
                    var querys = mySqlction.Query<AnswerGroup>("SELECT * FROM `qp.answergroup`");
                    foreach (var item in querys)
                    {
                        // 直接把字符串转成Json的形式
                        JObject jo = (JObject)JsonConvert.DeserializeObject(item.Answer);                        
                        foreach (var objects in jo["Values"])
                        {
                            Console.WriteLine(objects);
                        }
                    }

                    ///////  删除数据
                    //mySqlction.Execute("delete from `qp.setsetting` where id =@id", new { id = "0002" });

                    ////制定查询
                    //var find = mySqlction.Query<Setsetting>("select * from  `qp.setsetting` where id =@id", new { id= "0002" });
                    
                    //foreach (var item in find)
                    //{
                    //    Console.WriteLine(item.id.ToString() + "," + item.TopicClass.ToString());
                    //}
                    
                    

                    //return;
                    //mySqlction.Open();

                    //var itemJson = new
                    //{
                    //    firstName =new { item1 ="0"  },
                    //    lastName = "McLauglin",
                    //    email = "25361560685@qq.com"
                    //};                    
                    //var json = JObject.FromObject(itemJson);
                    //Console.WriteLine(json);


                    //var list = new Person[10];
                    //for (var i = 0; i < 10; i++)
                    //{
                    //    var data = new Person
                    //    {
                    //        Name = "Changwei_" + i,
                    //        Age = i + 20
                    //    };
                    //    list[i] = data;
                    //}

                    //var jsonSerializer = new JsonSerializer();
                    //var stringWriter = new StringWriter();
                    //var jsonWriter = new JsonTextWriter(stringWriter);
                    //jsonSerializer.Serialize(jsonWriter, list);

                    //Console.WriteLine(stringWriter.ToString());

                    //var jsonArray = JArray.FromObject(list);
                    //Console.WriteLine(jsonArray.ToString());

                    // MySqlCommand Command = new MySqlCommand("insert into questions(topic)  values('" + json + "');", mySqlction);
                    ////Command.Parameters.AddWithValue("@id",1);
                    ////Command.Parameters.AddWithValue("@topic", Newtonsoft.Json.JsonConvert.SerializeObject(sql));                
                    //Command.ExecuteNonQuery();
                    //Console.WriteLine("插入完成");                        
                }
                catch (Exception el)
                {
                    Console.WriteLine(el.ToString());
                    mySqlction.Close();
                }
                finally
                {
                    mySqlction.Close();
                }
            }
        }


    }
}
