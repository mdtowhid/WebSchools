using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.Jquery.Controllers
{
    public class JqueryTutorialController : Controller
    {
        public WebSchoolsDbContext db = new WebSchoolsDbContext();
        

        public ActionResult Jq(string id = "jq_default")
        {
            var check = db.JqueryIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.SinglePage = check;
            ViewBag.JqIndices = db.JqueryIndices.ToList();

            ViewBag.JqExamples = db.JqueryExamples.Where(x => x.Slug == check.Slug).ToList();
            ViewBag.JqTutorials = db.JqueryTutorials.Where(x => x.Slug == check.Slug).ToList();
            var noJqueryTutorial = db.JqueryTutorials.Where(x => x.Slug == check.Slug).ToList();

            var nextPage = db.JqueryIndices.FirstOrDefault(x => x.Sort == check.Sort + 1);
            if (nextPage == null)
            {
                ViewBag.NextPage = check;
            }
            else
            {
                ViewBag.NextPage = nextPage;
            }

            var prevPage = db.JqueryIndices.FirstOrDefault(x => x.Sort == check.Sort - 1);
            if (prevPage == null)
            {
                ViewBag.PrevPage = check;
            }
            else
            {
                ViewBag.PrevPage = prevPage;
            }


            if (!noJqueryTutorial.Any())
            {
                ViewBag.NoJqueryTutorial = "This tutorial not yet coverd";
                return View();
            }
            else
            {
                return View();
            }
        }

        public JsonResult GetJsIndices()
        {
            var result = db.JqueryIndices.ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJqIndicesByBanglaQuery(string queryString)
        {
            var result = db.JqueryIndices.Where(x => x.Slug.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJqIndicesByEnglishQuery(string queryString)
        {
            var result = db.JqueryIndices.Where(x => x.Heading.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult TryIt(int id)
        {
            var result = db.JqueryExamples.FirstOrDefault(x => x.Id == id);
            return View(result);
        }
	}
}