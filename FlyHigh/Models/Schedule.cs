//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlyHigh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public Schedule()
        {
            this.PilotSchedules = new HashSet<PilotSchedule>();
            this.StewardSchedules = new HashSet<StewardSchedule>();
            this.TicketInstances = new HashSet<TicketInstance>();
        }
    
        public long ScheduleId { get; set; }
        public int FlightId { get; set; }
        public System.DateTime Date { get; set; }
        public int PlaneId { get; set; }
    
        public System.DateTime DepartureTime
        {
            get
            {
                return new DateTime(Date.Year, Date.Month, Date.Day, Flight.Departure.Hours, Flight.Departure.Minutes,
                    Flight.Departure.Seconds);
            }
        }

        public System.DateTime ArrivalTime
        {
            get
            {
                DateTime d = DepartureTime.Add(TimeSpan.FromHours(Flight.Duration));
                return d;
            }
        }

        public TimeSpan ParkingDuration
        {
            get
            {
                return (ArrivalTime - DepartureTime);
            }
        }

        public virtual Flight Flight { get; set; }
        public virtual ICollection<PilotSchedule> PilotSchedules { get; set; }
        public virtual Plane Plane { get; set; }
        public virtual ICollection<StewardSchedule> StewardSchedules { get; set; }
        public virtual ICollection<TicketInstance> TicketInstances { get; set; }
    }
}
