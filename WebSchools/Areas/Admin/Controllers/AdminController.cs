using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.Html;

namespace WebSchools.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();

        public ActionResult SignInAdmin()
        {
            ViewBag.AdminTypes = db.AdminCategories.ToList();
            return View();
        }

        public void SetHtmlAdminSession(Models.Admin admin)
        {
            Session["HtmlAdminName"] = admin.Name;
            Session["HtmlAdminEmail"] = admin.Email;
            Session["HtmlAdminId"] = admin.Id;
        }

        public void SetCssAdminSession(Models.Admin admin)
        {
            Session["CssAdminName"] = admin.Name;
            Session["CssAdminEmail"] = admin.Email;
            Session["CssAdminId"] = admin.Id;
        }

        public void SetJsAdminSession(Models.Admin admin)
        {
            Session["JsAdminName"] = admin.Name;
            Session["JsAdminEmail"] = admin.Email;
            Session["JsAdminId"] = admin.Id;
        }

        public void SetJqAdminSession(Models.Admin admin)
        {
            Session["JqAdminName"] = admin.Name;
            Session["JqAdminEmail"] = admin.Email;
            Session["JqAdminId"] = admin.Id;
        }

        public void SetSqlAdminSession(Models.Admin admin)
        {
            Session["SqlAdminName"] = admin.Name;
            Session["SqlAdminEmail"] = admin.Email;
            Session["SqlAdminId"] = admin.Id;
        }

        public void SetPhpAdminSession(Models.Admin admin)
        {
            Session["PhpAdminName"] = admin.Name;
            Session["PhpAdminEmail"] = admin.Email;
            Session["PhpAdminId"] = admin.Id;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInAdmin(Models.Admin admin)
        {
            ViewBag.AdminType = db.Admins.ToList();
            var a = db.Admins.FirstOrDefault(x =>
                x.Email == admin.Email && x.Password == admin.Password);

            if (a != null)
            {
                if (admin.AdminType == "HTML Controller")
                {
                    SetHtmlAdminSession(a);
                    return RedirectToAction("Index", "AdminHtml");
                }
                else if (admin.AdminType == "CSS Controller")
                {
                    SetCssAdminSession(a);
                    return RedirectToAction("Index", "AdminCss");
                }
                else if (admin.AdminType == "JavaScript Controller")
                {
                    SetJsAdminSession(a);
                    return RedirectToAction("Index", "AdminJs");
                }
                else if (admin.AdminType == "jQuery Controller")
                {
                    SetJqAdminSession(a);
                    return RedirectToAction("Index", "AdminJq");
                }
                else if (admin.AdminType == "SQL Controller")
                {
                    SetSqlAdminSession(a);
                    return RedirectToAction("Index", "AdminSql");
                }
                else if (admin.AdminType == "PHP Controller")
                {
                    SetPhpAdminSession(a);
                    return RedirectToAction("Index", "AdminPhp");
                }

            }

            ViewBag.LoggedInError = "Given information is error";
            return View();

        }
    }

}