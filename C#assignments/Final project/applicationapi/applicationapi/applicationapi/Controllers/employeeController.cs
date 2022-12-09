using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;

namespace applicationapi.Controllers
{
    
    public class employeeController : ApiController
    {
        newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();

       
         public List<TravelRequest> GetTravelRequests()
         {
            
                return db.TravelRequests.ToList();
            
         }

        public IHttpActionResult Post(TravelRequest re)
        {
            try
            {
                var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "Insert");
                db.SaveChanges();
                return Ok(updateemprecord);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return Ok();

        }

      }
}
