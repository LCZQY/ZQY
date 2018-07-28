using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using QP.Prospectus.Models;
using QP.Prospectus.Utility;
namespace QP.Prospectus.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewData["WebName"] = GolbalDbContext.Instace().confige.Find(0).webName;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string passWord)
        {
            return RedirectToAction("Exhibition");
        }

        /// <summary>
        ///  显示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Exhibition()
        {
            ViewData["WebName"] = GolbalDbContext.Instace().confige.Find(0).webName;
            return View(GolbalDbContext.Instace().businessplan.ToList());
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ViewData["WebName"] = GolbalDbContext.Instace().confige.Find(0).webName;
            var db = GolbalDbContext.Instace();
            var busines = db.businessplan.FirstOrDefault(u => u.Id == id);
            if (busines != null)
            {

                return View("Details", busines);
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
            var db = GolbalDbContext.Instace();
            var entity = db.businessplan.SingleOrDefault(u => u.Id == id);            
            if (entity != null)
            {               
                db.businessplan.Remove(entity);
                db.SaveChanges();
              // 删除信息同时是否删除文件夹？？？  
                
            }
            return RedirectToAction("Exhibition");
        }

        /// <summary>
        ///  文件下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Download(string id)
        {

            var filepath = GolbalDbContext.Instace().businessplan.FirstOrDefault(u => u.Id == id).FilePath;
            if (filepath != null)
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + filepath;
                    string fileName = filepath.Split('/')[3];
                    return File(new FileStream(path, FileMode.Open), "application/octet-stream", fileName);
                }
                catch (Exception el)
                {
                    return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");
                }


            }
            else
            {
                return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");
            }
        }
    }
}