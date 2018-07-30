using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralSurvey_Utility;
using GeneralSurvey_Data.Model;
using System.Text;
using System.IO;
using GeneralSurvey_Data;
using Dapper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;

namespace GeneralSurvey_UI.Controllers
{
    public class AdminController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public AdminController(IHostingEnvironment hosting)
        {
            _hostingEnvironment = hosting;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string passWord)
        {
            return Redirect(Url.Action("Index", "Home"));
        }


        /// <summary>
        ///  显示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Exhibition()
        {

            ViewData["theader"] = HelpTopicgroup.GetList();
            return View(HelpAnswerGroup.GetList().ToList());
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var entity = HelpAnswerGroup.GetList().FirstOrDefault(u => u.id == id);
            if (entity != null)
            {
                return View("Details", entity);
            }
            return RedirectToAction("Exhibition");
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            // ???? 所有的Js 都没有弹出                    
            var entity = HelpAnswerGroup.GetList().FirstOrDefault(u => u.id == id);
            if (entity != null)
            {
                if (HelpAnswerGroup.Delete(id))
                {
                    return RedirectToAction("Exhibition");
                }
                return Content("<script>alert('删除失败 ,请重试');window.history.back(-1); </script>");
                // 删除信息同时是否删除文件夹？？？  

            }
            else
            {
                return Content("<script>alert('删除失败,请重试');window.history.back(-1); </script>");
            }

        }

        /// <summary>
        ///  文件下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Download(string id)
        {

            var FilesPath = HelpAnswerGroup.GetList().FirstOrDefault(u => u.id == id).Answer;
            if (FilesPath != null)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(FilesPath);
                foreach (var path in jo["Values"])
                {
                    ///判断是否为路径格式，如果存在就直接显示下载
                    if (path.ToString().IndexOf("/") > -1)
                    {
                        try
                        {
                            //  var wwwroot = _hostingEnvironment.ContentRootPath + path;
                            //判断该文件是否存在？？？？？
                            //if (System.IO.File.Exists(path.ToString()))
                            //{
                            /// 虚拟文件地址输出下载     
                            //}
                            //return Content("<script>alert('该文件已丢失,请重试');window.history.back(-1); </script>");
                            return File(path.ToString(), "application/vnd.android.package-archive", Path.GetFileName(path.ToString()));
                        }
                        catch (Exception el)
                        {
                            return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");
                        }

                    }
                }
            }
            return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");

        }




        /// <summary>
        ///  添加问卷
        /// </summary>
        /// <returns></returns>
        public IActionResult AddSurvey()
        {

            return View();
        }

    }
}