using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Controllers
{
    public class ImageResizerController : Controller
    {
        [HttpGet]
        public ActionResult Resizer()
        {
            ViewBag.ImgUrl = null;
            Response.Cookies.Add(new HttpCookie("ImageUrl", ""));
            return View();
        }

        public void DownloadFile(string fileName)
        {
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename="+fileName+"");
            Response.TransmitFile(Server.MapPath("~/Images/ResizedImg/"+fileName));
            Response.End();
        }

        [HttpPost]
        public ActionResult Resizer(ImgResize imgResize, HttpPostedFileBase file)
        {
            string imgName = Path.GetFileName(file.FileName);
            string ext = file.ContentType;
            string dir = Server.MapPath("~/Images/ResizedImg/");
            string phyDir = Path.Combine(dir, imgName);

            WebImage webImage = new WebImage(file.InputStream);
            if (imgResize.Width <= 0 || imgResize.Height <= 0)
            {
                ViewBag.Resized = "Please resize your image first.";
                //ModelState.AddModelError("", "");
                return View(imgResize);
            }
            else
            {
                webImage.Resize(imgResize.Width, imgResize.Height);
            }
            

            webImage.Save(phyDir);
            string imgUrl = "~/Images/ResizedImg/" + imgName;
            Session["ImageUrl"] = imgUrl;
            Session["ImageName"] = imgName;

            //Response.Cookies.Add(new HttpCookie("ImageUrl", imgUrl));
            //HttpCookie cookie = new HttpCookie("ImgCookie");
            //cookie.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(new HttpCookie("Date"));
            return View();
        }
    }
}