using QP.Prospectus.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QP.Prospectus.Controllers
{
    public class ConfigeController : Controller
    {

      
        public ActionResult Index()
        {
            ///给输入框赋值
            var db = GolbalDbContext.Instace().confige;
            foreach (var item in db.Where(u => u.id == 0))
            {
                ViewData["WebName"] = item.webName;
                ViewData["Brief"] = item.brief.Trim();
                ViewData["Parther"] = item.Parthers;
                ViewData["PayOut"] = item.PayOut.Trim();
                ViewData["keyWord"] = item.keyWord.Trim();
            }
            return View();
        }

        /// <summary>
        ///  修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection files)
        {
            var db = GolbalDbContext.Instace().confige.Find(0);
            if (files != null)
            {
                db.webName = files["WebName"].ToString().Trim();
                db.Parthers = files["Parthers"].ToString().Trim();
                db.brief = files["Webbrief"].ToString().Trim();
                db.PayOut = files["PayOut"].ToString().Trim();
                db.keyWord = files["keyWord"].ToString().Trim();
                GolbalDbContext.Instace().SaveChanges();
            }
            return RedirectToAction(Url.Action("Index","Home"));
        }
    }
}