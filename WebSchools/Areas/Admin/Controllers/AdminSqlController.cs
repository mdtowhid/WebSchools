using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.JavaScript;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminSqlController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public bool GetSignInStatus()
        {
            if (Session["SqlAdminName"] != null && Session["SqlAdminId"] != null && Session["SqlAdminEmail"] != null)
            {
                string adminName = (string)Session["SqlAdminName"];
                string adminEmail = (string)Session["SqlAdminEmail"];
                int adminUniqueId = (int)Session["SqlAdminId"];
                return true;
            }

            return false;
        }

        public ActionResult Index()
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                ViewBag.SqlExamples = db.SqlExamples.ToList();
                ViewBag.SqlSyntax = db.SqlSyntaxBoxes.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        public JsonResult GetSqlExampleByPageId(int id)
        {

            var result = db.SqlExamples.Where(x => x.SqlIndexId == id).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetSqlSyntaxesByPageId(int id)
        {
            var result = db.SqlSyntaxBoxes.Where(x => x.SqlIndexId == id).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        
        
        [HttpGet]
        public ActionResult CreateSqlSyntax()
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                ViewBag.SqlExamples = db.SqlExamples.ToList();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateSqlSyntax(SqlSyntaxBox ssb)
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                ViewBag.SqlExamples = db.SqlExamples.ToList();
                ssb.ActiveStatus = false;
                if (ssb.Slug == "none")
                {
                    ViewBag.Error = "Please select slug from drop-down.";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    ssb.AdminId = (int)Session["SqlAdminId"];
                    ssb.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    ssb.SqlSyntax = ssb.SqlSyntax;
                    db.SqlSyntaxBoxes.Add(ssb);
                    db.SaveChanges();
                    ViewBag.Success = "SQL syntax is added susseccfully.";
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
        public ActionResult EditSqlSyntax(int id)
        {
            if (GetSignInStatus())
            {
                ViewBag.SSB = db.SqlSyntaxBoxes.Find(id);
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                var c = db.SqlSyntaxBoxes.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSqlSyntax(SqlSyntaxBox ssb)
        {
            if (GetSignInStatus())
            {
                ViewBag.SSB = db.SqlSyntaxBoxes.Find(ssb.Id);

                ViewBag.SqlIdices = db.SqlIndices.ToList();
                var c = db.SqlSyntaxBoxes.Find(ssb.Id);
                if (ssb.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View();
                }
                else if (c != null && ModelState.IsValid)
                {
                    c.Slug = ssb.Slug;
                    c.SqlSyntax = ssb.SqlSyntax;
                    c.SqlSyntaxDescription = ssb.SqlSyntaxDescription;
                    c.SubHeading = ssb.SubHeading;
                    c.SqlIndexId = ssb.SqlIndexId;
                    c.ActiveStatus = false;
                    c.AdminId = (int)Session["SqlAdminId"];


                    db.SqlSyntaxBoxes.AddOrUpdate(c);
                    db.SaveChanges();
                    ViewBag.Success = "SqlSyntax is updated successfully";
                    return View(c);
                }
                return View(ssb);
            }
            return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            
        }

        [HttpGet]
        public ActionResult CreateSqlExample()
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateSqlExample(SqlExample se)
        {
            if (GetSignInStatus())
            {
                se.ActiveStatus = false;
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                if (se.Slug == "none")
                {
                    ViewBag.Error = "Please select slug from drop-down.";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    se.AdminId = (int)Session["SqlAdminId"];
                    se.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    db.SqlExamples.Add(se);
                    db.SaveChanges();
                    ViewBag.Success = "New SQL example is added susseccfully.";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }

            return View(se);
        }



        [HttpGet]
        public ActionResult EditSqlExample(int id)
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();
                ViewBag.SqlExample = db.SqlExamples.Find(id);

                var c = db.SqlExamples.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSqlExample(SqlExample se)
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIdices = db.SqlIndices.ToList();

                ViewBag.SqlExample = db.SqlExamples.Find(se.Id);

                var c = db.SqlExamples.Find(se.Id);
                if (se.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View();
                }
                else if (c != null && ModelState.IsValid)
                {
                    c.Slug = se.Slug;
                    c.ExampleBox = se.ExampleBox;
                    c.ExampleHeading = se.ExampleHeading;
                    c.ExampleHeadingDescription = se.ExampleHeadingDescription;
                    c.SqlIndexId = se.SqlIndexId;
                    c.ActiveStatus = false;
                    db.SqlExamples.AddOrUpdate(c);
                    db.SaveChanges();
                    return View(c);
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(se);
        }
        
    }
}