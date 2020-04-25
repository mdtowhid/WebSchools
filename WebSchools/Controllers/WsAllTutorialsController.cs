using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSchools.Controllers
{
    public class WsAllTutorialsController : Controller
    {
        // GET: WsAllTutorials
        public ActionResult Tutorials()
        {
            return View();
        }

        public ActionResult UpComing()
        {
            return View();
        }
    }
}