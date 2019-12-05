using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EasyBooking.Models;

namespace EasyBooking.Controllers.Api
{
    public class TablesApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TablesApi
        public IQueryable<Table> GetTables()
        {
            return db.Tables;
        }

        // GET: api/TablesApi/5
        [ResponseType(typeof(Table))]
        public async Task<IHttpActionResult> GetTable(int id)
        {
            Table table = await db.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        // PUT: api/TablesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTable(int id, Table table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table.Id)
            {
                return BadRequest();
            }

            db.Entry(table).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TablesApi
        [ResponseType(typeof(Table))]
        public async Task<IHttpActionResult> PostTable(Table table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tables.Add(table);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = table.Id }, table);
        }

        // DELETE: api/TablesApi/5
        [ResponseType(typeof(Table))]
        public async Task<IHttpActionResult> DeleteTable(int id)
        {
            Table table = await db.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            db.Tables.Remove(table);
            await db.SaveChangesAsync();

            return Ok(table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableExists(int id)
        {
            return db.Tables.Count(e => e.Id == id) > 0;
        }
    }
}