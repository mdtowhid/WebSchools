using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebSchools.Models;
using WebSchools.Crypto;

namespace WebSchools.Areas.User.Controllers
{
    public class RegistrationController : Controller
    {

        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        public ActionResult Account()
        {
            return View();
        }

        //public ActionResult Index()
        //{
        //    return View(db.Registrations.ToList());
        //}

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Registration registration = db.Registrations.Find(id);
        //    if (registration == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(registration);
        //}

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult SignUp()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignUp(Registration registration)
        //{
        //    var regStatus = GetCheckedStatus(registration);
        //    bool status = false;
        //    string message = "";

        //    if (ModelState.IsValid)
        //    {
        //        registration.RegistrationStatus = regStatus;
        //        //for suggesting available user name
        //        registration.RememberMe = false;
        //        registration.NameInUse = true;

        //        //generating activation code
        //        registration.ActivationCode = Guid.NewGuid();
                
        //        registration.Password = Crypto.Crypto.Hash(registration.Password);
        //        registration.ConfirmPassword = Crypto.Crypto.Hash(registration.ConfirmPassword);

        //        //initializing is email verified as false
        //        registration.IsEmailVerified = false;


        //        db.Registrations.Add(registration);
        //        if (db.SaveChanges() > 0)
        //        {
        //            ViewBag.Message = string.Format("Welcome {0} Your Information Is saved Successfully",
        //                registration.UserName);
        //            ViewBag.MessageType = "success";

        //            //SendVerificationLinkEmail(registration.Email, registration.ActivationCode.ToString());
        //            //message = "Registration successfully done. Account activation link " +
        //                //" has been sent to your email id:" + registration.Email;
        //            status = true;
        //            //return View();
        //        }
        //        /*
        //        *Need to update articleWriter table to fetch writer info
        //        *so that when writer submit new article i can track everything
        //        */
        //        if (regStatus == "cWritter")
        //        {
        //            ArticleWriter articleWriter = new ArticleWriter();


        //            articleWriter.UserName = registration.UserName;
        //            articleWriter.ArticleCounter = 0;
        //            articleWriter.NumberOfArticleDone = 0;

        //            db.ArticleWriters.Add(articleWriter);
        //            db.SaveChanges();



        //            //it always null
        //            //var checkArticleWriter = db.ArticleWriters.FirstOrDefault(x => x.Id == registration.Id);

        //            ////here i need null value
        //            //if (checkArticleWriter == null)
        //            //{
        //            //    articleWriter.UserName = registration.UserName;
        //            //    articleWriter.ArticleCounter = 0;
        //            //    articleWriter.NumberOfArticleDone = 0;

        //            //    db.ArticleWriters.Add(articleWriter);
        //            //    db.SaveChanges();
        //            //}
        //        }

        //        ViewBag.Message = message;
        //        ViewBag.Status = status;
        //    }
        //    return RedirectToAction("Index","Index", new {area=""});
        //}

        //[NonAction]
        //public void SendVerificationLinkEmail(string emailID, string activationCode)
        //{
        //    var verifyUrl = "/User/VerifyAccount/" + activationCode;
        //    if (Request.Url != null)
        //    {
        //        var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //        var fromEmail = new MailAddress("newtowhid1232@gmail.com", "Web Schools");
        //        var toEmail = new MailAddress(emailID);
        //        var fromEmailPassword = "********"; // Replace with actual password
        //        string subject = "Your account is successfully created!";

        //        string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //                      " successfully created. Please click on the below link to verify your account" +
        //                      " <br/><br/><a href='" + link + "'>" + link + "</a> ";

        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //        };
            
        //        using (var message = new MailMessage(fromEmail, toEmail)
        //        {
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = true
        //        }) smtp.Send(message);
        //    }
        //}

        //public void GetAvailableUserName(string userName)
        //{
        //    Registration registration = new Registration();
        //    registration.NameInUse = false;

        //    while (UserNameExist(userName))
        //    {
        //        Random random = new Random();
        //        int randomNumber = random.Next(1, 100);
        //        userName = userName + randomNumber;

        //        registration.NameInUse = true;
        //    }
        //    registration.UserName = userName;

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    Response.Write(js.Serialize(registration));
        //}

        //public bool UserNameExist(string userName)
        //{
        //    var check = db.Registrations.Any(x => x.UserName == userName);
        //    if (check)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private string GetCheckedStatus(Registration registration)
        //{
        //    var regStatus = "";
        //    if (registration.RegistrationStatus == "asTecaher")
        //    {
        //        regStatus = "teacher";
        //    }
        //    else if (registration.RegistrationStatus == "asStudent")
        //    {
        //        regStatus = "student";
        //    }
        //    else if (registration.RegistrationStatus == "asContentWritter")
        //    {
        //        regStatus = "cWritter";
        //    }
        //    else if (registration.RegistrationStatus == "asVisitors")
        //    {
        //        regStatus = "visitors";
        //    }

        //    return regStatus;
        //}


        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Registration registration = db.Registrations.Find(id);
        //    if (registration == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(registration);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,RegistrationStatus")] Registration registration)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(registration).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(registration);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Registration registration = db.Registrations.Find(id);
        //    if (registration == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(registration);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Registration registration = db.Registrations.Find(id);
        //    db.Registrations.Remove(registration);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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


    }
}