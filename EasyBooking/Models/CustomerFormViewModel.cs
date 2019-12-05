using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBooking.Models
{
    public class CustomerFormViewModel
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
        public string Title => Id != 0 ? "Customer details" : "Add customer";


        public CustomerFormViewModel()
        {
            // To make sure that the hidden field i populated
            Id = 0;
        }
        public CustomerFormViewModel(Customer customer)
        {

            Id = customer.Id;
            Name = customer.Name;
            CountryCode = customer.CountryCode;
            PhoneNumber = customer.PhoneNumber;

        }
    }
}