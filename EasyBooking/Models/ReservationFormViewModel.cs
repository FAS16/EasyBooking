using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using EasyBooking.Models.Domain;

namespace EasyBooking.Models
{
    public class ReservationFormViewModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter start of reservation")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date")]
        public DateTime Start { get; set; }


        [Required(ErrorMessage = "Please select start time")]
        [Display(Name = "Start time")]
        public DateTime? StartTime { get; set; }        
        
        [Required(ErrorMessage = "Please select end time")]
        [Display(Name = "End time")]
        public DateTime? EndTime { get; set; }


        [Required(ErrorMessage = "Please enter end of reservation")]
        public DateTime End;

        [Display(Name="Table number")]
        public int? TableId { get; set; }

        [Required(ErrorMessage = ("Please select number of guests."))]

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal to {1}")]

        public int? Seats { get; set; }


        [Display(Name = "Table")]
        public Table TablePlaceHolder { get; set; }

        
        public DateTime Arrival { get; set; }


        // Customer data
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

        public ReservationFormViewModel()
        {
            Start = DateTime.Now;
            Hours = new List<SelectListItem>();
            

        }

        public ReservationFormViewModel(Reservation reservation)
        {
            Id = reservation.Id;
            Start = reservation.Start;
            End = reservation.End;
            TableId = reservation.Table.Id;
            Seats = reservation.Table.Seats;
            Name = reservation.Customer.Name;
            CountryCode = reservation.Customer.CountryCode;
            PhoneNumber = reservation.Customer.PhoneNumber;
            Hours = new List<SelectListItem>();


        }
        public string Title => Id != 0 ? "Edit reservation" : "Add reservation";

        public List<Table> AvailableTables { get; set; }
        public List<SelectListItem> Hours { get; set; }



    }
}