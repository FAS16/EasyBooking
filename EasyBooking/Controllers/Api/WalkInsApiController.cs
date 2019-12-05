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
using EasyBooking.Models.Domain;

namespace EasyBooking.Controllers.Api
{
    public class WalkInsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WalkInsApi
        public IQueryable<WalkIn> GetWalkIns()
        {
            return db.WalkIns;
        }

        // GET: api/WalkInsApi/5
        [ResponseType(typeof(WalkIn))]
        public async Task<IHttpActionResult> GetWalkIn(int id)
        {
            WalkIn walkIn = await db.WalkIns.FindAsync(id);
            if (walkIn == null)
            {
                return NotFound();
            }

            return Ok(walkIn);
        }

        // PUT: api/WalkInsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWalkIn(int id, WalkIn walkIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != walkIn.Id)
            {
                return BadRequest();
            }

            db.Entry(walkIn).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalkInExists(id))
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

        // POST: api/WalkInsApi
        [ResponseType(typeof(WalkIn))]
        public async Task<IHttpActionResult> PostWalkIn(WalkIn walkIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WalkIns.Add(walkIn);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = walkIn.Id }, walkIn);
        }

        // DELETE: api/WalkInsApi/5
        [ResponseType(typeof(WalkIn))]
        public async Task<IHttpActionResult> DeleteWalkIn(int id)
        {
            WalkIn walkIn = await db.WalkIns.FindAsync(id);
            if (walkIn == null)
            {
                return NotFound();
            }

            db.WalkIns.Remove(walkIn);
            await db.SaveChangesAsync();

            return Ok(walkIn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WalkInExists(int id)
        {
            return db.WalkIns.Count(e => e.Id == id) > 0;
        }
    }
}