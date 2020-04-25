using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebSchools.Models;

namespace WebSchools.Controllers
{
    public class AccountController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        
        private string imgName = "";


        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        public void ImageProccessing(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                imgName = Path.GetFileName(file.FileName);
                string phyPath = Server.MapPath("~/Images/RegImg");
                string phyFullPath = Path.Combine(phyPath, imgName);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(250, 250);
                img.Save(phyFullPath);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Registration registration, HttpPostedFileBase file)
        {

            if (registration.RegistrationStatus == "-1")
            {
                ViewBag.Error = "Please select your area";
                return View();
            }
            else if (ModelState.IsValid)
            {
                var ext = file.ContentType.ToLower();
                if (ext != "image/jpg" && ext != "image/jpeg" && ext != "image/pjpeg" && ext != "image/gif" &&
                    ext != "image/x-png" && ext != "image/png")
                {
                    ModelState.AddModelError("", "The image was not uploded. File format not suuported");
                    return View();
                }
                else
                { 
                    ImageProccessing(file);

                    registration.ImageUrl = imgName;
                    registration.ActiveStatus = true;
                    registration.Date = DateTime.Now;

                    registration.IsEmailVerified = false;
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    ViewBag.Success = "Registration successfully completed as a " + registration.RegistrationStatus.ToUpper();
                    ModelState.Clear();
                    return View();
                }
            }
            return View();
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            if (Request.Url != null)
            {
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                var fromEmail = new MailAddress("newtowhid1232@gmail.com", "Web Schools");
                var toEmail = new MailAddress(emailID);
                var fromEmailPassword = "109203towhidWebschoo"; // Replace with actual password
                string subject = "Your account is successfully created!";

                string body = "<br/><br/>We are excited to tell you that your Web Schools account is" +
                              " successfully created. Please click on the below link to verify your account" +
                              " <br/><br/><a href='" + link + "'>" + link + "</a> ";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                }) smtp.Send(message);
            }

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.Credentials = new NetworkCredential("newtowhid1232@gmail.com", "*******");
            //smtp.EnableSsl = true;
            //MailMessage msg = new MailMessage();
            //msg.Subject = "Activation code to verify email address";
            //msg.Body = "Dear " + emailID + ", your activation code is " + activationCode +
            //           "\n\n\nThanks for reagards WebSchools";
            //string toAdress = emailID;
            //msg.To.Add(toAdress);
            //string formAdress = "WebSchools <newtowhid1232@gmail.com>";
            //msg.From = new MailAddress(formAdress);
            //try
            //{
            //    smtp.Send(msg);
            //}
            //catch
            //{
            //    throw;
            //}
        }


        public bool UserNameExist(string userName)
        {
            var check = db.Registrations.Any(x => x.UserName == userName);
            if (check)
            {
                return true;
            }
            return false;
        }

        public void GetAvailableUserName(string userName)
        {
            Registration registration = new Registration();
            registration.NameInUse = false;

            while (UserNameExist(userName))
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 100);
                userName = userName + randomNumber;

                registration.NameInUse = true;
            }
            registration.UserName = userName;

            JavaScriptSerializer js = new JavaScriptSerializer();
            Response.Write(js.Serialize(registration));
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

        [HttpGet]
        public JsonResult IsExistEmail(string Email)
        {
            var check = db.Registrations.Any(x => x.Email == Email);
            if (check)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void CreateSession(Registration reg)
        {
            Session["UserName"] = reg.UserName;
            Session["Id"] = reg.Id;
            Session["RegistrationStatus"] = reg.RegistrationStatus;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Registration reg)
        {
            var check = db.Registrations.FirstOrDefault(x =>
                x.Email == reg.Email && x.Password == reg.Password && x.RegistrationStatus == reg.RegistrationStatus);
            if (check != null)
            {
                if (check.RegistrationStatus == "contentWritter")
                {
                    CreateSession(check);
                    return RedirectToAction("Home", "Article", new {area = "Content"});
                }
                if (check.RegistrationStatus == "blogger")
                {
                    CreateSession(check);
                    return RedirectToAction("Home", "BlogContents", new { area = "Blog" });
                }
                if (check.RegistrationStatus == "student")
                {
                    CreateSession(check);
                    return RedirectToAction("Home", "Student", new { area = "Student" });
                }
                if (check.RegistrationStatus == "teacher")
                {
                    CreateSession(check);
                    return RedirectToAction("Home", "Teacher", new { area = "Teacher" });
                }
            }
            else
            {
                ViewBag.LogInError = "Given Information is error.";
                return View();
            }
            return View();

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Index", new { area = "" });
        }

        public ActionResult SignUp1()
        {
            return View();
        }
    }
}