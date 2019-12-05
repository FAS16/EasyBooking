using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyBooking.Models.Domain;

namespace EasyBooking.Models
{
    public class BookingsViewModel
    {

        public List<Reservation> Reservations { get; set; }
        public List<WalkIn> WalkIns { get; set; }

//        public List<Booking> AllBookings { get; }

        public BookingsViewModel()
        {
            // LINQ EXTENSION

            if (this.Reservations != null)
            {
                this.Reservations = Reservations.OrderBy(x => x.Start).ToList();

            }

            if (this.WalkIns != null)
            {
                this.WalkIns = WalkIns.OrderBy(x => x.Start).ToList();

            }
            //            this.AllBookings = new List<Booking>();
            //            AllBookings.AddRange(Reservations);
            //            AllBookings.AddRange(WalkIns);
            //            AllBookings = AllBookings.OrderBy(x => x.Start).ToList();
        }
    }
}