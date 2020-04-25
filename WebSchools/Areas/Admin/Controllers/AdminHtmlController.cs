using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebSchools.Models;
using WebSchools.Models.Html;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminHtmlController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();
        
        public bool GetSignInStatus()
        {
            if (Session["HtmlAdminName"] != null && Session["HtmlAdminId"] != null && Session["HtmlAdminEmail"] != null)
            {
                string adminName = (string)Session["HtmlAdminName"];
                int adminUniqueId = (int)Session["HtmlAdminId"];
                string adminEmail = (string)Session["HtmlAdminEmail"];
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (GetSignInStatus())
            {
                id = (int) Session["HtmlAdminId"];
                return View(db.HtmlTutorials.Where(x => x.AdminId == id).OrderBy(x => x.Slug).ToList());
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpGet]
        public ActionResult CeateHtmlTutorial()
        {
            if (GetSignInStatus())
            {
                ViewBag.HtmlIndices = db.HtmlIndices.OrderBy(x => x.Slug).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CeateHtmlTutorial(HtmlTutorial htmlTutorial)
        {
            ViewBag.HtmlIndices = db.HtmlIndices.OrderBy(x => x.Slug).ToList();
            htmlTutorial.ActiveStatus = false;

            if (GetSignInStatus())
            {
                if (htmlTutorial.Slug == "none")
                {
                    ViewBag.Error = "please select <b>Slug</b>";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    htmlTutorial.AdminId = (int)Session["HtmlAdminId"];
                    htmlTutorial.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    db.HtmlTutorials.Add(htmlTutorial);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "Html tutorial added successfully!";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }

           
            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditHtmlTutorial(int id)
        {
            var htmltutorial = db.HtmlTutorials.FirstOrDefault(x => x.Id == id);
            if (GetSignInStatus())
            {
                ViewBag.HtmlIndices = db.HtmlIndices.OrderBy(x => x.Slug).ToList();
                return View(htmltutorial);
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult EditHtmlTutorial(HtmlTutorial ht)
        {
            var htmltutorial = db.HtmlTutorials.Where(x => x.Id == ht.Id);
            ViewBag.HtmlIndices = db.HtmlIndices.OrderBy(x => x.Slug).ToList();
            var check = db.HtmlTutorials.FirstOrDefault(x => x.Id == ht.Id);

            if (GetSignInStatus())
            {
                if (ht.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>slug</b>";
                    return View(htmltutorial);
                }


                if (check != null && ModelState.IsValid)
                {
                    check.AwesomeClass = "awesomeClass";
                    check.ActiveStatus = false;


                    ht.AdminId = (int)Session["HtmlAdminId"];

                    check.Description = ht.Description;
                    check.Example = ht.Example;
                    check.Explanation = ht.Explanation;

                    check.Heading = ht.Heading;
                    check.Slug = ht.Slug;
                    check.Sort = ht.Sort;

                    db.HtmlTutorials.AddOrUpdate(check);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Default", "AdminHtml", new { area = "Admin" });

                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }

            
            return View(htmltutorial);
        }


        [HttpPost]
        public void ReorderHtmlIndices(int[] id)
        {
            int count = 1;

            foreach (var pageId in id)
            {
                var sortHtmlIndex = db.HtmlIndices.Find(pageId);
                if (sortHtmlIndex != null) sortHtmlIndex.Sort = count;

                db.SaveChanges();
                count++;
            }

        }
        //[HttpPost]
        //public void ReorderPages(int[] id)
        //{
        //    int count = 1;

        //    foreach (var pageId in id)
        //    {
        //        var sortHtmlIndex = db.HtmlExamples.Find(pageId);
        //        if (sortHtmlIndex != null) sortHtmlIndex.Sort = count;

        //        db.SaveChanges();
        //        count++;
        //    }

        //}
    }
}