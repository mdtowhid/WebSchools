using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.BlogVm;

namespace WebSchools.Areas.Blog.Controllers
{
    public class BlogContentsController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        private string adminName;
        private int adminUniqueId;
        private string adminEmail;
        private string imgName = "";
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

        // GET: Blog/BlogContents
        public ActionResult Home()
        {
            if (GetSignInStatus())
            {
                ViewBag.BlogContents = db.BlogContents.Where(x => x.BloggerId == adminUniqueId).ToList();
                return View();
            }

            return RedirectToAction("SignUp", "Account", new { area=""});
        }

        public ActionResult Details(int? id)
        {
            if (GetSignInStatus())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BlogContent blogContent = db.BlogContents.Find(id);
                if (blogContent == null)
                {
                    return HttpNotFound();
                }
                return View(blogContent);
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });

        }

        public ActionResult Create()
        {
            if (GetSignInStatus())
            {
                ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "BlogCategoryName");
                return View();
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        public void ImageProccessing(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                //Get file extension
                var ext = file.ContentType.ToLower();
                //Verify extension
                if (ext != "image/jpg" && ext != "image/jpeg" && ext != "image/pjpeg" && ext != "image/gif" &&
                    ext != "image/x-png" && ext != "image/png")
                {
                    ViewBag.Error = "The image was not uploded. File format not suuported";
                    //ModelState.AddModelError("", "The image was not uploded. File format not suuported");
                }
                imgName = Path.GetFileName(file.FileName);
                string phyPath = Server.MapPath("~/UpImages");
                string phyFullPath = Path.Combine(phyPath, imgName);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(400, 400);
                //file.SaveAs(phyFullPath);
                img.Save(phyFullPath);
            }
        }

        public Registration GetBloggerData(int id)
        {
            var blogger = db.Registrations.Find(adminUniqueId);
            return blogger;
        }

        public void SetFormValues(BlogContent blogContent)
        {
            blogContent.ImageUrl = imgName;
            blogContent.BloggerId = adminUniqueId;
            blogContent.Date = DateTime.Now;
            blogContent.ActiveStatus = false;
            blogContent.Slug = blogContent.BlogHeading.Replace(" ", "_").ToLower();
            blogContent.Sort = 0;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogContent blogContent, HttpPostedFileBase file)
        {
            if (GetSignInStatus())
            {
                ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "BlogCategoryName", blogContent.BlogCategoryId);
                if (ModelState.IsValid)
                {
                    //working with uploading image
                    ImageProccessing(file);
                    Registration regBlogger = GetBloggerData(adminUniqueId);

                    SetFormValues(blogContent);

                    blogContent.PostedBy = regBlogger.UserName;
                    db.BlogContents.Add(blogContent);
                    db.SaveChanges();

                    return RedirectToAction("Home");
                }
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });
        }


        public ActionResult Edit(int? id)
        {
            if (GetSignInStatus())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BlogContent blogContent = db.BlogContents.Find(id);
                if (blogContent == null)
                {
                    return HttpNotFound();
                }
                ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "BlogCategoryName", blogContent.BlogCategoryId);
                return View(blogContent);
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogContent blogContent, HttpPostedFileBase file)
        {
            if (GetSignInStatus())
            {
                if (ModelState.IsValid)
                {
                    ImageProccessing(file);

                    var model = db.BlogContents.Find(blogContent.Id);

                    //delete old image file
                    string oldFilePath = model.ImageUrl;
                    string phyPath = Server.MapPath("~/UpImages");
                    string fullPath = Path.Combine(phyPath, oldFilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    Registration regBlogger = GetBloggerData(adminUniqueId);

                    SetFormValues(blogContent);

                    db.BlogContents.AddOrUpdate(blogContent);
                    db.SaveChanges();
                    return RedirectToAction("Home");
                }
                ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "BlogCategoryName", blogContent.BlogCategoryId);
                return View(blogContent);
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        // GET: Blog/BlogContents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GetSignInStatus())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BlogContent blogContent = db.BlogContents.Find(id);
                if (blogContent == null)
                {
                    return HttpNotFound();
                }
                return View(blogContent);
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GetSignInStatus())
            {
                BlogContent blogContent = db.BlogContents.Find(id);
                db.BlogContents.Remove(blogContent);
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            return RedirectToAction("SignUp", "Account", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
