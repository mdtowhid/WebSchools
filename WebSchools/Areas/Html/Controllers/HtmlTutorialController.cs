using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using WebSchools.Models;
using WebSchools.Models.Html;

namespace WebSchools.Areas.Html.Controllers
{
    public class HtmlTutorialController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();

        public ActionResult Index(string id = "html_home")
        {
            var htmlIndex = db.HtmlIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.HtmlIndex = htmlIndex;

            var nextPage = db.HtmlIndices.FirstOrDefault(x => x.Sort == htmlIndex.Sort + 1);
            if (nextPage == null)
            {
                ViewBag.NextPage = htmlIndex;
            }
            else
            {
                ViewBag.NextPage = nextPage;
            }

            var prevPage = db.HtmlIndices.FirstOrDefault(x => x.Sort == htmlIndex.Sort - 1);
            if (prevPage == null)
            {
                ViewBag.PrevPage = htmlIndex;
            }
            else
            {
                ViewBag.PrevPage = prevPage;
            }
            ViewBag.HtmlIndexes = db.HtmlIndices.OrderBy(x=>x.Sort).ToList();

             var htmlTutorials=
                db.HtmlTutorials.Where(x => x.Slug == id).ToList();

            if (!htmlTutorials.Any())
            {
                ViewBag.NoneHtmlTutorials = "This Tutorial not yet covered";
                return View();
            }
            else
            {
                
                ViewBag.HtmlTutorials = htmlTutorials;
                return View();
            }
        }
        

        public JsonResult GetHtmlIndices()
        {
            var result = db.HtmlIndices.ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetHtmlIndicesByEnglishQuery(string id)
        {
            var result = db.HtmlIndices.Where(x=>x.Slug.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetHtmlIndicesByBanglaQuery(string id)
        {
            var result = db.HtmlIndices.Where(x => x.Heading.Contains(id)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult TryIt(int id)
        {
            var result = db.HtmlTutorials.FirstOrDefault(x => x.Id == id);
            return View(result);
        }
    }
}