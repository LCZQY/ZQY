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
            if (formid == null)
            {
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
        ///   List<IFormFile> files 该参数必须和  文件框中的 name一致
        /// </returns>
        [HttpPost]
        public IActionResult Index(IFormCollection model, List<IFormFile> files)
        {
            var Path = FileSave();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>() { };
            foreach (var item in model)
            {
                if (item.Key.IndexOf("_") != 0)
                {
                    //前端会上传类型路径的字符串，如果存在就替换成上传路径！
                    if (item.Value.ToString().IndexOf("\\") > -1)
                    {
                        keyValuePairs.Add(item.Key, Path);
                    }
                    else
                    {
                        keyValuePairs.Add(item.Key, item.Value);
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
                    return Json(ResultMsg.FormatResult());
                }
                return Json("插入失败");
            }
            catch (Exception el) { return Json(ResultMsg.FormatResult(el)); }

        }

        /// <summary>
        ///  接受文件 
        /// </summary>
        /// <returns></returns>
        public string FileSave()
        {
            var date = Request;
            var files = Request.Form.Files;
            //long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;
            var filename = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filename = formFile.FileName;
                    //获得文件大小，以字节为单位
                    long fileSize = formFile.Length;
                    //文件夹命名规则： 当前日期          
                    var filePath = webRootPath + "/Files/" + DateTime.Now.ToShortDateString().Replace("-", "");
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
            return "/Files/" + DateTime.Now.ToShortDateString().Replace("-", "") + "/" + filename;
        }
      

    }
}