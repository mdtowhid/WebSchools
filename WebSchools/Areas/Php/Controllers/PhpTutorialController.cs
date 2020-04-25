using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.Php.Controllers
{
    public class PhpTutorialController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public ActionResult Php(string id="php_default")
        {
            var phpIndex = db.PhpIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.PhpIndices = db.PhpIndices.ToList();
            ViewBag.PhpSinglePage = phpIndex;

            var phpTutorials = db.PhpTutorials.Where(x => x.Slug == id || x.Slug == phpIndex.Slug).ToList();
            ViewBag.PhpTutorials = phpTutorials;

            var nextPage = db.PhpIndices.FirstOrDefault(x => x.Sort == phpIndex.Sort + 1);
            if (nextPage == null)
            {
                ViewBag.NextPage = phpIndex;
            }
            else
            {
                ViewBag.NextPage = nextPage;
            }


            var prevPage = db.PhpIndices.FirstOrDefault(x => x.Sort == phpIndex.Sort - 1);
            if (prevPage == null)
            {
                ViewBag.PrevPage = phpIndex;
            }
            else
            {
                ViewBag.PrevPage = prevPage;
            }

            if (!phpTutorials.Any())
            {
                ViewBag.NonePhpTutorials = "This Tutorial not yet covered";
                return View();
            }
            else
            {
                return View();
            }
        }



        public JsonResult GetPhpIndicesByEnglishQuery(string id)
        {
            var result = db.PhpIndices.Where(x => x.Slug.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetJsIndicesByBanglaQuery(string id)
        {
            var result = db.PhpIndices.Where(x => x.Heading.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }







    }
}