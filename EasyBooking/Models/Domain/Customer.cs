using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBooking.Models
{
    public class Customer
    {

        public int Id { get; set; }

            [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Must only have alphabetic characters")]
            [Required(ErrorMessage = "Please enter customer's name")]
            [StringLength(255)]
            public string Name { get; set; }

            [Display(Name = "Country code")]
            public string CountryCode { get; set; }

            [Required(ErrorMessage = "Please enter customer's phone number")]
            [Display(Name = "Phone number")]
            [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number can only consist of numbers")]
            public string PhoneNumber { get; set; }
        

    }
}