using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.Php;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminPhpController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();

        public bool GetSignInStatus()
        {
            if (Session["PhpAdminName"] != null && Session["PhpAdminId"] != null && Session["PhpAdminEmail"] != null)
            {
                string adminName = (string)Session["PhpAdminName"];
                int adminUniqueId = (int)Session["PhpAdminId"];
                string adminEmail = (string)Session["PhpAdminEmail"];
                return true;
            }

            return false;
        }

        public ActionResult Index(int? id)
        {
            if (GetSignInStatus())
            {
                id = (int)Session["PhpAdminId"];
                return View(db.PhpTutorials.Where(x=>x.AdminId == id).OrderBy(x=>x.Slug).ToList());
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult CreateTutorial()
        {
            if (GetSignInStatus())
            {
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTutorial(PhpTutorial pt)
        {
            if (GetSignInStatus())
            {
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                pt.ActiveStatus = false;
                if (pt.Slug == "none")
                {
                    ViewBag.Error = "Please select Slug from dropdown menu";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    pt.AdminId = (int)Session["PhpAdminId"];
                    pt.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

                    db.PhpTutorials.Add(pt);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "New PHP tutotrils is added successfully";
                    return View();
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditTutorial(int id)
        {
            if (GetSignInStatus())
            {
                ViewBag.CurrentPhpTPage = db.PhpTutorials.Find(id);
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                return View(db.PhpTutorials.Find(id));
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTutorial(PhpTutorial pt)
        {
            if (GetSignInStatus())
            {

                ViewBag.CurrentPhpTPage = db.PhpTutorials.Find(pt.Id);
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                var phpTutorial = db.PhpTutorials.Find(pt.Id);
                pt.ActiveStatus = false;
                if (pt.Slug == "none")
                {
                    ViewBag.Error = "Please select Slug from dropdown menu";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    phpTutorial.ActiveStatus = false;

                    phpTutorial.AdminId = (int)Session["PhpAdminId"];
                    phpTutorial.Slug = pt.Slug;
                    phpTutorial.HeadingDescription = pt.HeadingDescription;
                    phpTutorial.SubHeading = pt.SubHeading;
                    phpTutorial.Example = pt.Example;
                    db.PhpTutorials.AddOrUpdate(phpTutorial);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "PHP tutorial is updated successfully";
                    return View();
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        public JsonResult GetPhpTutorialsBySlug(string id)
        {
            var tutorials = db.PhpTutorials.Where(x => x.Slug == id).ToList();
            return new JsonResult
            {
                Data = tutorials,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        

    }
}