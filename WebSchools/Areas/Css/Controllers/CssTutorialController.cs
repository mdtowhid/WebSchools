using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.Css.Controllers
{
    public class CssTutorialController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        
        public ActionResult Css(string id = "css_default")
        {
            var cssIndex = db.CssIndices.FirstOrDefault(x => x.Slug == id);

            //var nextPage = db.CssIndices.FirstOrDefault(x => x.Sort == cssIndex.Sort + 1);
            //if (nextPage == null)
            //{
            //    ViewBag.NextPage = cssIndex;
            //}
            //else
            //{
            //    ViewBag.NextPage = nextPage;
            //}

            //var prevPage = db.CssIndices.FirstOrDefault(x => x.Sort == cssIndex.Sort - 1);
            //if (prevPage == null)
            //{
            //    ViewBag.PrevPage = cssIndex;
            //}
            //else
            //{
            //    ViewBag.PrevPage = prevPage;
            //}

            ViewBag.NextPage = null;
            ViewBag.PrevPage = null;
            ViewBag.CssIndex = cssIndex;
            ViewBag.CssIndexes = db.CssIndices.ToList();

            var result = db.CssTutorials.Where(x => x.Slug == id && x.ActiveStatus == true).ToList();
            if (!result.Any())
            {
                ViewBag.NoArticles = "This tutorial is not covered yet";
                return View();
            }

            ViewBag.CssTutorials = result;
            return View();
        }




        public JsonResult GetCssIndices()
        {
            var result = db.CssIndices.ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCssIndicesByEnglishQuery(string queryString)
        {
            var result = db.CssIndices.Where(x => x.Heading.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCssIndicesByBanglaQuery(string queryString)
        {
            var result = db.CssIndices.Where(x => x.Heading.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public ActionResult TryIt(int id)
        {
            var result = db.CssTutorials.FirstOrDefault(x=>x.Id == id);
            return View(result);
        }
	}
}