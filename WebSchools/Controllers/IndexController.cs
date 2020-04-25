using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Controllers
{
    public class IndexController : Controller
    {
        WebSchoolsDbContext db = new WebSchoolsDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articles()
        {
            ViewBag.RegisteredUser = db.Registrations.ToList();
            ViewBag.Articles = db.Articles.ToList();
            return View();
        }
	}
}