using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.Css;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminCssController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        public bool GetSignInStatus()
        {
            if (Session["CssAdminName"] != null && Session["CssAdminId"] != null && Session["CssAdminEmail"] != null)
            {
                string adminName = (string)Session["CssAdminName"];
                int adminUniqueId = (int)Session["CssAdminId"];
                string adminEmail = (string)Session["CssAdminEmail"];
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (GetSignInStatus())
            {
                id = (int)Session["CssAdminId"];
                return View(db.CssTutorials.Where(x => x.AdminId == id).OrderBy(x => x.Slug).ToList());
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        //public ActionResult Index(int id)
        //{
        //    if (GetSignInStatus())
        //    {
        //        id = (int)Session["CssAdminId"];
        //        return View(db.CssTutorials.Where(x => x.AdminId == id).OrderBy(x => x.Slug).ToList());
        //    }
        //    else
        //    {
        //        return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
        //    }
        //}

        public JsonResult GetCssTutorialsByPageSlug(string slug)
        {
            var cssTutorials = db.CssTutorials.Where(x => x.Slug == slug);
            return new JsonResult
            {
                Data = cssTutorials,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        
        public ActionResult CreateCssTutorial()
        {
            if (GetSignInStatus())
            {
                ViewBag.CssIndices = db.CssIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCssTutorial(CssTutorial cssTutorial)
        {
            if (GetSignInStatus())
            {
                cssTutorial.ActiveStatus = false;
                ViewBag.CssIndices = db.CssIndices.ToList();
                if (ModelState.IsValid)
                {
                    if (cssTutorial.Slug == "none")
                    {
                        ViewBag.Error = "Please select slug";
                        return View();
                    }
                    cssTutorial.AdminId = (int)Session["CssAdminId"];
                    cssTutorial.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    db.CssTutorials.Add(cssTutorial);
                    db.SaveChanges();
                    ViewBag.Success = "New css tutorials has been added successfully!";
                    ModelState.Clear();
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(cssTutorial);
        }

        [HttpGet]
        //[ValidateInput(false)]
        public ActionResult EditCssTutorial(int? id)
        {
            if (GetSignInStatus())
            {
                ViewBag.CssIndices = db.CssIndices.ToList();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                CssTutorial cssTutorial = db.CssTutorials.Find(id);
                if (cssTutorial == null)
                {
                    return HttpNotFound();
                }
                return View(cssTutorial);
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditCssTutorial(CssTutorial cssTutorial)
        {

            if (GetSignInStatus())
            {
                cssTutorial.ActiveStatus = false;
                ViewBag.CssIndices = db.CssIndices.ToList();
                if (cssTutorial.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View(cssTutorial);
                }
                if (ModelState.IsValid)
                {
                    cssTutorial.AdminId = (int)Session["CssAdminId"];
                    cssTutorial.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                    db.Entry(cssTutorial).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Success = "This tutorials edited succesfully!";
                    ModelState.Clear();
                    return View(cssTutorial);
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            return View(cssTutorial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
