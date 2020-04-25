using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSchools.Models;

namespace WebSchools.Controllers
{
    public class PriorExperiencesController : Controller
    {
        private WebSchoolsDbContext db = new WebSchoolsDbContext();

        // GET: PriorExperiences
        public ActionResult Index()
        {
            return View(db.PriorExperiences.ToList());
        }

        // GET: PriorExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriorExperience priorExperience = db.PriorExperiences.Find(id);
            if (priorExperience == null)
            {
                return HttpNotFound();
            }
            return View(priorExperience);
        }

        // GET: PriorExperiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriorExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,PhoneNumber,ProjectIdea,ProjectCategory,Problem")] PriorExperience priorExperience)
        {
            if (ModelState.IsValid)
            {
                db.PriorExperiences.Add(priorExperience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(priorExperience);
        }

        // GET: PriorExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriorExperience priorExperience = db.PriorExperiences.Find(id);
            if (priorExperience == null)
            {
                return HttpNotFound();
            }
            return View(priorExperience);
        }

        // POST: PriorExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,PhoneNumber,ProjectIdea,ProjectCategory,Problem")] PriorExperience priorExperience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priorExperience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(priorExperience);
        }

        // GET: PriorExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriorExperience priorExperience = db.PriorExperiences.Find(id);
            if (priorExperience == null)
            {
                return HttpNotFound();
            }
            return View(priorExperience);
        }

        // POST: PriorExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PriorExperience priorExperience = db.PriorExperiences.Find(id);
            db.PriorExperiences.Remove(priorExperience);
            db.SaveChanges();
            return RedirectToAction("Index");
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
