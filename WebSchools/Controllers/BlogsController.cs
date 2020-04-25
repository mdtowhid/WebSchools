using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;
using WebSchools.Models.BlogVm;

namespace WebSchools.Controllers
{
    public class BlogsController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        // GET: Blogs

        public void ViewBags()
        {
            ViewBag.Bloggers = db.Registrations.Where(x => x.RegistrationStatus == "blogger").ToList();
            ViewBag.Blogs = db.BlogContents.ToList();
            ViewBag.BlogCategories = db.BlogCategories.ToList();
        }
        public ActionResult Home()
        {
            ViewBags();
            return View();
        }

        public ActionResult Collections(int id)
        {
            ViewBags();
            
            var blogs = db.BlogContents.Where(x => x.BlogCategoryId == id).ToList();
            if (blogs.Count() <= 0)
            {
                ViewBag.EmptyBlogs = "There is no blogs about this category.</br> " +
                                     "Write about this blog Create an account <a href="+"~/account/signUp/>Click here @raquo; </a>";
                return View();
            }
            else
            {
                ViewBag.BlogsByCategoryId = blogs;
                return View();
            }
        }

        public ActionResult ContinueReading(int id)
        {
            ViewBags();
            var blog = db.BlogContents.Find(id);
            return View(blog);
        }
    }
}