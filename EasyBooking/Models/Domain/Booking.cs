using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace EasyBooking.Models.Domain
{
    public class Booking
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter start of reservation")]
        //DateTime Regex
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Please enter end of reservation")]
        //DateTime Regex

        public DateTime End { get; set; }
        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Please choose table")]

        public Table Table { get; set; }

        public string Note { get; set; }

        public Booking()
        {
        }



    }
}