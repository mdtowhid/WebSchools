using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.Jquery;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminJqController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public bool GetSignInStatus()
        {
            if (Session["JqAdminName"] != null && Session["JqAdminId"] != null && Session["JqAdminEmail"] != null)
            {
                string adminName = (string)Session["JqAdminName"];
                int adminUniqueId = (int)Session["JqAdminId"];
                string adminEmail = (string)Session["JqAdminEmail"];
                return true;
            }

            return false;
        }
        public async Task<ActionResult> Index()
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryIndices = await db.JqueryIndices.ToListAsync();
                ViewBag.JqueryTutorials = await db.JqueryTutorials.ToListAsync();
                ViewBag.JqueryExamples = await db.JqueryExamples.ToListAsync();
                return View();
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
        }

        public JsonResult GetJqTutorialsByPageSlug(string id)
        {
            var result = db.JqueryTutorials.Where(x => x.Slug == id).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJqExamplesByPageSlug(string id)
        {
            var result = db.JqueryExamples.Where(x => x.Slug == id).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        [HttpGet]
        public async Task<ActionResult> CreateJqTutorial()
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryIndices = await db.JqueryIndices.ToListAsync();
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
        public async Task<ActionResult> CreateJqTutorial(JqueryTutorial jqt)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryIndices = await db.JqueryIndices.ToListAsync();

                jqt.ActiveStatus = false;

                if (jqt.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>slug</b>";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        jqt.Slug = jqt.Slug.Replace(" ", "_");
                        jqt.AdminId = (int)Session["JqAdminId"];
                        jqt.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                        db.JqueryTutorials.Add(jqt);
                        await db.SaveChangesAsync();
                        ViewBag.Success = "New Jquery tutorial added successfully";
                        ModelState.Clear();
                        return View();
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
        public ActionResult EditJqTutorial(int id)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryTutorial = db.JqueryTutorials.Find(id);
                ViewBag.JqueryIndices = db.JqueryIndices.ToList();
                var c = db.JqueryTutorials.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJqTutorial(JqueryTutorial jqt)
        {

            if (GetSignInStatus())
            {
                ViewBag.JqueryTutorial = db.JqueryTutorials.Find(jqt.Id);
                ViewBag.JqueryIndices = db.JqueryIndices.ToList();
                var c = db.JqueryTutorials.FirstOrDefault(x => x.Id == jqt.Id);
                if (jqt.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View(jqt);
                }
                else if (c != null)
                {
                    if (ModelState.IsValid)
                    {
                        c.ActiveStatus = false;

                        c.Slug = jqt.Slug.Replace(" ", "_");
                        c.AdminId = (int)Session["JqAdminId"];
                        c.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                        c.AwesomeClass = "awesomeClass";
                        c.CodeSample = jqt.CodeSample;

                        c.JqueryIndexId = jqt.JqueryIndexId;
                        c.Slug = jqt.Slug;
                        c.Sort = 123;
                        c.SubHeading = jqt.SubHeading;
                        c.SubExplanation = jqt.SubExplanation;

                        db.JqueryTutorials.AddOrUpdate(c);
                        db.SaveChanges();
                        return View(c);
                    }

                    return View(jqt);
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(jqt);

        }
        

        [HttpGet]
        public async Task<ActionResult> CreateJqExample()
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryIndices = await db.JqueryIndices.ToListAsync();
                //ViewBag.jQueryTutorials = await db.JqueryTutorials.ToListAsync();
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
        public async Task<ActionResult> CreateJqExample(JqueryExample jqe)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqueryIndices = await db.JqueryIndices.ToListAsync();
                jqe.ActiveStatus = false;

                if (jqe.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>slug</b>";
                    return View();
                }
                else if (jqe.JqTutorialId == -1)
                {
                    ViewBag.Error = "Please select <b>Example on.</b>";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        jqe.AdminId = (int)Session["JqAdminId"];
                        jqe.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                        db.JqueryExamples.Add(jqe);
                        await db.SaveChangesAsync();
                        ViewBag.Success = "New Jquery Example is added successfully";
                        ModelState.Clear();
                        return View();
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
        public ActionResult EditJqExample(int id)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqExample = db.JqueryExamples.Find(id);
                ViewBag.JqueryIndices = db.JqueryIndices.ToList();
                ViewBag.jQueryTutorials = db.JqueryTutorials.ToList();
                

                var c = db.JqueryExamples.Find(id);
                if (c != null)
                {
                    return View(c);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJqExample(JqueryExample jqe)
        {

            if (GetSignInStatus())
            {
                ViewBag.JqExample = db.JqueryExamples.Find(jqe.Id);
                ViewBag.jQueryIndices = db.JqueryIndices.ToList();
                ViewBag.jQueryTutorials = db.JqueryTutorials.ToList();
                

                var c = db.JqueryExamples.FirstOrDefault(x => x.Id == jqe.Id);
                if (c != null)
                {
                    if (jqe.Slug == "none")
                    {
                        ViewBag.Error = "Please select <b>slug</b>";
                        return View(jqe);
                    }
                    else if (jqe.JqTutorialId == -1)
                    {
                        ViewBag.Error = "Please select <b>Example on.</b>";
                        return View(jqe);
                    }
                    else if (ModelState.IsValid)
                    {
                        c.ExampleHeading = jqe.ExampleHeading;
                        c.Example = jqe.Example;
                        c.Description = jqe.Description;
                        c.AdminId = (int)Session["JqAdminId"];
                        c.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
                        c.Slug = jqe.Slug;
                        c.JqueryIndexId = jqe.JqueryIndexId;
                        c.JqTutorialId = jqe.JqTutorialId;
                        c.Sort = jqe.Sort;
                        c.ActiveStatus = false;
                        db.JqueryExamples.AddOrUpdate(c);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                return RedirectToAction("SignInAdmin", "Admin", new { area = "Admin" });
            }
            
            return View(jqe);
        }

        public JsonResult GetJqTutorialsBySlugId(int id)
        {
            var result = db.JqueryTutorials.Where(x => x.JqueryIndexId == id).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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