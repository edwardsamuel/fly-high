using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyHigh.Models
{
    public class CheckInModel
    {/*Use the attribute to validate the values posted from the form
        1st arguement is the minimum number of chcked boxes. 1 in this case
        2nd arguement is the declear if the length if fixed. false in this case
        3rd is the error message to display when validation fails
         * Note: the posted values are converted to List<String> as one property because they all have same input name; 'DynamicMultiBoxes'.
        [CheckList(2, true, ErrorMessage = "Must be 2 selection!")]
         
      */
        [Display(Name = "Dynamic Multiple Checkboxes")]
        public List<long> DynamicMultiBoxes { get; set; }

        //This Mentains the state of teh DynamicMultiBoxes List
        //public string dChecked(long input)
        //{
        //    string ck = "";
        //    if (this.DynamicMultiBoxes != null && this.DynamicMultiBoxes.Count > 0)
        //    {
        //        if (this.DynamicMultiBoxes.Contains(input))
        //        {
        //            ck = "checked=\"checked\"";
        //        }
        //    }
        //    return ck;
        //}

        public Booking booking { get; set; }

    }

}