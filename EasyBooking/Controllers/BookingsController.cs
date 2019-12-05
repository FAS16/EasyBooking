using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EasyBooking.Migrations;
using EasyBooking.Models;
using EasyBooking.Models.Domain;
using DateTime = System.DateTime;
using Table = EasyBooking.Models.Table;

namespace EasyBooking.Controllers
{
    public class BookingsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private DateTime openingTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2,0,0);
        private DateTime closingTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22,0,0);
        

        public BookingsController()
        {
            
        }


        // GET: Bookings
        public async Task<ActionResult> Index()
        {

            

            var viewModel = new BookingsViewModel
            {
                Reservations = await db.Reservations.Include(x => x.Customer).Include(x => x.Table).ToListAsync(),
                WalkIns = await db.WalkIns.Include(x => x.Table).ToListAsync()
            };

            if (TempData["ArrivalError"] != null)
            {
                ModelState.AddModelError("", TempData["ArrivalError"].ToString());

            }
            return View(viewModel);
        }



        public async Task<ActionResult> NoteArrivalOfCustomer(int id)
        {
            var reservation = await db.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            if (DateTime.Now < reservation.Start)
            {
                TempData["ArrivalError"] = "You are noting the customer's arrival too early. It's not today!. Id = " + reservation.Id.ToString();

            }

            else if (DateTime.Now.Date > reservation.Start.Date)
            {
                TempData["ArrivalError"] = "This reservation is back in time. Id = " + reservation.Id.ToString();

            }

            else if (DateTime.Now.TimeOfDay < reservation.Start.TimeOfDay)
               
            {
                TempData["ArrivalError"] = "You are noting the customer's arrival too early! Id = " + reservation.Id.ToString();
            }
            
            else if (DateTime.Now.TimeOfDay >= reservation.End.TimeOfDay)
            {
                TempData["ArrivalError"] = "Late note. The time of the reservation was earlier. Id = " + reservation.Id.ToString();

            }

            else
            {
                reservation.Arrival = DateTime.Now;
                await db.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }

        public ActionResult AddReservation()
        {
            var tables = db.Tables.ToList();
            var viewModel = new ReservationFormViewModel();
            viewModel.Hours = GetTimeOptions();

            return View("ReservationForm", viewModel);
        }

        public ActionResult AddWalkIn()
        {
            var tables = db.Tables.ToList();
            var viewModel = new WalkInFormViewModel();
            viewModel.Hours = GetTimeOptions();



            return View("WalkInForm", viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnSubmitWalkInForm(string submit, WalkInFormViewModel walkInFormViewModel)
        {

            walkInFormViewModel.Hours = GetTimeOptions();
            var startDate = walkInFormViewModel.Start;
            var startTime = walkInFormViewModel.StartTime;
            var endTime = walkInFormViewModel.EndTime;
            var start = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Value.Hour, startTime.Value.Minute, startTime.Value.Second);
            var end = new DateTime(startDate.Year, startDate.Month, startDate.Day, endTime.Value.Hour, endTime.Value.Minute, endTime.Value.Second);
            // Check if reservation are being made back in time and return error
            if (InvalidDates(start, end))
            {
                return View("WalkinForm", walkInFormViewModel);

            }

            


            if (walkInFormViewModel.TableId == null || submit == "availb")
            {

                // Check availability has been pressed
                var tables = GetAvailableTables(start, end, walkInFormViewModel.Seats);
                walkInFormViewModel.AvailableTables = tables;

                if (tables != null)
                {
                    if (tables.Count == 0)
                    {
                        ModelState.AddModelError("", "No tables available for this request!");
                        walkInFormViewModel.AvailableTables = null;
                    }

                    // The user has been shown a list of available tables, but bas not chosen one.
                    if (walkInFormViewModel.TableId == null && submit == "saveb")
                    {
                        ModelState.AddModelError("", "Please select a table!");
                    }

                }



                //GET AVAILABLE TIMESs
                return View("WalkinForm", walkInFormViewModel);

            }

            if (!ModelState.IsValid)
            {

                return View("WalkInForm", walkInFormViewModel);

            }


            if (walkInFormViewModel.Id == 0)
            {

                //                walkInFormViewModel.End = walkInFormViewModel.Start.AddHours(1);

                var table = db.Tables.Single(t => t.Id == walkInFormViewModel.TableId);

                var walkIn = new WalkIn
                {
                    Created = DateTime.Now,
                    End = end,
                    Start = start,
                    Table = table

                };
                db.WalkIns.Add(walkIn);
            }
            else
            {
                var walkInInDb = db.WalkIns.Single(m => m.Id == walkInFormViewModel.Id);
                walkInInDb.Start = walkInFormViewModel.Start;
                walkInInDb.End = walkInFormViewModel.End;
                walkInInDb.Table.Id = walkInFormViewModel.TableId.Value;


            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnSubmitReservationForm(string submit, ReservationFormViewModel reservationViewModel)
        {

            

            reservationViewModel.Hours = GetTimeOptions();
            var startDate = reservationViewModel.Start;
            var startTime = reservationViewModel.StartTime;
            var endTime = reservationViewModel.EndTime;
            var start = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Value.Hour, startTime.Value.Minute, startTime.Value.Second);
            var end = new DateTime(startDate.Year, startDate.Month, startDate.Day, endTime.Value.Hour, endTime.Value.Minute, endTime.Value.Second);

            // Check if reservation are being made back in time and return error
            if (InvalidDates(start,end))
            {
                return View("ReservationForm", reservationViewModel);

            }


            if (reservationViewModel.TableId == null || submit == "availb")
            {
                
                // Check availability has been pressed
                var tables = GetAvailableTables(start, end, reservationViewModel.Seats);
                reservationViewModel.AvailableTables = tables;

                if (tables != null)
                {
                    if (tables.Count == 0)
                    {
                        ModelState.AddModelError("", "No tables available for this request!");
                        reservationViewModel.AvailableTables = null;
                    }

                    // The user has been shown a list of available tables, but bas not chosen one.
                    if (reservationViewModel.TableId == null && submit == "saveb")
                    {
                        ModelState.AddModelError("", "Please select a table!");
                    }

                }

                //GET AVAILABLE TIMESs
                return View("ReservationForm", reservationViewModel);

            }


            if (!ModelState.IsValid)
            {

                    return View("ReservationForm", reservationViewModel);

            }


            if (reservationViewModel.Id == 0)
            {


                    var customer = db.Customers
                        .FirstOrDefault(c =>
                            c.Name == reservationViewModel.Name && c.PhoneNumber == reservationViewModel.PhoneNumber);

                    if (customer == null) 
                    {
                        // New customer
                        //TODO toast
                        customer = new Customer
                        {
                            Name = reservationViewModel.Name,
                            CountryCode = reservationViewModel.CountryCode,
                            PhoneNumber = reservationViewModel.PhoneNumber
                        };
                        db.Customers.Add(customer);
                        await db.SaveChangesAsync();
                    }

                    var table = db.Tables.Single(t => t.Id == reservationViewModel.TableId);

                    var reservation = new Reservation
                    {
                        Customer = customer,
                        End = end,
                        Start = start,
                        Table = table,
                        Created = DateTime.Now


                    };
                    db.Reservations.Add(reservation);
            }
            else
            {
                // Edit reservation
                    var reservationInDb = db.Reservations.Single(m => m.Id == reservationViewModel.Id);
                    reservationInDb.Start = reservationViewModel.Start;
                    reservationInDb.Table.Id = reservationViewModel.TableId.Value;

            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            
        }

        public async Task<ActionResult> EditReservation(int id)
        {

            Reservation reservation =
                await db.Reservations.Include(x => x.Customer).Include(x => x.Table).SingleOrDefaultAsync(x => x.Id == id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ReservationFormViewModel(reservation);
            var tables = db.Tables.ToList();
//            viewModel.AvailableTables = db.Tables.ToList();


            viewModel.Hours = GetTimeOptions();
            

            return View("ReservationForm", viewModel);
        }

        public async Task<ActionResult> EditWalkin(int id)
        {

            var walkIn = await db.WalkIns.Include(x => x.Table).SingleOrDefaultAsync(x => x.Id == id);

            if (walkIn == null)
            {
                return HttpNotFound();
            }

            var viewModel = new WalkInFormViewModel(walkIn);
            var tables = db.Tables.ToList();
//            viewModel.AvailableTables = db.Tables.ToList();

            return View("WalkInForm", viewModel);

        }


        private List<Table> GetAvailableTables(DateTime start, DateTime end, int? seats)
        {

            // Get All tables
            var allTables = db.Tables.Where(x => x.Seats == seats).ToList();

            // Get All bookings that matches the requested time ie. booked tables
            var reservations = from r in db.Reservations
                where ((r.Start >= start && r.Start <= end) || (r.End >= start && r.End <= end) ||(r.Start < start && r.End > end))  && r.Table.Seats == seats
                select r;

            // Get All bookings that matches the requested time ie. booked tables
            var walkIns = from r in db.WalkIns
                where ((r.Start >= start && r.Start <= end) || (r.End >= start && r.End <= end) || (r.Start < start && r.End > end)) && r.Table.Seats == seats
                select r;

           var aa= reservations.ToList();
            var bb = walkIns.ToList();


            // Get the id of the tables from bookings
            var idsOfBookedTables= reservations.Select(b => b.Table.Id).ToList();

            // Get the id of the tables from bookings
            var idsOfWalkInTables = walkIns.Select(b => b.Table.Id).ToList();

            idsOfBookedTables.AddRange(idsOfWalkInTables);

            // Extract tables that are not booked at the requested time
            var availableTables = allTables.Where(x => !idsOfBookedTables.Contains(x.Id));


            return availableTables.ToList();

        }

        private List<Table> GetAvailableTablesDeprecated(DateTime start, DateTime end, int? seats)
        {

            // Get All tables
            var allTables = db.Tables.Where(x => x.Seats == seats).ToList();

            // Get All bookings that matches the requested time ie. booked tables
            var reservations = from r in db.Reservations
                where ((r.Start >= start && r.Start <= end) || (r.End >= start && r.End <= end) || (r.Start < start && r.End > end))
                select r;


            var aa = reservations.ToList();

            // Get the id of the tables from bookings
            var idsOfBookedTables = reservations.Select(b => b.Table.Id).ToList();


            // Extract tables that are not booked at the requested time
            var availableTables = allTables.Where(x => !idsOfBookedTables.Contains(x.Id));


            return availableTables.ToList();

        }

        // Generate the list of available booking times based on opening hours
        private List<SelectListItem> GetTimeOptions()
        {
            List<DateTime> timings = new List<DateTime>();

            var hours = (closingTime - openingTime).TotalHours;
            var time = openingTime;
            int hour = time.Hour;
            int minute = 0;
            for (int i = 0; i <= (hours * 2); i++)
            {
                timings.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0));

                // If minute is 30 we should add one hour to time
                if (minute == 30)
                {
                    time = time.AddHours(1);
                    hour = time.Hour;
                    minute = 0;

                }


                // Every second iteration we should add 30 minutes 
                if (i % 2 == 0)
                {
                    //i is even

                    minute = 30;


                }


            }

            var selectList = timings.Select(s => new SelectListItem
            {
                Value = s.ToString(),
                Text = s.ToShortTimeString()
            }).ToList();

            return selectList;
        }

        public bool InvalidDates(DateTime start, DateTime end)
        {
            if (start > end ||start == end)
            {

                ModelState.AddModelError("", "Start time cannot be after or equal to End time");

                return true;

            }

            if (start < DateTime.Now || end < DateTime.Now)
            {
                ModelState.AddModelError("", "Date and timings cannot be back in time");


            }


            return false;
        }
    }
}