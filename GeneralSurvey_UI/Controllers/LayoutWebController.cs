using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralSurvey_Utility;
using GeneralSurvey_Data.Model;
using GeneralSurvey_Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace GeneralSurvey_UI.Controllers
{
    /// <summary>
    ///  排版设置
    /// </summary>

    [Authorize]
    public class LayoutWebController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string id)
        {
            if (HelpTopicgroup.Delete(id))
            {
                return Json(ResultMsg.FormatResult());
            }
            return Json(ResultMsg.FormatResult(0, "删除失败", "删除失败"));
        }
        /// <summary>
        ///  编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            List<Topicgroups> edit = new List<Topicgroups>();
            edit = HelpTopicgroup.GetList("id='"+ id +"'");
            foreach (var item in edit)
            {
                Seesion._stide = item.Stide.ToString();
            }
            ViewData["EditData"] = edit;
            return View();
        }
        [HttpPost]
        public IActionResult Edit()
        {
            try
            {
                var topicID = Request.Form["id"];
                var stIdes = Request.Form["Stide"];
                var stsettingId = Request.Form["SetsettingId"];
                var topicName = Request.Form["TopicName"];
                var charactersSize = Request.Form["CharactersSize"];
                var optionText = Request.Form["OptionText"];
                ///题号修改规则
                /// 先判断该题号是否修改，如果没有动直接修改全部
                /// 如果题号不一致 ， 去查询是否存在改动题号， 题号存在，内容和题号全部替换  不存在直接修改题号
                if (stIdes == Seesion._stide)
                {
                    string fromName = Seesion._stide + stsettingId + Seesion.sj.Next(10, 100).ToString();
                    Topicgroups updateModel = new Topicgroups()
                    {
                        TopicName = topicName,
                        CharactersSize = charactersSize,
                        SetsettingId = stsettingId,
                        OptionText = optionText,
                        Stide = int.Parse(Seesion._stide),
                        id = topicID,
                        FromName = fromName,
                        FromID = Seesion.FromIds
                    };
                    if (HelpTopicgroup.Update(updateModel))
                    {
                        return RedirectToAction("Index");
                    }
                    return Json(ResultMsg.FormatResult(0, "修改失败", "修改失败"));
                }
                else
                {


                    //把用户修改的题号 拿去数据库中对比，如果存在就两两相互替换，不修改其他内容，不存在就直接修改，并且修改所用
                    var existStide = Databases.Instance.Query<Topicgroups>("select  id,TopicName,CharactersSize,SetsettingId ,OptionText ,Stide from  `qp.topicgroup`  where Stide =@Stide", new
                    {
                        Stide = stIdes
                    });
                    if (existStide.Count() > 0)
                    {
                        try
                        {
                            Topicgroups updateModel;
                            foreach (var item in existStide)
                            {
                                string fromNamed = Seesion._stide + item.SetsettingId + Seesion.sj.Next(10, 100).ToString();
                                // 更改成被修改的题号，同时内容也全部替换
                                updateModel = new Topicgroups()
                                {
                                    id = Guid.NewGuid().ToString(),
                                    TopicName = item.TopicName,
                                    CharactersSize = item.CharactersSize,
                                    SetsettingId = item.SetsettingId,
                                    OptionText = item.OptionText,
                                    Stide = int.Parse(Seesion._stide),
                                    FromName = fromNamed,
                                    FromID = Seesion.FromIds
                                };

                                HelpTopicgroup.Insert(updateModel);
                                HelpTopicgroup.Delete(item.id);
                            }
                            HelpTopicgroup.Delete(topicID);
                            //就相当于新增加一组数据，把新的题号带上 ！！！！                             
                            string fromName = stIdes + stsettingId + Seesion.sj.Next(10, 100).ToString();
                            updateModel = new Topicgroups()
                            {
                                TopicName = topicName,
                                CharactersSize = charactersSize,
                                SetsettingId = stsettingId,
                                OptionText = optionText,
                                id = topicID,
                                Stide = int.Parse(stIdes),
                                FromName = fromName,
                                FromID = Seesion.FromIds
                            };

                            HelpTopicgroup.Insert(updateModel);
                            return View("Index");
                        }
                        catch (Exception el)
                        {
                            return Json(ResultMsg.FormatResult(el));
                        }
                    }
                    else
                    {
                        //不存在这个题号的时候直接修改题号不需要搭理内容
                        try
                        {
                            Databases.Instance.Execute("update `qp.topicgroup` set Stide =@Stide where id=@id", new
                            {
                                id = topicID,
                                Stide = stIdes
                            });
                            return RedirectToAction("Index");
                        }
                        catch (Exception el)
                        {
                            return Json(ResultMsg.FormatResult(el));
                        }
                    }
                }
            }
            catch (Exception el)
            {
                return Json(ResultMsg.FormatResult(el));
            }
        }

        /// <summary>
        ///  组合 layui表格 json
        /// </summary>
        /// <returns></returns>
        public IActionResult jsonTable()
        {
            List<Topicgroups> query = new List<Topicgroups>();
            query = HelpTopicgroup.GetList();
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