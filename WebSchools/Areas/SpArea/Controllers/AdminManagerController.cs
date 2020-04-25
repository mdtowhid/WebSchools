using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.SpArea.Controllers
{
    public class AdminManagerController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public bool GetSignInStatus()
        {
            string adminName = (string)Session["FullName"];
            string adminEmail = (string)Session["Email"];
            string adminPassword = (string)Session["Password"];

            if (adminName != null && adminPassword != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // GET: SpArea/AdminManager
        public ActionResult Index()
        {
            if (GetSignInStatus())
            {
                return View(db.Admins.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult GetAdminCategories()
        {
            if (GetSignInStatus())
            {
                return View(db.AdminCategories.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult CreateAdminCategory()
        {
            if (GetSignInStatus())
            {
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        public ActionResult CreateAdminCategory(AdminCategory ac)
        {
            if (GetSignInStatus())
            {
                if (ModelState.IsValid)
                {
                    var exist = db.AdminCategories.FirstOrDefault(x => x.AdminType == ac.AdminType);
                    if (exist != null)
                    {
                        ViewBag.Error = "This admin type already exist.";
                        return View(ac);
                    }
                    else
                    {
                        db.AdminCategories.Add(ac);
                        db.SaveChanges();
                        ViewBag.Success = "New category added successfully";
                        ModelState.Clear();
                        return View(ac);
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }


        [HttpGet]
        public ActionResult EditAdminCategory(int id)
        {
            if (GetSignInStatus())
            {
                var c = db.AdminCategories.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpPost]
        public ActionResult EditAdminCategory(AdminCategory ac)
        {
            if (GetSignInStatus())
            {
                if (ModelState.IsValid)
                {
                    var adc = db.AdminCategories.Find(ac.Id);
                    if (adc != null)
                    {
                        adc.ActiveStatus = ac.ActiveStatus;
                        adc.AdminType = ac.AdminType;

                        db.AdminCategories.AddOrUpdate(adc);
                        db.SaveChanges();
                        ViewBag.Success = "Category is updated successfully";
                        return View();
                    }
                    return View();
                }
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }


        [HttpGet]
        public ActionResult GetAdmins()
        {
            if (GetSignInStatus())
            {
                return View(db.Admins.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            if (GetSignInStatus())
            {
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Models.Admin a)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(a);
                db.SaveChanges();
                ViewBag.Success = "New user added successfully";
                return View();
            }
            else
            {
                ViewBag.Error = "There is an error.";
                return View(ViewBag.Error);
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.Admins.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Models.Admin a)
        {
            if (GetSignInStatus())
            {
                var ad = db.Admins.Find(a.Id);
                if (ModelState.IsValid && ad != null)
                {
                    ad.AdminStatus = a.AdminStatus;
                    ad.Name = a.Name;
                    ad.AdminType = a.AdminType;
                    ad.Password = a.Password;
                    ad.ConfirmPassword = a.ConfirmPassword;
                    ad.Email = a.Email;

                    ViewBag.Success = "User informations is updated successfully.";
                    db.Admins.AddOrUpdate(ad);
                    db.SaveChanges();
                    return View(ad);
                }
                else
                {
                    return View(a);
                }
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

    }
}