using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using WebSchools.Models;
using WebSchools.Models.Css;
using WebSchools.Models.Html;
using WebSchools.Models.JavaScript;
using WebSchools.Models.Jquery;
using WebSchools.Models.Php;


namespace WebSchools.Areas.SpArea.Controllers
{
    public class TController : Controller
    {
        public WebSchoolsDbContext db = new WebSchoolsDbContext();

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

        [HttpGet]
        public ActionResult SPASignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SPASignIn(SuperAdmins spa)
        {
            var sa = db.SuperAdmins
                .FirstOrDefault(x => x.Email == spa.Email && x.Password == spa.Password && x.ActiveStatus == true);
            if (sa != null)
            {
                Session["SuperAdminID"] = sa.SuperAdminID;
                Session["ActiveStatus"] = sa.ActiveStatus;
                Session["Email"] = sa.Email;
                Session["FullName"] = sa.FullName;
                Session["Password"] = sa.Password;

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Oops!Given information is error.");
                return View();
            }
        }

        // GET: SpArea/T
        public ActionResult Index()
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


        public ActionResult GetHtmlIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.HtmlIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetCssIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.CssIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetJsIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.JavaScriptIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetJqIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.JqueryIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetSqlIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.SqlIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }


        public ActionResult GetPhpIndices()
        {
            if (GetSignInStatus())
            {
                return View(db.PhpIndices.ToList());
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }





        [HttpGet]
        public ActionResult CreateHtmlIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.HtmlIndices = db.HtmlIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        public ActionResult CreateHtmlIndex(HtmlIndex htmlIndex)
        {
            if (GetSignInStatus())
            {
                ViewBag.HtmlIndices = db.HtmlIndices.ToList();
                var c = db.HtmlIndices.FirstOrDefault(x => x.Slug == htmlIndex.Slug);
                if (c != null)
                {
                    if (c.Slug == htmlIndex.Slug)
                    {
                        ViewBag.Error = htmlIndex.Slug + "Slug already exist";
                        return View();
                    }
                    if (c.Heading == htmlIndex.Heading)
                    {
                        ViewBag.Error = htmlIndex.Heading + "Heading already exist";
                        return View();
                    }
                }
                else if (ModelState.IsValid)
                {
                    htmlIndex.Slug = htmlIndex.Slug.Replace(" ", "_");
                    db.HtmlIndices.Add(htmlIndex);
                    db.SaveChanges();
                    ViewBag.Success = "New html index created successfully!";
                    ModelState.Clear();
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
        public ActionResult EditHtmlIndex(string id)
        {
            if (GetSignInStatus())
            {
                var hIndex = db.HtmlIndices.FirstOrDefault(x => x.Slug == id);
                if (hIndex != null)
                {
                    return View(hIndex);
                }
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        public ActionResult EditHtmlIndex(HtmlIndex hi)
        {
            if (GetSignInStatus())
            {
                ViewBag.CssIndices = db.CssIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }


        }


        [HttpGet]
        public ActionResult CreateCssIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.CssIndices = db.CssIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCssIndex(CssIndex ci)
        {
            if (GetSignInStatus())
            {
                ViewBag.CssIndices = db.CssIndices.ToList();
                if (ModelState.IsValid)
                {
                    var check = db.CssIndices.FirstOrDefault(x => x.Slug == ci.Slug);
                    if (check != null)
                    {
                        ViewBag.Error = "Slug already exist";
                        return View();
                    }
                    else
                    {
                        ci.Slug = ci.Slug.Replace(" ", "_");
                        db.CssIndices.Add(ci);
                        db.SaveChanges();
                        ViewBag.Success = "New CSS index is saved successfully!";
                        ModelState.Clear();
                        return View();
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
        public ActionResult EditCssIndex(string id)
        {
            if (GetSignInStatus())
            {
                var ci = db.CssIndices.FirstOrDefault(x => x.Slug == id);
                return View(ci);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCssIndex(CssIndex ci)
        {
            if (GetSignInStatus())
            {
                var cindex = db.CssIndices.FirstOrDefault(x => x.Id == ci.Id);
                var ct = db.CssTutorials.Where(x => x.Slug == cindex.Slug).ToList();
                if (ModelState.IsValid)
                {
                    cindex.ActiveStatus = ci.ActiveStatus;
                    cindex.Slug = ci.Slug.Replace(" ", "_");
                    cindex.Heading = ci.Heading;
                    cindex.MainHeadingDescription = ci.MainHeadingDescription;
                    cindex.MenuDivider = ci.MenuDivider;
                    cindex.Sort = ci.Sort;
                    db.CssIndices.AddOrUpdate(cindex);
                    db.SaveChanges();

                    if (ct != null)
                    {
                        foreach (CssTutorial cssTutorial in ct)
                        {
                            cssTutorial.Slug = ci.Slug;
                            db.CssTutorials.AddOrUpdate(cssTutorial);
                            db.SaveChanges();
                        }
                        ViewBag.Success = "Css Index is updated successfully!";
                        ModelState.Clear();
                        return View(cindex);
                    }
                    return View(ci);
                }
                return View(ci);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpGet]
        public ActionResult CreateJsIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJsIndex(JavaScriptIndex jsIndex)
        {
            if (GetSignInStatus())
            {
                ViewBag.JsIndices = db.JavaScriptIndices.ToList();
                var check = db.JavaScriptIndices.FirstOrDefault(x => x.Slug == jsIndex.Slug);
                if (check != null)
                {
                    if (check.Slug == jsIndex.Slug)
                    {
                        ViewBag.Error = "This slug already exist.";
                        return View();
                    }
                }
                else if (ModelState.IsValid)
                {
                    jsIndex.Slug = jsIndex.Slug.Replace(" ", "_");
                    db.JavaScriptIndices.Add(jsIndex);
                    db.SaveChanges();
                    ViewBag.Success = "New Index is Created Susseccfully";
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
        public ActionResult EditJsIndex(string id)
        {
            if (GetSignInStatus())
            {
                var check = db.JavaScriptIndices.FirstOrDefault(x => x.Slug == id);
                return View(check);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJsIndex(JavaScriptIndex jsIndex)
        {
            if (GetSignInStatus())
            {
                var check = db.JavaScriptIndices.FirstOrDefault(x => x.Id == jsIndex.Id);
                if (check != null)
                {
                    if (jsIndex.Slug == "js_default")
                    {
                        ViewBag.Error = "Default slug can't be editable";
                        return View(jsIndex);
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            var jsExample = db.JavaScriptExamples.Where(x => x.JavaScriptIndexId == check.Id).ToList();
                            if (jsExample != null)
                            {
                                foreach (JavaScriptExample jse in jsExample)
                                {
                                    jse.Slug = jsIndex.Slug;
                                    db.JavaScriptExamples.AddOrUpdate(jse);
                                    db.SaveChanges();
                                }
                            }

                            var jsTutorial = db.JavaScriptTutorials.Where(x => x.JavaScriptIndexId == check.Id).ToList();
                            if (jsTutorial != null)
                            {
                                foreach (JavaScriptTutorial jt in jsTutorial)
                                {
                                    jt.Slug = jsIndex.Slug;
                                    db.JavaScriptTutorials.AddOrUpdate(jt);
                                    db.SaveChanges();
                                }
                            }

                            check.ActiveStatus = jsIndex.ActiveStatus;
                            check.MenuDivider = jsIndex.MenuDivider;
                            check.Heading = jsIndex.Heading;
                            check.MainHeadingDescription = jsIndex.MainHeadingDescription;
                            check.Slug = jsIndex.Slug;
                            check.Sort = jsIndex.Sort;

                            db.JavaScriptIndices.AddOrUpdate(check);
                            db.SaveChanges();
                            ViewBag.Success = "Javascript Index updated successfully.";
                            ModelState.Clear();
                            return View(check);
                        }
                    }
                }
                return View(jsIndex);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpGet]
        public ActionResult CreateJqIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.JqIndices = db.JqueryIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJqIndex(JqueryIndex jqi)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqIndices = db.JqueryIndices.ToList();

                var check = db.JqueryIndices.FirstOrDefault(x => x.Slug == jqi.Slug);
                if (check != null)
                {
                    if (check.Slug == jqi.Slug)
                    {
                        ViewBag.Error = "This slug already exist.";
                        return View();
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.JqueryIndices.Add(jqi);
                        db.SaveChanges();
                        ViewBag.Success = "New Index is Created Susseccfully";
                        ModelState.Clear();
                        return View();
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
        public ActionResult EditJqIndex(int id)
        {
            if (GetSignInStatus())
            {
                var c = db.JqueryIndices.Find(id);
                return View(c);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditJqIndex(JqueryIndex jqi)
        {
            if (GetSignInStatus())
            {
                ViewBag.JqIndex = db.JqueryIndices.Find(jqi.Id);
                var check = db.JqueryIndices.FirstOrDefault(x => x.Id == jqi.Id);

                if (check != null)
                {
                    if (check.Slug == "jq_default")
                    {
                        ViewBag.Error = "Default slug cannot be editable";
                        return View(check);
                    }
                    else if (ModelState.IsValid)
                    {
                        var jqTutorials = db.JqueryTutorials.Where(x => x.JqueryIndexId == check.Id).ToList();
                        if (jqTutorials != null)
                        {
                            foreach (JqueryTutorial jqt in jqTutorials)
                            {
                                jqt.Slug = jqi.Slug;
                                db.JqueryTutorials.AddOrUpdate(jqt);
                                db.SaveChanges();
                            }
                        }

                        var jqExample = db.JqueryExamples.Where(x => x.JqueryIndexId == check.Id).ToList();
                        if (jqExample != null)
                        {
                            foreach (JqueryExample jqe in jqExample)
                            {
                                jqe.Slug = jqi.Slug;
                                db.JqueryExamples.AddOrUpdate(jqe);
                                db.SaveChanges();
                            }
                        }

                        check.ActiveStatus = jqi.ActiveStatus;
                        check.Heading = jqi.Heading;
                        check.MainHeadingDescription = jqi.MainHeadingDescription;
                        check.MenuDivider = jqi.MenuDivider;
                        check.Slug = jqi.Slug;
                        check.Sort = jqi.Sort;
                        db.JqueryIndices.AddOrUpdate(check);
                        db.SaveChanges();
                        ViewBag.Success = "Jquery Index updated successfully.";
                        ModelState.Clear();
                        return View(check);
                    }

                }
                return View(check);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpGet]
        public ActionResult CreateSqlIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIndices = db.SqlIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSqlIndex(SqlIndex si)
        {
            if (GetSignInStatus())
            {
                ViewBag.SqlIndices = db.SqlIndices.ToList();
                var slug = db.SqlIndices.FirstOrDefault(x => x.Slug == si.Slug);
                if (slug != null)
                {
                    ViewBag.Error = "This Slug already exist";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    si.Slug = si.Slug.Replace(" ", "_");
                    db.SqlIndices.Add(si);
                    db.SaveChanges();
                    ViewBag.Success = "New SQL index is added susseccfully";
                    ModelState.Clear();
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
        public ActionResult EditSqlIndex(int? id)
        {
            if (GetSignInStatus())
            {
                return View(db.SqlIndices.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditSqlIndex(SqlIndex si)
        {
            if (GetSignInStatus())
            {
                var c = db.SqlIndices.FirstOrDefault(x => x.Id == si.Id);
                if (si.Slug == "sql_default")
                {
                    ViewBag.Error = "Default slug can't be editable.";
                    return View();
                }
                else if (c != null && ModelState.IsValid)
                {
                    var sqlExamples = db.SqlExamples.Where(x => x.Slug == c.Slug).ToList();
                    if (sqlExamples != null)
                    {
                        foreach (SqlExample se in sqlExamples)
                        {
                            se.Slug = si.Slug;
                            db.SqlExamples.AddOrUpdate(se);
                            db.SaveChanges();
                        }
                    }

                    var sqlSyntax = db.SqlSyntaxBoxes.Where(x => x.Slug == c.Slug).ToList();
                    if (sqlSyntax != null)
                    {
                        foreach (SqlSyntaxBox ssb in sqlSyntax)
                        {
                            ssb.Slug = si.Slug;
                            db.SqlSyntaxBoxes.AddOrUpdate(ssb);
                            db.SaveChanges();
                        }
                    }

                    c.Slug = si.Slug.Replace(" ", "_");
                    c.Heading = si.Heading;
                    c.ActiveStatus = si.ActiveStatus;
                    c.MainHeadingDescription = si.MainHeadingDescription;
                    c.MenuDivider = si.MenuDivider;
                    db.SqlIndices.AddOrUpdate(c);
                    db.SaveChanges();
                    ViewBag.Success = "Sql Index is edited successfully";
                    return View(c);
                }
                return View(si);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult CreatePhpIndex()
        {
            if (GetSignInStatus())
            {
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSqlIndex(PhpIndex pi)
        {
            if (GetSignInStatus())
            {
                ViewBag.PhpIndices = db.PhpIndices.ToList();
                var getSlug = db.PhpIndices.FirstOrDefault(x => x.Slug == pi.Slug);
                pi.ActiveStatus = false;
                if (getSlug != null)
                {
                    ViewBag.Error = "This index already exist.";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    pi.Slug = pi.Slug.Replace(" ", "_");
                    db.PhpIndices.Add(pi);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "New PHP index is added successfully.";
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
        [ValidateInput(false)]
        public ActionResult EditPhpIndex(int? id)
        {
            if (GetSignInStatus())
            {
                return View(db.PhpIndices.Find(id));
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPhpIndex(PhpIndex pi)
        {
            if (GetSignInStatus())
            {
                var pIndex = db.PhpIndices.Find(pi.Id);
                var pTutorial = db.PhpTutorials.Where(x => x.Slug == pIndex.Slug).ToList();

                pi.ActiveStatus = false;
                if (pi.Slug == "php_default")
                {
                    ViewBag.Error = "Default slug can't be editable";
                    return View(pi);
                }
                else if (ModelState.IsValid)
                {
                    foreach (PhpTutorial pt in pTutorial)
                    {
                        pt.Slug = pi.Slug;
                        db.PhpTutorials.AddOrUpdate(pt);
                        db.SaveChanges();
                    }

                    pIndex.Heading = pi.Heading;
                    pIndex.Slug = pi.Slug.Replace(" ", "_");
                    db.PhpIndices.AddOrUpdate(pIndex);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Success = "PHP index is updated successfully.";
                    return View(pIndex);
                }
                return View(pi);
            }
            else
            {
                return RedirectToAction("SPASignIn", "T", new { area = "SpArea" });
            }

        }


    }
}
