using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyBooking.Models.Domain;

namespace EasyBooking.Models
{
    public class WalkInFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter start of walkInFormViewModel")]
        //DateTime Regex
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Please enter end of walkInFormViewModel")]
        //DateTime Regex
        public DateTime End;
        [Required(ErrorMessage = "Please select start time")]
        [Display(Name = "Start time")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Please select end time")]
        [Display(Name = "End time")]
        public DateTime? EndTime { get; set; }


        [Display(Name = "Table number")]

        public int? TableId { get; set; }

        [Required(ErrorMessage = ("Please select number of guests."))]

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal to {1}")]

        public int? Seats { get; set; }


        [Display(Name = "Table")]
        public Table TablePlaceHolder { get; set; }

        public List<Table> AvailableTables { get; set; }
        public List<SelectListItem> Hours { get; set; }


        public WalkInFormViewModel()
        {
            Start = DateTime.Now;
        }


        public WalkInFormViewModel(WalkIn walkIn)
        {
            Id = walkIn.Id;
            Start = walkIn.Start;
            End = walkIn.End;
            TableId = walkIn.Table.Id;
            Seats = walkIn.Table.Seats;
            Hours = new List<SelectListItem>();





        }

        public string Title => Id != 0 ? "Edit walk-in" : "Add walk-in";


    }
}