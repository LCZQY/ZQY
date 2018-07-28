using QP.Prospectus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QP.Prospectus.Utility;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using static System.Net.WebRequestMethods;

namespace QP.Prospectus.Controllers
{
    public class HomeController : Controller
    {
   
        public ActionResult Index()
        {  
            var db = GolbalDbContext.Instace().confige;

            foreach (var item in db.Where(u=>u.id==0))
            {
                ViewData["WebName"] = item.webName;
                ViewData["Brief"] =  item.brief.Trim();
                ViewData["Parther"] = Communal.ReturnList(item.Parthers).ToList();
                ViewData["PayOut"] = Communal.ReturnList(item.PayOut).ToList();
                ViewData["keyWord"] = Communal.ReturnList(item.keyWord).ToList();
            }               
            Dictionary<string, string> pairs = new Dictionary<string, string>() { };
            pairs.Add("男", "男");
            pairs.Add("女", "女");
            ViewData["Sex"] = new SelectList(pairs, "Key", "Value");
            return View();

        }


        [HttpPost]
        public ActionResult Index(FormCollection file, HttpPostedFileBase FilePath)
        {
            if (ModelState.IsValid)
            {
                var db = GolbalDbContext.Instace();
                
                var date = file["Birthday"] == null ? DateTime.Now : DateTime.Parse(file["Birthday"]);
                var payout = file["PayMoney"] == null ? 0 : decimal.Parse(file["PayMoney"]);
                string FilesName = UploadFile(file["Name"], FilePath);
                try
                {
                    db.businessplan.Add(new businessplan
                    {
                        Id = Guid.NewGuid().ToString(),
                        Birthday = date,
                        Partner = file["Partner"],
                        Address = file["Address"].Replace(",", ""),
                        CompanyUrl = file["CompanyUrl"],
                        C__CompanyName = file["C__CompanyName"],
                        C__ProjectPayOut = Communal.StringBuil(file["C__ProjectPayOut"]),
                        C__Setback = file["C__Setback"],
                        ProjectTantistop = Communal.StringBuil(file["ProjectTantistop"]),
                        Dictum = file["Dictum"],
                        Email = file["Email"],
                        InDetails = file["InDetails"],
                        Name = file["Name"],
                        PayMoney = payout,
                        Phone = file["Phone"],
                        ProjectDescribe = file["ProjectDescribe"],
                        Sex = file["Sex"],
                        ProjectName = file["ProjectName"],
                        TeamDescribe = file["TeamDescribe"],
                        WeChat = file["WeChat"],
                        FilePath = "/File/" + FilesName
                    });
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    return Content("<script>alert('信息添加失败，请在仔细核对..');window.location.href='home/index'</script>");
                }

                return Content("<script>alert('已收到您的信息，请耐心等待，我们会向您联系，请保持电话畅通.！');window.location.href='home/index'</script>");
            }
            else
            {
                ModelState.AddModelError("Error", "数据库添加失败");
                return Content("<script>alert('数据添加失败，请在仔细核对..')</script>");

            }
        }



        /// <summary>
        ///  添加成功
        /// </summary>
        /// <returns></returns>
        public ActionResult Succeed()
        {
            ViewBag.Message = "已收到您的信息，请耐心等待，我们会向您联系，请保持电话畅通.！";
            return View();
        }



        public string UploadFile(string usrName, HttpPostedFileBase FilePath)
        {
            if (FilePath == null)
            {
                return null;
            }
            try
            {
                string maPpath = HttpContext.Server.MapPath("/File/").Replace("\\", "/") + "/";
                string fileName = usrName + DateTime.Now.ToShortDateString().Replace("/", "")+"/";
                if (!Directory.Exists(maPpath + fileName))//判断文件夹是否存在,若不存在则创建
                {
                    Directory.CreateDirectory(maPpath + fileName);
                }
                var filePath = Path.Combine(maPpath + fileName , FilePath.FileName );
                FilePath.SaveAs(filePath);

                return fileName + FilePath.FileName;
            }
            catch (Exception ex)
            {
               // ViewBag.ErrorMessage = "文件上传失败，请重试";
                return null;                
            }
        }

    }

}