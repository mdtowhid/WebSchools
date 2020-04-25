using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.JavaScript;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminJsController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        public bool GetSignInStatus()
        {
            if (Session["JsAdminName"] != null && Session["JsAdminId"] != null && Session["JsAdminEmail"] != null)
            {
                string adminName = (string)Session["JsAdminName"];
                string adminEmail = (string)Session["JsAdminEmail"];
                int adminUniqueId = (int)Session["JsAdminId"];
                return true;
            }

            return false;
        }

        public ActionResult Index()
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                ViewBag.JsTutorials = db.JavaScriptTutorials.ToList();
                ViewBag.JsExamples = db.JavaScriptExamples.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        public JsonResult GetJsTutorialsByPageSlug(string slug)
        {
            var result = db.JavaScriptTutorials.Where(x => x.Slug == slug).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJsExampleByPageSlug(string slug)
        {
            var result = db.JavaScriptExamples.Where(x => x.Slug == slug).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        

        [HttpGet]
        public ActionResult CreateJsTutorial()
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
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
        public ActionResult CreateJsTutorial(JavaScriptTutorial jt)
        {
            if (GetSignInStatus())
            {
                jt.ActiveStatus = false;
                jt.AwesomeClass = "awesomeClass";
                jt.AdminId = (int)Session["JsAdminId"];
                jt.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                //jt.Slug = jt.Slug.Replace(" ", "_");

                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                if (jt.Slug == "none")
                {
                    ViewBag.Error = "<b>Please select slug</b>";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    db.JavaScriptTutorials.Add(jt);
                    db.SaveChanges();
                    ViewBag.Success = "New javascript tutorial has been created";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View();
        }

        public ActionResult EditJsTutorial(string id)
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                var jt = db.JavaScriptTutorials.FirstOrDefault(x => x.Slug == id);
                if (jt != null)
                {
                    return View(jt);
                }

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
        public ActionResult EditJsTutorial(JavaScriptTutorial jt)
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                var jsTutorial = db.JavaScriptTutorials.Find(jt.Id);
                if (jt.Slug == "none")
                {
                    ViewBag.Error = "<b>Please select slug</b>";
                    return View(jt);
                }
                else if (jsTutorial != null)
                {
                    if (ModelState.IsValid)
                    {
                        jsTutorial.ActiveStatus = false;
                        jsTutorial.AwesomeClass = "awesomeClass";
                        jt.AdminId = (int)Session["JsAdminId"];

                        jsTutorial.Explanation = jt.Explanation;
                        //jsTutorial.Slug = jt.Slug.Replace(" ", "_");
                        jsTutorial.Sort = jt.Sort;
                        jsTutorial.Heading = jt.Heading;

                        db.SaveChanges();
                        return View(jsTutorial);
                    }
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(jt);
        }

        [HttpGet]
        public ActionResult CreateJsExample()
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
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
        public ActionResult CreateJsExample(JavaScriptExample je)
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                je.ActiveStatus = false;
                je.AdminId = (int)Session["JsAdminId"];
                je.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");


                var c = db.JavaScriptExamples.FirstOrDefault(x => x.Slug == je.Slug);
                if (je.Slug == "none")
                {
                    ViewBag.Error = "Please select Slug.";
                    return View(je);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.JavaScriptExamples.Add(je);
                        db.SaveChanges();
                        ViewBag.Success = "New Javascript example saved successfully!";
                        return View(je);
                    }
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult EditJsExample(int id)
        {
            ViewBag.JsIndices = db.JavaScriptIndices.ToList();
            var jsExample = db.JavaScriptExamples.FirstOrDefault(x => x.Id == id);
            if (jsExample != null)
            {
                return View(jsExample);
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJsExample(JavaScriptExample je)
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                if (je.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>Slug</b>";
                    return View(je);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var c = db.JavaScriptExamples.FirstOrDefault(x => x.Id == je.Id);

                        c.Description = je.Description;
                        c.Example = je.Example;
                        c.SubHeading = je.SubHeading;
                        c.SubDescription = je.SubDescription;
                        c.Slug = je.Slug;
                        c.ActiveStatus = false;
                        c.AdminId = (int)Session["JsAdminId"];
                        c.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

                        db.JavaScriptExamples.AddOrUpdate(c);
                        db.SaveChanges();
                        ViewBag.Success = "Javascipt example is updated successfully";
                        return View(c);
                    }
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(je);
        }
    }
}