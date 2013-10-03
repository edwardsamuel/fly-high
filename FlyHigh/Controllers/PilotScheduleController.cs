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
    public class PilotScheduleController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        // Begin Ikmal
        public List<Schedule> GetSchedule()
        {
            var x = db.Schedules;
            return x.ToList();
        }

        public ActionResult GetPilots(int id = 0)
        {
            //select pilotname
            //from pilot left join pilotschedule
            //on pilot.pilotid = pilotschedule.pilotid
            //where
            //scheduleid=8
            var pilots = db.Pilots;
            var pilotschedules = db.PilotSchedules;

            var result =
                from pilot in pilots
                join pilotschedule in pilotschedules.Where(x => x.ScheduleId == id)
                on pilot.PilotId equals pilotschedule.PilotId into res
                where !res.Any()
                select new
                {
                    pilot.PilotName,
                    pilot.PilotId
                };

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            //ViewBag.PilotId = new SelectList(db.Pilots, "PilotId", "PilotName");
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId");
            SelectList s = new SelectList(db.Pilots, "PilotId", "PilotName");
            return View();

        }
        // End Ikmal

        //
        // GET: /PilotSchedule/

        public ActionResult Index()
        {
            var pilotschedules = db.PilotSchedules.Include(p => p.Pilot).Include(p => p.Schedule);
            return View(pilotschedules.ToList());
        }

        //
        // GET: /PilotSchedule/Details/5

        public ActionResult Details(int id = 0)
        {
            PilotSchedule pilotschedule = db.PilotSchedules.Find(id);
            if (pilotschedule == null)
            {
                return HttpNotFound();
            }
            return View(pilotschedule);
        }

        //
        // GET: /PilotSchedule/Create

        //public ActionResult Create()
        //{
        //    ViewBag.PilotId = new SelectList(db.Pilots, "PilotId", "PilotName");
        //    ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId");
        //    SelectList s = new SelectList(db.Pilots, "PilotId", "PilotName");
        //    return View();

        //}

        //
        // POST: /PilotSchedule/Create

        [HttpPost]
        public ActionResult Create(PilotSchedule pilotschedule)
        {
            if (ModelState.IsValid)
            {
                db.PilotSchedules.Add(pilotschedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PilotId = new SelectList(db.Pilots, "PilotId", "PilotName", pilotschedule.PilotId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", pilotschedule.ScheduleId);
            return View(pilotschedule);
        }

        //
        // GET: /PilotSchedule/Edit/5

        public ActionResult Edit(int pilotId = 0, int scheduleId = 0)
        {
            PilotSchedule pilotschedule = db.PilotSchedules.Where(ps => ps.PilotId == pilotId && ps.ScheduleId == scheduleId).FirstOrDefault();
            if (pilotschedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.PilotId = new SelectList(db.Pilots, "PilotId", "PilotName", pilotschedule.PilotId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", pilotschedule.ScheduleId);
            return View(pilotschedule);
        }

        //
        // POST: /PilotSchedule/Edit/5

        [HttpPost]
        public ActionResult Edit(PilotSchedule pilotschedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pilotschedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PilotId = new SelectList(db.Pilots, "PilotId", "PilotName", pilotschedule.PilotId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", pilotschedule.ScheduleId);
            return View(pilotschedule);
        }

        //
        // GET: /PilotSchedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PilotSchedule pilotschedule = db.PilotSchedules.Find(id);
            if (pilotschedule == null)
            {
                return HttpNotFound();
            }
            return View(pilotschedule);
        }

        //
        // POST: /PilotSchedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PilotSchedule pilotschedule = db.PilotSchedules.Find(id);
            db.PilotSchedules.Remove(pilotschedule);
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