using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GeneralSurvey_UI.Controllers
{

    //AuthenticationSchemes=CookieAuthenticationDefaults.AuthenticationScheme 找到保存的Cookies值，才允许登陆
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(IHostingEnvironment hosting, IHttpContextAccessor contextAccessor)
        {
            _hostingEnvironment = hosting;
            _httpContextAccessor = contextAccessor;
        }

        //[AllowAnonymous] //允许访问
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string passWord)
        {
            if (userName != null)
            {
                try
                {
                   /**
                    * 登陆后获取token , 
                    * 获取传递的token，去保存用户信息
                    **/
                    ClaimsIdentity identity = new ClaimsIdentity("Forms");
                    identity.AddClaim(new Claim(ClaimTypes.Sid, "Admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    Seesion.UserName = User.Identity.Name;
                    var queryId = Databases.Instance.Query<Formsettings>("select FormID from `qp.formsettings` where FormCreater = '" + userName + "'").Single().FormID;
                    Seesion.FromIds = queryId;
                }
                catch (Exception ex)
                {
                    Seesion.FromIds = "1";
                }
                return Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                return Json(ResultMsg.FormatResult(0, "登陆失败", "失败"));
            }
        }


        /// <summary>
        ///  显示数据
        /// </summary>
        /// <returns></returns>    
        public ActionResult Exhibition(string fid)
        {
            ViewData["theader"] = HelpTopicgroup.GetList("FromID='" + fid + "'");
            return View(HelpAnswerGroup.GetList("FromID='" + fid + "'").ToList());
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        /// 
        [Authorize]
        public IActionResult AddSurvey()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSurvey(string title, string brief, string copy)
        {
            try
            {
                // 表单ID 
                string formId = Guid.NewGuid().ToString();
                Formsettings InsertModel = new Formsettings()
                {
                    FormID = formId,
                    FormNote = brief,
                    FormTitle = title,
                    FormCopyright = copy,
                    FormCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                    FormStatus = 1,
                    FormCreater = Seesion.UserName
                };
                //给全局的表单ID赋值
                var InsertState = HelpFormsettings.Insert(InsertModel);
                Seesion.FromIds = formId;
                if (InsertState > 0)
                {
                    return Json(ResultMsg.FormatResult(200, Seesion.FromIds, Seesion.UserName));
                }
                return Json(ResultMsg.FormatResult(0, "数据添加失败", "失败"));
            }
            catch (Exception el)
            {
                return Json(ResultMsg.FormatResult(el));
            }
        }
    }
}