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
    public class ShowTextController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public ShowTextController(IHostingEnvironment hosting) // 依赖注入类. Net Core已经没有了Server.MapPath() 属性了
        {
            _hostingEnvironment = hosting;
        }


        public IActionResult Index()
        {        
            ////问题形式
            ViewData["query"] = HelpTopicgroup.GetList();
            return View();
        }


        /// <summary>
        ///  保存所有的提交的数据， 先根据题号区分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IFormCollection model)
        {
            //// .Net Core  中获取不到上传的文件        ??????????????????? 
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>() { };       
            foreach (var item in model)
            {
                if (item.Key.IndexOf("_") != 0)
                {
                    keyValuePairs.Add(item.Key, item.Value);
                }
            }                   
            AnswerGroup InsertModel = new AnswerGroup()
            {
                id = Guid.NewGuid().ToString(),
                FromID = Seesion.FromIds,
                Answer = JObject.FromObject(new{ FormName = keyValuePairs.Keys, Values = keyValuePairs.Values }).ToString()
            };     
            try
            {
                int flage = HelpAnswerGroup.Insert(InsertModel);
                if (flage > 0)
                {
                    return Json(new { keys = keyValuePairs.Keys, values = keyValuePairs.Values });
                }
                return Json("插入失败");
            } catch (Exception el){ return Json(ResultMsg.FormatResult(el)); }
        }



        

        public async Task<IActionResult> FileSave(List<IFormFile> files)
        {          
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filename = formFile.FileName;
                    string fileExt = filename.Substring(filename.IndexOf("."), filename.Length - filename.IndexOf(".")); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    var filePath = webRootPath + "/Files/" + newFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size });
        }

        /// <summary>
        ///  接受文件 【在设置上传文件的时候还要考虑其中的需要什么格式】
        /// </summary>
        /// <returns></returns>
        //public bool FileSave()
        //{
        //    var date = Request;
        //    var files = Request.Form.Files;
        //    long size = files.Sum(f => f.Length);
        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    string contentRootPath = _hostingEnvironment.ContentRootPath;
        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var filename = formFile.FileName;
        //            string fileExt = filename.Substring(filename.IndexOf("."), filename.Length - filename.IndexOf(".")); //文件扩展名，不含“.”
        //            long fileSize = formFile.Length; //获得文件大小，以字节为单位
        //            string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
        //            var filePath = webRootPath + "/Files/" + newFileName;
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                 formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //    return true;         
        //}


    }
}