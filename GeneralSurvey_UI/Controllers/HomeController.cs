using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralSurvey_UI.Models;
using GeneralSurvey_Data;
using GeneralSurvey_Utility;
using GeneralSurvey_Data.Model;
using MySql.Data.MySqlClient;
using Dapper;
using Newtonsoft;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.AspNetCore.Http;
using GeneralSurvey_Utility.Filter;

namespace GeneralSurvey_UI.Controllers
{
    public class HomeController : Controller
    {
        [PermissionRequired]
        public IActionResult Index()
        {
            return View();

        }

        /// <summary>
        ///添加提项
        /// </summary>
        /// <returns></returns>
        public ActionResult ContentPage()
        {
            // 赋值题号
            try
            {
                int order = int.Parse(Databases.Instance.Query<Topicgroups>("select Stide from `qp.topicgroup`").Max(u => u.Stide).ToString()) + 1;
                ViewData["stide"] = order.ToString();
            }
            catch (Exception el) { return Json(data: ResultMsg.FormatResult(el)); }

            List<Setsetting> query = new List<Setsetting>() { };
            //模型类里面的字段要和表结构要一一对应
            query = Databases.Instance.Query<Setsetting>("select id,TopicClass from `qp.setsetting`").ToList();
            ViewData["Group"] = new SelectList(Dropdown.createDropdown(query), "Key", "Value");
            return View();
        }


        /// <summary>
        ///  添加数据
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public IActionResult ContentPage(string Stides, string interest, string TopicName, string CharactersSize, string OptionSet)
        {
            try
            {
                //name关键字生成规则【题号+选项ID+10-99随机数】        
                string fromName = Stides + interest + Seesion.sj.Next(10, 100).ToString();
                CharactersSize = CharactersSize == null ? "0" : CharactersSize;
                Stides = Stides == null ? "0" : Stides;
                Topicgroups InsertModel = new Topicgroups()
                {
                    id = Guid.NewGuid().ToString(),
                    TopicName = TopicName,
                    CharactersSize = CharactersSize,
                    SetsettingId = interest,
                    OptionText = OptionSet,
                    Stide = int.Parse(Stides),
                    FromName = fromName,
                    FromID = Seesion.FromIds
                };
                int request = HelpTopicgroup.Insert(InsertModel);
                if (request > 0)
                {
                    return Json(data: ResultMsg.FormatResult());
                }
                return Json(data: ResultMsg.FormatResult(0, "插入数据库失败", "错误"));

            }
            catch (Exception ex) { return Json(data: ResultMsg.FormatResult(ex)); }
        }


        /// <summary>
        ///   添加成功
        /// </summary>
        /// <returns></returns>
        public IActionResult AddSucceed()
        {
            return View();
        }

        /// <summary>   
        /// 查看当前用户添加的
        /// </summary>
        /// <returns></returns>
        public IActionResult LookSurvey()
        {
            return View();
        }
        /// <summary>
        ///  组合 layui表格 json
        /// </summary>
        /// <returns></returns>
        public IActionResult jsonTable()
        {
            List<Formsettings> query = new List<Formsettings>();

            query = HelpFormsettings.GetList();// Databases.Instance.Query<Formsettings>("select FormID,FormNote,FormTitle,FormCopyright,FormCreateDate,FormStatus,FormCreater from `qp.formsettings`").ToList();
            var tableJson = new
            {
                count = query.Count(),  //总行数
                code = 0, //状态码 0 成功
                msg = "操作成功",
                data = query
            };
            return Json(tableJson);
        }
    }
}
