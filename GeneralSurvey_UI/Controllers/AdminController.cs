using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneralSurvey_Utility;
using GeneralSurvey_Data.Model;
using System.Text;

namespace GeneralSurvey_UI.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
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

            ViewData["theader"] = HelpTopicgroup.GetList();


            return View(HelpAnswerGroup.GetList().ToList());     
        }



        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            return View();
            //ViewData["WebName"] = GolbalDbContext.Instace().confige.Find(0).webName;
            //var db = GolbalDbContext.Instace();
            //var busines = db.businessplan.FirstOrDefault(u => u.Id == id);
            //if (busines != null)
            //{

            //    return View("Details", busines);
            //}
            //return RedirectToAction("Exhibition");
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            return View();
            //var db = GolbalDbContext.Instace();
            //var entity = db.businessplan.SingleOrDefault(u => u.Id == id);
            //if (entity != null)
            //{
            //    db.businessplan.Remove(entity);
            //    db.SaveChanges();
            //    // 删除信息同时是否删除文件夹？？？  

            //}
            //return RedirectToAction("Exhibition");
        }

        /// <summary>
        ///  文件下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Download(string id)
        {

            //var filepath = GolbalDbContext.Instace().businessplan.FirstOrDefault(u => u.Id == id).FilePath;
            //if (filepath != null)
            //{
            //    try
            //    {
            //        string path = AppDomain.CurrentDomain.BaseDirectory + filepath;
            //        string fileName = filepath.Split('/')[3];
            //        return File(new FileStream(path, FileMode.Open), "application/octet-stream", fileName);
            //    }
            //    catch (Exception el)
            //    {
            //        return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");
            //    }
            //}
            //else
            //{
            //    return Content("<script>alert('文件下载失败,请重试');window.history.back(-1); </script>");
            //}



            return View();
        }


    }
}