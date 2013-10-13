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
    [Authorize(Roles = "Teller")]
    public class CheckInController : Controller
    {
        private static double BAGGAGE_CHARGE_MULTIPLIER = 50000;

        private ErlanggaEntities db = new ErlanggaEntities();

        [HttpPost]
        public ActionResult Index(int id = 0)
        {
            var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == id).FirstOrDefault();

            CheckInModel fm = new CheckInModel();
            fm.booking = booking;

            if (booking == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("CheckIn", fm);
            }
        }

        //
        // GET: /CheckIn/

        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CheckIn(CheckInModel model)
        {
            if (model.DynamicMultiBoxes != null)
            {
                foreach (var i in model.DynamicMultiBoxes)
                {
                    var ticketToUpdate = db.Tickets.Where(ps => ps.TicketId == i).Single();
                    ticketToUpdate.CheckIn = DateTime.Now;
                    db.Entry(ticketToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            var tickets = db.Tickets.Include(ps => ps.Booking).Where(ps => ps.BookingId == model.booking.BookingId && ps.CheckIn != null).ToList();
            var ticketId = tickets.ElementAt(0).TicketId;
            TicketInstance ti = db.TicketInstances.Where(ps => ps.TicketId == ticketId).FirstOrDefault();
            PlaneClass pc = db.PlaneClasses.Where(ps => ps.ClassId == ti.ClassId).FirstOrDefault();


            ViewBag.maxWeight = pc.FreeBaggage;
            return View("Baggage", tickets);
        }

        //
        // GET: /CheckIn/AddBaggage/5

        public ActionResult AddBaggage(int id = 0)
        {
            Baggage baggage = new Baggage() { TicketId = id };
            return View("AddBaggage", baggage);
        }

        //
        // POST: /CheckIn/AddBaggage/5

        [HttpPost]
        public ActionResult AddBaggage(Baggage baggageToAdd)
        {
            if (ModelState.IsValid)
            {
                db.Baggages.Add(baggageToAdd);
                db.SaveChanges();

                double totalWeight = 0;

                long ticketId = baggageToAdd.TicketId;
            TicketInstance ti = db.TicketInstances.Where(ps => ps.TicketId == ticketId).FirstOrDefault();
            PlaneClass pc = db.PlaneClasses.Where(ps => ps.ClassId == ti.ClassId).FirstOrDefault();

                var baggages = db.Baggages.Where(ps => ps.TicketId == baggageToAdd.TicketId).ToList();
                foreach (var baggage in baggages)
                {
                    totalWeight += baggage.Weight;
                }

                double maxWeight = (double) pc.FreeBaggage;
                ViewBag.maxWeight = maxWeight;

                if (totalWeight > maxWeight)
                {
                    var ticketToUpdate = db.Tickets.Where(ps => ps.TicketId == baggageToAdd.TicketId).Single();
                    ticketToUpdate.BaggageCharge = (decimal)((totalWeight - maxWeight) * BAGGAGE_CHARGE_MULTIPLIER);
                    db.Entry(ticketToUpdate).State = EntityState.Modified;
                }

                db.SaveChanges();


                var bookingId = db.Tickets.Where(ps => ps.TicketId == baggageToAdd.TicketId).FirstOrDefault().BookingId;

                var tickets = db.Tickets.Include(ps => ps.Booking).Where(ps => ps.BookingId == bookingId && ps.CheckIn != null).ToList();

                //var booking = db.Bookings.Include(ps => ps.Tickets).Where(ps => ps.BookingId == bookingId).FirstOrDefault();

                //List<Ticket> tickets = new List<Ticket>();

                //foreach (var ticket in booking.Tickets)
                //{
                //    if (ticket.CheckIn != null)
                //    {
                //        tickets.Add(ticket);
                //    }
                //}
                //var tickets = db.Tickets.Include(ps => ps.Booking).Where(ps => ps.BookingId == baggageToAdd.Ticket.BookingId && ps.CheckIn != null).ToList()
                //var tickets2 = db.Tickets.Include(ps => ps.Booking);
                //var tickets3 = tickets2.Where(ps => ps.BookingId == baggageToAdd.Ticket.BookingId && ps.CheckIn != null).ToList();
                //var tickets = tickets3.ToList();

                return View("Baggage", tickets);
            }
            //ViewBag.FromAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.FromAirportId);
            //ViewBag.ToAirportId = new SelectList(db.Airports, "AirportId", "AirportCode", flight.ToAirportId);
            return View();
        }

        [HttpPost]
        public ActionResult Success(Baggage baggageToAdd)
        {
            return View("Success");
        }
    }
}
