using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.Css;
using WebSchools.Models.Html;
using WebSchools.Models.BlogVm;
using WebSchools.Models.HowTo;
using WebSchools.Models.JavaScript;
using WebSchools.Models.Jquery;
using WebSchools.Models.Sql;
using WebSchools.Models.Php;

namespace WebSchools.Areas.SpArea.Controllers
{
    public class TutorialManagersController : Controller
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

        public ActionResult Index()
        {
            if (GetSignInStatus())
            {
                GetAllViewBag();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditHtmlTutorial(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.HtmlTutorials.FirstOrDefault(x => x.Id == id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult EditHtmlTutorial(HtmlTutorial ht)
        {
            if (GetSignInStatus())
            {
                var htmltutorial = db.HtmlTutorials.FirstOrDefault(x => x.Id == ht.Id);
                ViewBag.HtmlIndices = db.HtmlIndices.OrderBy(x => x.Slug).ToList();
                var check = db.HtmlTutorials.FirstOrDefault(x => x.Id == ht.Id);
                if (ht.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>slug</b>";
                    return View(htmltutorial);
                }


                if (check != null && ModelState.IsValid)
                {
                    //check.AdvertisementActive = ht.AdvertisementActive;
                    check.AwesomeClass = ht.AwesomeClass;
                    check.ActiveStatus = ht.ActiveStatus;


                    check.Description = ht.Description;
                    check.Example = ht.Example;
                    check.Explanation = ht.Explanation;

                    check.Heading = ht.Heading;
                    check.Slug = ht.Slug;
                    check.Sort = ht.Sort;

                    db.HtmlTutorials.AddOrUpdate(check);
                    db.SaveChanges();
                    ViewBag.Success = ht.Heading + " updated successfully";
                    return View(htmltutorial);

                }

                return View(ht);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditCssTutorial(int? id)
        {
            if (GetSignInStatus())
            {
                return View(db.CssTutorials.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditCssTutorial(CssTutorial cssTutorial)
        {
            if (GetSignInStatus())
            {
                if (cssTutorial.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View(cssTutorial);
                }

                if (ModelState.IsValid)
                {
                    db.Entry(cssTutorial).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Success = "This tutorials edited succesfully!";
                    ModelState.Clear();
                    return View(cssTutorial);
                }

                return View(cssTutorial);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult EditJsTutorial(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptTutorials.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJsTutorial(JavaScriptTutorial jt)
        {
            if (GetSignInStatus())
            {
                var jst = db.JavaScriptTutorials.Find(jt.Id);
                if (jt.Slug == "none")
                {
                    ViewBag.Error = "<b>Please select slug</b>";
                    return View(jt);
                }
                else if (jst != null)
                {
                    if (ModelState.IsValid)
                    {
                        jst.ActiveStatus = jt.ActiveStatus;
                        jst.AdminId = jt.AdminId;
                        jst.AwesomeClass = jt.AwesomeClass;
                        jst.Slug = jt.Slug;
                        jst.Sort = jt.Sort;
                        jst.Date = jst.Date;

                        jst.Heading = jt.Heading;
                        jst.Explanation = jt.Explanation;
                        jst.JavaScriptIndexId = jt.JavaScriptIndexId;

                        db.JavaScriptTutorials.AddOrUpdate(jst);
                        db.SaveChanges();
                        ModelState.Clear();
                        return View(jt);
                    }
                }
                return View(jt);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult EditJsExample(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptExamples.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJsExample(JavaScriptExample je)
        {
            if (GetSignInStatus())
            {
                if (je.Slug == "none")
                {
                    ViewBag.Error = "Please select <b>Slug</b>";
                    return View(je);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(je).State = EntityState.Modified;
                        //var c = db.JavaScriptExamples.Find(je.Id);

                        //c.Description = je.Description;
                        //c.Example = je.Example;
                        ////c.ExampleOutput = je.ExampleOutput;
                        //c.SubHeading = je.SubHeading;
                        //c.SubDescription = je.SubDescription;
                        //c.Slug = je.Slug;
                        //c.ActiveStatus = je.ActiveStatus;

                        //db.JavaScriptExamples.AddOrUpdate(c);
                        db.SaveChanges();
                        ViewBag.Success = "Javascipt example is updated successfully";
                        ModelState.Clear();
                        return View(je);
                    }
                }
                return View(je);
            }

            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditJqTutorial(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryTutorials.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJqTutorial(JqueryTutorial jqt)
        {
            if (GetSignInStatus())
            {
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
                        c.ActiveStatus = jqt.ActiveStatus;
                        c.AwesomeClass = c.AwesomeClass;
                        c.CodeSample = jqt.CodeSample;

                        c.JqueryIndexId = jqt.JqueryIndexId;
                        c.Slug = jqt.Slug;
                        c.Sort = 123;
                        c.SubHeading = jqt.SubHeading;
                        c.SubExplanation = jqt.SubExplanation;

                        db.JqueryTutorials.AddOrUpdate(c);
                        ViewBag.Success = "Jquery Tutorial edited successfully";
                        db.SaveChanges();
                        return View(c);
                    }

                    return View(jqt);
                }

                return View(jqt);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult EditJqExample(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryExamples.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJqExample(JqueryExample jqe)
        {
            if (GetSignInStatus())
            {
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
                        //c.ExampleOutput = jqe.ExampleOutput;
                        c.Slug = jqe.Slug;
                        c.JqueryIndexId = jqe.JqueryIndexId;
                        c.JqTutorialId = jqe.JqTutorialId;
                        c.Sort = jqe.Sort;
                        c.ActiveStatus = jqe.ActiveStatus;
                        db.JqueryExamples.AddOrUpdate(c);
                        db.SaveChanges();
                        ViewBag.Success = "Jquery example edited successfully";
                        return View(c);
                    }
                }
                return View(jqe);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        [HttpGet]
        public ActionResult EditSqlSyntax(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.SqlSyntaxBoxes.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSqlSyntax(SqlSyntaxBox ssb)
        {
            if (GetSignInStatus())
            {
                var c = db.SqlSyntaxBoxes.Find(ssb.Id);
                if (ssb.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View();
                }
                else if (c != null && ModelState.IsValid)
                {
                    c.Slug = ssb.Slug;
                    c.SqlSyntax = ssb.SqlSyntax;
                    c.SqlSyntaxDescription = ssb.SqlSyntaxDescription;
                    c.SubHeading = ssb.SubHeading;
                    c.SqlIndexId = ssb.SqlIndexId;
                    c.ActiveStatus = ssb.ActiveStatus;
                    db.SqlSyntaxBoxes.AddOrUpdate(c);
                    db.SaveChanges();
                    ViewBag.Success = "Sql Syntax is updated successfully";
                    return View(c);
                }
                return View(ssb);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        public ActionResult EditSqlExample(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.SqlExamples.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSqlExample(SqlExample se)
        {
            if (GetSignInStatus())
            {
                var c = db.SqlExamples.Find(se.Id);
                if (se.Slug == "none")
                {
                    ViewBag.Error = "Please select slug";
                    return View();
                }
                else if (c != null && ModelState.IsValid)
                {
                    c.Slug = se.Slug;
                    c.ExampleBox = se.ExampleBox;
                    c.ExampleHeading = se.ExampleHeading;
                    c.ExampleHeadingDescription = se.ExampleHeadingDescription;
                    c.SqlIndexId = se.SqlIndexId;
                    c.ActiveStatus = se.ActiveStatus;
                    db.SqlExamples.AddOrUpdate(c);
                    db.SaveChanges();
                    ViewBag.Success = "Sql example edited successfully";
                    return View(c);
                }
                return View(se);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditPhpTutorial(int id)
        {
            if (GetSignInStatus())
            {
                return View(db.PhpTutorials.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPhpTutorial(PhpTutorial pt)
        {
            if (GetSignInStatus())
            {
                var phpTutorial = db.PhpTutorials.Find(pt.Id);
                if (pt.Slug == "none")
                {
                    ViewBag.Error = "Please select Slug from dropdown menu";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    phpTutorial.Slug = pt.Slug;
                    phpTutorial.HeadingDescription = pt.HeadingDescription;
                    phpTutorial.SubHeading = pt.SubHeading;
                    phpTutorial.Example = pt.Example;
                    phpTutorial.ActiveStatus = pt.ActiveStatus;
                    phpTutorial.Date = pt.Date;
                    phpTutorial.AdminId = pt.AdminId;

                    db.PhpTutorials.AddOrUpdate(phpTutorial);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "PHP tutorial is updated successfully";
                    return View(phpTutorial);
                }

                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetInactiveHtmlTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.HtmlTutorials.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveHtmlTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.HtmlTutorials.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }




        public ActionResult GetInactiveCssTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.CssTutorials.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveCssTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.CssTutorials.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }




        public ActionResult GetInactiveJsTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptTutorials.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveJsTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptTutorials.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }





        public ActionResult GetInactiveJsExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptExamples.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveJsExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptExamples.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }






        public ActionResult GetInactiveJqTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryTutorials.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveJqTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryTutorials.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }







        public ActionResult GetInactiveJqExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryExamples.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveJqExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryExamples.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }








        public ActionResult GetInactiveSyntaxBoxes()
        {
            if (GetSignInStatus())
            {
                return View(db.SqlSyntaxBoxes.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveSyntaxBoxes()
        {
            if (GetSignInStatus())
            {
                return View(db.SqlSyntaxBoxes.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }



        public ActionResult GetInactiveSqlExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.SqlExamples.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActiveSqlExamples()
        {
            if (GetSignInStatus())
            {
                return View(db.SqlExamples.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }









        public ActionResult GetInactivePhpTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.PhpTutorials.Where(x => x.ActiveStatus == false).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        public ActionResult GetActivePhpTutorials()
        {
            if (GetSignInStatus())
            {
                return View(db.PhpTutorials.Where(x => x.ActiveStatus == true).ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }







        public void GetAllViewBag()
        {
            if (GetSignInStatus())
            {
                var htmlInactiveTutorials = db.HtmlTutorials.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.HtmlInactiveTutorialsCounter = htmlInactiveTutorials.Count;
                ViewBag.HtmlInactiveTutorials = htmlInactiveTutorials;

                var htmlActiveTutorials = db.HtmlTutorials.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.HtmlActiveTutorialsCounter = htmlActiveTutorials.Count;
                ViewBag.HtmlActiveTutorials = htmlActiveTutorials;

                //CSS TUTORIALS
                var cssActiveTutorials = db.CssTutorials.Where(x => x.ActiveStatus == true);
                ViewBag.CssActiveTutorialsCounter = cssActiveTutorials.Count();
                ViewBag.CssActiveTutorials = cssActiveTutorials;

                var cssInActiveTutorials = db.CssTutorials.Where(x => x.ActiveStatus == false);
                ViewBag.CssInActiveTutorialsCounter = cssInActiveTutorials.Count();
                ViewBag.CssInActiveTutorials = cssInActiveTutorials;


                //JAVASCRIPT TUTORIALS
                var jsActiveTuts = db.JavaScriptTutorials.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.JsActiveTutsCounter = jsActiveTuts.Count();
                ViewBag.JsActiveTuts = jsActiveTuts;

                var jsInActiveTuts = db.JavaScriptTutorials.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.JsInActiveTutsCounter = jsInActiveTuts.Count();
                ViewBag.JsInActiveTuts = jsInActiveTuts;

                //JAVASCRIPT EXAMPLES
                var jsActiveEx = db.JavaScriptExamples.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.jsActiveExCounter = jsActiveEx.Count();
                ViewBag.jsActiveEx = jsActiveEx;

                var jsInActiveEx = db.JavaScriptExamples.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.jsInActiveExCounter = jsInActiveEx.Count();
                ViewBag.jsInActiveEx = jsInActiveEx;


                //JQUERY TUTORIALS
                var jqActiveTuts = db.JqueryTutorials.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.JqActiveTutsCounter = jqActiveTuts.Count();
                ViewBag.JqActiveTuts = jqActiveTuts;

                var jqInActiveTuts = db.JqueryTutorials.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.JqInActiveTutsCounter = jqInActiveTuts.Count();
                ViewBag.JqInActiveTuts = jqInActiveTuts;

                //JQUERY EXAMPLES
                var jqActiveEx = db.JqueryExamples.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.jqActiveExCounter = jqActiveEx.Count();
                ViewBag.jqActiveEx = jqActiveEx;

                var jqInActiveEx = db.JqueryExamples.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.JqInActiveExCounter = jqInActiveEx.Count();
                ViewBag.JqInActiveEx = jqInActiveEx;

                //SQL EXAMPLES
                var sqlActiveEx = db.SqlExamples.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.sqlActiveExCounter = sqlActiveEx.Count();
                ViewBag.sqlActiveEx = sqlActiveEx;

                var sqlInActiveEx = db.SqlExamples.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.sqlInActiveExCounter = sqlInActiveEx.Count();
                ViewBag.sqlInActiveEx = sqlInActiveEx;

                //SQL SyntaxBoxes
                var sqlActiveSynBoxes = db.SqlSyntaxBoxes.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.sqlActiveSynBoxesCounter = sqlActiveSynBoxes.Count();
                ViewBag.sqlActiveSynBoxes = sqlActiveSynBoxes;

                var sqlInActiveSynBoxes = db.SqlSyntaxBoxes.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.sqlInActiveSynBoxesCounter = sqlInActiveSynBoxes.Count();
                ViewBag.sqlInActiveSynBoxes = sqlInActiveSynBoxes;


                //PHP TUTORIALS
                var phpActiveTuts = db.PhpTutorials.Where(x => x.ActiveStatus == true).ToList();
                ViewBag.phpActiveTutsCounter = phpActiveTuts.Count();
                ViewBag.phpActiveTuts = phpActiveTuts;

                var phpInActiveTuts = db.PhpTutorials.Where(x => x.ActiveStatus == false).ToList();
                ViewBag.phpInActiveTutsCounter = phpInActiveTuts.Count();
                ViewBag.phpInActiveTuts = phpInActiveTuts;
            }
        }
    }
}