using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSchools.Areas.Teacher.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/Teacher
        public ActionResult Home()
        {
            return View();
        }
    }
}