using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using applicationapi.Models;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace applicationapi.Controllers
{
    public class AdminController : ApiController
    {
        newtravelbookingsystemEntities db1 = new newtravelbookingsystemEntities();


        public IHttpActionResult GetTravelRequest(int id)
        {
            User user = db1.Users.SingleOrDefault(m => m.User_id == id);
            try
            {
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return Ok(user);
        }

        public IHttpActionResult getuser()
        {
            return Ok(db1.Users.ToList());
        }
        public IHttpActionResult PutTravelRequest(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_id)
            {
                return BadRequest();
            }

            db1.Entry(user).State = EntityState.Modified;

            try
            {
                db1.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelRequestExists(id))
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

        
        public IHttpActionResult Postuser(User user)
        {
            try
            {
                db1.Users.Add(user);
                db1.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return CreatedAtRoute("DefaultApi", new { id = user.User_id }, user);
        }

      
        public IHttpActionResult DeleteTravelRequest(int id)
        {
            User user= db1.Users.Find(id);
            try
            {
                if (user == null)
                {
                    return NotFound();
                }

                db1.Users.Remove(user);
                db1.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db1.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TravelRequestExists(int id)
        {
            return db1.Users.Count(e => e.User_type_id== id) > 0;
        }
    }
}

