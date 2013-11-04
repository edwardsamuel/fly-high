using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyHigh.Models;
using System.Data.Objects;

namespace FlyHigh.Controllers
{
    [Authorize(Roles = "Scheduler")]
    public class ParkingController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /Parking/
        
        public ActionResult Index()
        {
            ViewData.Add("PlaneId", new SelectList(db.Planes, "PlaneId", "PlaneId"));
            return View();
        }

        //
        // GET: /Parking/Details?start=2013-12-30&end=2013-12-30

        public ActionResult Details(DateTime start, DateTime end)
        {
            List<ParkingModel> parking = new List<ParkingModel>();

            var q = from p in db.Planes select p.PlaneId;
            foreach (int planeId in q.ToList())
            {
                var scheduleBefore = db.Schedules.Include(s => s.Flight).Include(s => s.Plane).OrderByDescending(p => p.Date).OrderByDescending(p => p.Flight.Departure).FirstOrDefault(p => p.PlaneId == planeId && p.Date < start);
                var scheduleIn = db.Schedules.Include(s => s.Flight).Include(s => s.Plane).Where(p => p.PlaneId == planeId && p.Date >= start && p.Date <= end).OrderBy(s => s.Date).ThenBy(s => s.Flight.Departure).ToList();
                var scheduleAfter = db.Schedules.Include(s => s.Flight).Include(s => s.Plane).FirstOrDefault(p => p.PlaneId == planeId && p.Date > end);
                
                ParkingModel pm = new ParkingModel(scheduleBefore, null);
                if (scheduleIn.Count > 0)
                {
                    pm.Departure = scheduleIn[0];
                    parking.Add(pm);

                    for (int i = 0, len = scheduleIn.Count - 1; i < len; i++)
                    {
                        pm = new ParkingModel(scheduleIn[i], scheduleIn[i + 1]);
                        parking.Add(pm);
                    }

                    pm = new ParkingModel(scheduleIn[scheduleIn.Count - 1], scheduleAfter);
                    parking.Add(pm);
                }
                else
                {
                    pm.Departure = scheduleAfter;
                    parking.Add(pm);
                }
            }
            return View(parking);
        }
    }
}
