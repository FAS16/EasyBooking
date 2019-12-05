using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyBooking.Models;

namespace EasyBooking.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Add()
        {

            var viewModel = new CustomerFormViewModel();
            return View("CustomerForm", viewModel);
        }

//        // POST: Customers/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PhoneNumber")] Customer customer)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Customers.Add(customer);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//
//            return View(customer);
//        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            Customer customer = await db.Customers.FindAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel(customer);

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
           
            var customerInDb = await db.Customers.FindAsync(id);
            if (customerInDb == null)
            {
                return HttpNotFound();
            }

            db.Customers.Remove(customerInDb);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

//        // POST: Customers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Customer customer = await db.Customers.FindAsync(id);
//            db.Customers.Remove(customer);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer);

                return View("CustomerForm", viewModel);

            }
            if (customer.Id == 0)
            {
                db.Customers.Add(customer);
            }
            else
            {
                var customerInDb = db.Customers.Single(m => m.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.PhoneNumber = customer.PhoneNumber;


            }

            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}
