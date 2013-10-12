using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyHigh.Models;

namespace FlyHigh.Controllers
{
    public class StewardController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /Steward/

        public ActionResult Index()
        {
            return View(db.Stewards.ToList());
        }

        //
        // GET: /Steward/Details/5

        public ActionResult Details(int id = 0)
        {
            Steward steward = db.Stewards.Find(id);
            if (steward == null)
            {
                return HttpNotFound();
            }
            return View(steward);
        }

        //
        // GET: /Steward/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Steward/Create

        [HttpPost]
        public ActionResult Create(Steward steward)
        {
            if (ModelState.IsValid)
            {
                db.Stewards.Add(steward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(steward);
        }

        //
        // GET: /Steward/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Steward steward = db.Stewards.Find(id);
            if (steward == null)
            {
                return HttpNotFound();
            }
            return View(steward);
        }

        //
        // POST: /Steward/Edit/5

        [HttpPost]
        public ActionResult Edit(Steward steward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(steward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(steward);
        }

        //
        // GET: /Steward/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Steward steward = db.Stewards.Find(id);
            if (steward == null)
            {
                return HttpNotFound();
            }
            return View(steward);
        }

        //
        // POST: /Steward/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Steward steward = db.Stewards.Find(id);
            db.Stewards.Remove(steward);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}