using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBooking.Models
{
    public class Table
    {

        public int Id { get; set; }

        [Required(ErrorMessage = ("Please select number of guests."))]


        public int Seats { get; set; }


        public Table()
        {
        }

        
    }
}