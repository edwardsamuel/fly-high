using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyHigh.Models
{
    public class CheckListAttribute : ValidationAttribute
    {
        //private variables to set the validation lenght and whether the lenght is fixed or not. this is set on initialzation by the constructor.
        private int _length;
        private bool _fixed;

        public override bool IsValid(object value)
        {
            int l = 0;


            // retrieves the lenght of the collection by checking the collection type and casting to approperiate type to avoid compile or runtime error.
            if (value != null && typeof(List<long>) == value.GetType())
            {
                var v = (List<long>)value;
                l = v.Count;
            }
            else if (value != null)//string
            {
                var v = (List<string>)value;
                l = v.Count;
            }

            //return the vaidation result based on length of the List and the isFixed flag
            //value is check for nullable to handle the null value when nothing is checked.
            if (this._fixed)
            {
                return value != null && this._length == l;
            }
            else
            {
                return value != null && l >= this._length;
            }
        }


        /*
         length : this is the number of checkboxes that must be checked to pass the validation. Default: 1
         isFixed : this determins if the number of checked boxes is fixed to length. (true: it must be equal to lenght) (false: it must not be less than length) Default: false
         */
        public CheckListAttribute(int length = 1, bool isFixed = false)
        {

            this._length = length;
            this._fixed = isFixed;
        }

    }
}