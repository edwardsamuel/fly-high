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
    public class StewardScheduleController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        public ActionResult GetStewards(int id = 0)
        {
            //select stewardname
            //from steward left join stewardschedule
            //on steward.stewardid = stewardschedule.stewardid
            //and scheduleid=8
            //where scheduleid is null

            var stewards = db.Stewards;
            var stewardschedules = db.StewardSchedules;

            var result =
                from steward in stewards
                join stewardschedule in stewardschedules.Where(x => x.ScheduleId == id)
                on steward.StewardId equals stewardschedule.StewardId into res
                where !res.Any()
                select new
                {
                    steward.StewardName,
                    steward.StewardId
                };

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /StewardSchedule/

        public ActionResult Index()
        {
            var stewardschedules = db.StewardSchedules.Include(s => s.Schedule).Include(s => s.Steward);
            return View(stewardschedules.ToList());
        }

        //
        // GET: /StewardSchedule/Details/5

        public ActionResult Details(int id = 0)
        {
            StewardSchedule stewardschedule = db.StewardSchedules.Find(id);
            if (stewardschedule == null)
            {
                return HttpNotFound();
            }
            return View(stewardschedule);
        }

        //
        // GET: /StewardSchedule/Create

        public ActionResult Create()
        {
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId");
            ViewBag.StewardName = new SelectList(db.Stewards, "StewardId", "StewardName");
            return View();
        }

        //
        // POST: /StewardSchedule/Create

        [HttpPost]
        public ActionResult Create(StewardSchedule stewardschedule)
        {
            if (ModelState.IsValid)
            {
                db.StewardSchedules.Add(stewardschedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", stewardschedule.ScheduleId);
            ViewBag.StewardId = new SelectList(db.Stewards, "StewardId", "StewardName", stewardschedule.StewardId);
            return View(stewardschedule);
        }

        //
        // GET: /StewardSchedule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StewardSchedule stewardschedule = db.StewardSchedules.Find(id);
            if (stewardschedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", stewardschedule.ScheduleId);
            ViewBag.StewardId = new SelectList(db.Stewards, "StewardId", "StewardName", stewardschedule.StewardId);
            return View(stewardschedule);
        }

        //
        // POST: /StewardSchedule/Edit/5

        [HttpPost]
        public ActionResult Edit(StewardSchedule stewardschedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stewardschedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", stewardschedule.ScheduleId);
            ViewBag.StewardId = new SelectList(db.Stewards, "StewardId", "StewardName", stewardschedule.StewardId);
            return View(stewardschedule);
        }

        //
        // GET: /StewardSchedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StewardSchedule stewardschedule = db.StewardSchedules.Find(id);
            if (stewardschedule == null)
            {
                return HttpNotFound();
            }
            return View(stewardschedule);
        }

        //
        // POST: /StewardSchedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StewardSchedule stewardschedule = db.StewardSchedules.Find(id);
            db.StewardSchedules.Remove(stewardschedule);
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