using FlyHigh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace FlyHigh.Controllers
{
    public class CheckInController : Controller
    {
        private ErlanggaEntities db = new ErlanggaEntities();

        //
        // GET: /CheckIn/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CheckIn/CheckIn/5

        public ActionResult CheckIn(int id = 0)
        {
            var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == id).FirstOrDefault();
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
    }
}
