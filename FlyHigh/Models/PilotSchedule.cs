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
    using System.ComponentModel.DataAnnotations;
    
    public partial class PilotSchedule
    {
        public int PilotScheduleId { get; set; }
        public int PilotId { get; set; }
        public long ScheduleId { get; set; }

        [Display(Name = "Pilot Captain")]
        public bool IsHead { get; set; }
    
        public virtual Pilot Pilot { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
