﻿using System;
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

namespace GeneralSurvey_UI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  内容页面一
        /// </summary>
        /// <returns></returns>
        public ActionResult ContentPage()
        {
            // 赋值题号
            try
            {
                int order = int.Parse(Databases.connect().Query<Topicgroups>("select Stide from `qp.topicgroup`").Max(u => u.Stide).ToString()) + 1;
                ViewData["stide"] = order.ToString();
            }
            catch (Exception el) { return Json(data: ResultMsg.FormatResult(el)); }

            List<Setsetting> query = new List<Setsetting>() { };
            //类里面的字段要和表结构要一一对应
            query = Databases.connect().Query<Setsetting>("select id,TopicClass from `qp.setsetting`").ToList();
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}