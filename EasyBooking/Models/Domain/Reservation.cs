using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBooking.Models.Domain
{
    public class Reservation : Booking
    {

        [Required]
        public Customer Customer { get; set; }
        public DateTime? Arrival { get; set; }


        public Reservation()
        {


            
        }
    }
}