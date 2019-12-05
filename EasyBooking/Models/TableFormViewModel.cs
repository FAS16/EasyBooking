using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBooking.Models
{
    public class TableFormViewModel
    {



        public int Id { get; set; }

        [Required]
        public int Seats { get; set; }

        public TableFormViewModel()
        {
            Id = 0;
        }

        public string Title => Id != 0 ? "Edit table" : "Add table";


        public TableFormViewModel(Table table)
        {
            Id = table.Id;
            Seats = table.Seats;
        }
    }
}