using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        //[HttpGet]
        //public ActionResult LogIn()
        //{
        //    string regStatus = (string)Session["RegistrationStatus"];
        //    if (regStatus == "cWritter")
        //    {
        //        return RedirectToAction("Home", "Article", new { area = "Content" });
        //    }
        //    // will implement for different users
        //    //else if (regStatus == "student")
        //    //{
        //    //    return RedirectToAction("NewArticle", "Article", new { area = "Content" });
        //    //}
        //    return View();
        //}
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogIn(Registration registration)
        {
            bool remeberMe = registration.RememberMe;
            registration.RegistrationStatus = GetCheckedStatus(registration);
            var check =
                db.Registrations.FirstOrDefault(
                    x => x.Email == registration.Email && x.Password == registration.Password && x.RegistrationStatus == registration.RegistrationStatus);
            if (check != null)
            {
                if (!remeberMe)
                {
                    check.RememberMe = remeberMe;
                    db.Registrations.AddOrUpdate(check);
                    db.SaveChanges();
                }
                else
                {
                    check.RememberMe = remeberMe;
                    db.Registrations.AddOrUpdate(check);
                    db.SaveChanges();
                }
                Session["UserName"] = check.UserName;
                Session["Id"] = check.Id;
                Session["RegistrationStatus"] = check.RegistrationStatus;
                if (registration.RegistrationStatus == "cWritter")
                {
                    return RedirectToAction("Home", "Article", new { area = "Content" });
                }
                //return RedirectToAction("Index", "HtmlTutorial", new { area = "Html" });
            }
            else
            {
                ViewBag.Error = "User Email Or Password Is Invalid.<br/> Please Check <b>Checkboxes.</b>";
            }

            return RedirectToAction("Index", "Index", new{ area=""});
        }

        private string GetCheckedStatus(Registration registration)
        {
            var regStatus = "";
            if (registration.RegistrationStatus == "asTecaher")
            {
                regStatus = "teacher";
            }
            else if (registration.RegistrationStatus == "asStudent")
            {
                regStatus = "student";
            }
            else if (registration.RegistrationStatus == "asContentWritter")
            {
                regStatus = "cWritter";
            }
            else if (registration.RegistrationStatus == "asVisitors")
            {
                regStatus = "visitors";
            }

            return regStatus;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Index");
        }
	}
}