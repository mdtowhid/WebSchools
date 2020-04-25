using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.JavaScript.Controllers
{
    public class JavaScriptTutorialController : Controller
    {
        
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Js(string id = "js_default")
        {
            var check = db.JavaScriptIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.JsIndices = db.JavaScriptIndices.ToList();
            ViewBag.JsSinglePage = check;

            var jsExamples = db.JavaScriptExamples.Where(x => x.Slug == check.Slug).ToList();
            ViewBag.JsExamples = jsExamples;

            ViewBag.JsTutorials = db.JavaScriptTutorials.Where(x => x.Slug == check.Slug).ToList();

            var nextPage = db.JavaScriptIndices.FirstOrDefault(x => x.Sort == check.Sort + 1);
            if (nextPage == null)
            {
                ViewBag.NextPage = check;
            }
            else
            {
                ViewBag.NextPage = nextPage;
            }

            var prevPage = db.JavaScriptIndices.FirstOrDefault(x => x.Sort == check.Sort - 1);
            if (prevPage == null)
            {
                ViewBag.PrevPage = check;
            }
            else
            {
                ViewBag.PrevPage = prevPage;
            }


            var javaScriptTutoials = db.JavaScriptTutorials.Where(x => x.Slug == check.Slug).ToList();

            if (!javaScriptTutoials.Any() && !jsExamples.Any())
            {
                ViewBag.NoneJsTutorials = "This Tutorial not yet covered";
                return View();
            }
            else
            {
                return View();
            }
        }



        public JsonResult GetJsIndicesByEnglishQuery(string id)
        {
            var result = db.JavaScriptIndices.Where(x => x.Slug.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJsIndicesByBanglaQuery(string id)
        {
            var result = db.JavaScriptIndices.Where(x => x.Heading.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult TryIt(int id)
        {
            var result = db.JavaScriptExamples.FirstOrDefault(x => x.Id == id);
            return View(result);

        }
        
	} 
}