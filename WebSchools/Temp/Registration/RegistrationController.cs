using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebSchools.Models;

namespace WebSchools.Areas.Controllers
{
    public class RegistrationController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        // GET: /Registration/
        //public ActionResult Index()
        //{
        //    return View(db.Registrations.ToList());
        //}

        //// GET: /Registration/Details/5
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Registration registration)
        //{
        //    var regStatus = GetCheckedStatus(registration);
        //    if (ModelState.IsValid)
        //    {
        //        registration.RegistrationStatus = regStatus;
        //        registration.NameInUse = true;

        //        db.Registrations.Add(registration);
        //        if (db.SaveChanges() > 0)
        //        {
        //            ViewBag.Message = string.Format("Welcome {0} Your Information Is saved Successfully",
        //                registration.UserName);
        //            ViewBag.MessageType = "success";
        //            return View();
        //        }
        //    }
        //    return View(registration);
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
        //public ActionResult Edit([Bind(Include="Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,RegistrationStatus")] Registration registration)
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //public JsonResult IsExistEmail(string Email)
        //{
        //    var check = db.Registrations.Any(x => x.Email == Email);
        //    if (check)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult LogIn()
        //{
        //    return View();
        //}
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult LogIn(Registration registration)
        //{
        //    registration.RegistrationStatus = GetCheckedStatus(registration);
        //    var check =
        //        db.Registrations.FirstOrDefault(
        //            x => x.Email == registration.Email && x.Password == registration.Password && x.RegistrationStatus == registration.RegistrationStatus);
        //    if (check != null)
        //    {
        //        Session["UserName"] = check.UserName;
        //        Session["UserType"] = check.RegistrationStatus;

        //        return RedirectToAction("Index", "Html");
        //    }
        //    else
        //    {
        //        ViewBag.Error = "User Email Or Password Is Invalid";
        //    }

        //    return View();
        //}
    }
}
