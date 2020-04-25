using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.BusinessLogic;
using WebSchools.Models;
using WebSchools.Models.JavaScript;
using WebSchools.Models.Sql;

namespace WebSchools.Areas.Sql.Controllers
{
    public class SqlTutorialController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        public ActionResult Sql(string id= "sql_default")
        {
            ViewBag.SqlIndices = db.SqlIndices.ToList();
            ViewBag.DemoCustomers = db.DemoCustomers.ToList();
            var sqlSinglePage = db.SqlIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.SqlSinglePage = sqlSinglePage;

            var nextPage = db.SqlIndices.FirstOrDefault(x => x.Sort == sqlSinglePage.Sort + 1);
            if (nextPage == null)
            {
                ViewBag.NextPage = sqlSinglePage;
            }
            else
            {
                ViewBag.NextPage = nextPage;
            }

            var prevPage = db.SqlIndices.FirstOrDefault(x => x.Sort == sqlSinglePage.Sort - 1);
            if (prevPage == null)
            {
                ViewBag.PrevPage = sqlSinglePage;
            }
            else
            {
                ViewBag.PrevPage = prevPage;
            }


            if (sqlSinglePage == null)
            {
                return RedirectToAction("PageNotFound");
            }
            else
            {
                var sqlSyntaxBoxes = db.SqlSyntaxBoxes.Where(x => x.Slug == sqlSinglePage.Slug).ToList();
                var sqlExamples = db.SqlExamples.Where(x => x.Slug == sqlSinglePage.Slug).ToList();
                
                ViewBag.SqlSyntaxBoxes = sqlSyntaxBoxes;
                ViewBag.SqlExamples = sqlExamples;

                if (!sqlExamples.Any() && !sqlSyntaxBoxes.Any())
                {
                    ViewBag.NoneSqlTutorials = "This tutorial not yet covered.";
                    return View();
                }
            }
            
            return View();
        }

        public JsonResult GetSqlIndices()
        {
            var result = db.SqlIndices.ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetSqlIndicesByEnglishQuery(string queryString)
        {
            var result = db.SqlIndices.Where(x => x.Slug.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetSqlIndicesByBanglaQuery(string queryString)
        {
            var result = db.SqlIndices.Where(x => x.Heading.Contains(queryString)).ToList();
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult PageNotFound(string id= "sql_default")
        {
            ViewBag.SqlIndices = db.SqlIndices.ToList();
            ViewBag.DemoCustomers = db.DemoCustomers.ToList();
            var sqlSinglePage = db.SqlIndices.FirstOrDefault(x => x.Slug == id);
            ViewBag.SqlSinglePage = sqlSinglePage;
            return View();
        }
        [HttpGet]
        public ActionResult TryIt(int id)
        {
            var result = db.SqlExamples.Find(id);
            return View(result);
        }

        public JsonResult GetCustomersDataByUserQuery(string userQuery)
        {
            var validQuery = db.SqlSyntaxBoxes.FirstOrDefault(x => x.SqlSyntax == userQuery);

            SqlTryItResult sqlTryItResult = new SqlTryItResult();
            var result = sqlTryItResult.GetCustomersDataByUserQuery(userQuery);
            if (result == null)
            {
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult {Data = result.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}