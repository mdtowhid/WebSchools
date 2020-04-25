using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.Content.Controllers
{
    public class ArticleController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();

        private string adminName;
        private int adminUniqueId;
        private string adminEmail;
        public bool GetSignInStatus()
        {
            if (Session["UserName"] != null && Session["Id"] != null && Session["RegistrationStatus"] != null)
            {
                adminName = (string)Session["UserName"];
                adminUniqueId = (int)Session["Id"];
                adminEmail = (string)Session["RegistrationStatus"];
                ViewBag.LogedInUser = db.Registrations.Find(adminUniqueId);
                return true;
            }

            return false;
        }

        public ActionResult Home()
        {
            if (GetSignInStatus())
            {
                ViewBag.Articles = db.Articles.ToList();
                return View();
            }
            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        [HttpGet]
        public ActionResult NewArticle()
        {
            if (GetSignInStatus())
            {
                ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Names");
                return View();
            }
            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        [HttpPost]
        public ActionResult NewArticle(Article article)
        {
            if (GetSignInStatus())
            {
                if (ModelState.IsValid)
                {
                    ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Names");
                    var cWritter = db.Registrations.Find(adminUniqueId);

                    article.Date = DateTime.Now;
                    article.ActiveStatus = false;
                    article.RegistrationId = adminUniqueId;
                    article.CreatedBy = cWritter.UserName;
                    db.Articles.Add(article);
                    db.SaveChanges();
                    ViewBag.ArticleSubmitionSuccessMessage = "The article has been saved successfully.Need an admin apprpval.";

                    return View();
                }
            }
            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (GetSignInStatus())
            {
                ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Names");
                var result = db.Articles.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    ViewBag.NullArticle = "There is no article.";
                    return View(ViewBag.NullArticle);
                }
            }
            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (GetSignInStatus())
            {
                ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Names");
                var cWritter = db.Registrations.Find(adminUniqueId);
                if (ModelState.IsValid)
                {
                    article.ActiveStatus = false;
                    article.CreatedBy = cWritter.UserName;
                    article.Date = DateTime.Now;
                    article.RegistrationId = adminUniqueId;
                    db.Articles.AddOrUpdate(article);
                    db.SaveChanges();
                    ViewBag.ArticleEditedSuccessMessage = "The article has been edited successfully.";
                }
                return View();
            }
            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        [HttpGet]
        public ActionResult DeleteArticle(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Index", new { area = "" });
            }
            var result = db.Articles.Find(id);
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult DeleteArticle(Article article)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Index", new { area = "" });
            }
            var result = db.Articles.FirstOrDefault(x => x.Id == article.Id);
            string userName = (string)Session["UserName"];
            var articleWriter = db.ArticleWriters.FirstOrDefault(x => x.UserName == userName);


            if (result != null)
            {
                db.Articles.Remove(result);
                db.SaveChanges();
                ViewBag.DeletionMassageStat = "This article is successfully deleted.";
                if (articleWriter != null)
                {
                    articleWriter.ArticleCounter = articleWriter.ArticleCounter - 1;
                    articleWriter.NumberOfArticleDone = articleWriter.NumberOfArticleDone - 1;
                    articleWriter.Balance = articleWriter.Balance - 10;
                    db.ArticleWriters.AddOrUpdate(articleWriter);
                    db.SaveChanges();
                }
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Index", new { area = "" });
            }
            var result = db.Registrations.Find(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult EditProfile(Registration registration)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Index", new { area = "" });
            }
            //var writer = db.Articles.FirstOrDefault(x => x.RegistrationId == registration.Id);
            var registrationUpdate = db.Registrations.FirstOrDefault(x => x.Id == registration.Id);
            //var articleWriter = db.ArticleWriters.FirstOrDefault(x => x.Id == writer.Id);

            registration.UserName = registration.UserName;
            db.Registrations.AddOrUpdate(registrationUpdate);
            db.SaveChanges();

            //if (articleWriter != null)
            //{
            //    articleWriter.UserName = registration.UserName;
            //    db.ArticleWriters.AddOrUpdate(articleWriter);
            //    db.SaveChanges();
            //    ViewBag.ProfileUpadationMessage = "You profile updated successfully!";
            //    return RedirectToAction("Home", "Article");
            //}

            return View();
        }
    }
}