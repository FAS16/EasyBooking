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
    public class TablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tables
        public async Task<ActionResult> Index()
        {
            return View(await db.Tables.ToListAsync());
        }

        // GET: Tables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = await db.Tables.FindAsync(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Add()
        {

            var viewModel = new TableFormViewModel();
            return View("TableForm", viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {

            Table table = await db.Tables.FindAsync(id);

            if (table == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TableFormViewModel(table);

            return View("TableForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PhoneNumber")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {

            var tableInDb = await db.Tables.FindAsync(id);
            if (tableInDb == null)
            {
                return HttpNotFound();
            }

            db.Tables.Remove(tableInDb);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        //        // POST: Tables/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<ActionResult> DeleteConfirmed(int id)
        //        {
        //            Table table = await db.Tables.FindAsync(id);
        //            db.Tables.Remove(table);
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
        public ActionResult Save(Table table)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new TableFormViewModel(table);

                return View("TableForm", viewModel);

            }
            if (table.Id == 0)
            {
                db.Tables.Add(table);
            }
            else
            {
                var tableInDb = db.Tables.Single(m => m.Id == table.Id);
                tableInDb.Seats = table.Seats;


            }

            db.SaveChanges();
            return RedirectToAction("Index", "Tables");
        }
    }
}
