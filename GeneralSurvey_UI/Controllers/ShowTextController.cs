using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralSurvey_Data;
using GeneralSurvey_Data.Model;
using GeneralSurvey_Utility;
using Dapper;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
namespace GeneralSurvey_UI.Controllers
{
    /// <summary>
    ///  手机端--调研问卷页面
    /// </summary>    
    public class ShowTextController : Controller
    {
        // 依赖注入类读取本地wwwroot文件夹  .Net Core已经没有了Server.MapPath() 属性了
        private IHostingEnvironment _hostingEnvironment;
        public ShowTextController(IHostingEnvironment hosting)
        {
            _hostingEnvironment = hosting;
        }

        public IActionResult Index(string formid)
        {
            // 如果没有formid为空直接展示默认的调研问卷
            if (formid == null)
            {
                var name = User.Identity.Name;                
                ViewData["query"] = HelpTopicgroup.GetList();
            }
            else
            {
                Seesion.FromIds = formid;
                ViewData["query"] = HelpTopicgroup.GetList("FromID='" + formid + "'");
            }
            return View();
        }

        /// <summary>
        ///  保存所有的提交的数据
        /// </summary>
        /// <returns>
        /// <IFormFile> files 该参数必须和  文件框中的 name一致
        [HttpPost]
        public IActionResult Index(IFormCollection model, List<IFormFile> files)
        {
            //在文件上传的时候， 只能采用表单的提交后台才能够获取到文件？？ 而Ajax的方式不行？
            string paths = FileSave(files);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
            {
            };
            foreach (var item in model)
            {
                // 去掉莫名上传的Cookies防伪标志
                if (item.Key.IndexOf("_") != 0)
                {
                    //前端会上传类型路径的字符串，如果存在就替换成上传路径！
                    if (item.Value.ToString().IndexOf("\\") > -1)
                    {
                        keyValuePairs.Add(item.Key, paths);
                    }
                    else
                    {
                        string Value=item.Value.ToString() == "" ? "...." : item.Value.ToString();
                        //keyValuePairs.Add(item.Key, item.Value);
                        keyValuePairs.Add(item.Key, Value);
                    }
                }
            }
            AnswerGroup InsertModel = new AnswerGroup()
            {
                id = Guid.NewGuid().ToString(),
                FromID = Seesion.FromIds,
                Answer = JObject.FromObject(new { FormName = keyValuePairs.Keys, Values = keyValuePairs.Values }).ToString()
            };
            try
            {
                int flage = HelpAnswerGroup.Insert(InsertModel);
                if (flage > 0)
                {
                    //RedirectToAction 只能够在本页面跳转
                    return RedirectToAction("Succeed");
                }
                return Json("插入失败");
            }
            catch (Exception el)
            {
                return Json(ResultMsg.FormatResult(el));
            }
        }
        /// <summary>
        ///  接受文件 
        /// </summary>
        /// <returns></returns>
        public string FileSave(List<IFormFile> files)
        {
            var date = Request;
            var file = Request.Form.Files;
            //long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;          
            var filename = "";
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    filename += formFile.FileName;
                    //获得文件大小，以字节为单位
                    long fileSize = formFile.Length;
                    //文件夹命名规则： 当前日期          
                    var filePath = webRootPath + "/Files/" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //判断文件夹是否存在,若不存在则创建
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var stream = new FileStream(filePath + "/" + filename, FileMode.Create))
                    {
                        formFile.CopyToAsync(stream);
                    }
                }
            }
            return "/Files/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/" + filename;
        }


        /// <summary>
        ///   问卷添加成功
        /// </summary>
        /// <returns></returns>
        public IActionResult Succeed()
        {
            return View();
        }
    }
}