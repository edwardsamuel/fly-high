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
    
    public partial class Ticket
    {
        public Ticket()
        {
            this.Baggages = new HashSet<Baggage>();
            this.TicketInstances = new HashSet<TicketInstance>();
        }
    
        public long TicketId { get; set; }
        public long BookingId { get; set; }
        public string TicketFirstName { get; set; }
        public string TicketLastName { get; set; }
        public string TicketIdType { get; set; }
        public string TicketIdNumber { get; set; }
        public Nullable<int> MemberId { get; set; }
        public decimal Price { get; set; }
        public decimal BaggageCharge { get; set; }
        public bool IsInfant { get; set; }
        public Nullable<System.DateTime> CheckIn { get; set; }
        public string SeatNumber { get; set; }
    
        public virtual ICollection<Baggage> Baggages { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<TicketInstance> TicketInstances { get; set; }
    }
}
