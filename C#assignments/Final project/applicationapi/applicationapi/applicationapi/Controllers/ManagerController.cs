using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;

namespace applicationapi.Controllers
{
    public class ManagerController : ApiController
    {
        //newtravelbookingsystem db = new newtravelbookingsystem();
        newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();

        public IHttpActionResult getmethod()
        {
            
            var queryLondonCustomers = from cust in db.TravelRequests
                                       where cust.CurrentStatus == "Pending"
                                       select cust;
            return Ok(queryLondonCustomers);
        }

        public IHttpActionResult getempid(int id)
        {
            TravelRequest request = db.TravelRequests.SingleOrDefault(m => m.Requestid == id );
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);

        }

       
        public IHttpActionResult Put(TravelRequest re)
        {
            if (re.CurrentStatus == "Pending" )
            {
                
                    var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "Pending");
                    db.SaveChanges();
                    return Ok(updateemprecord);
                
               
            }
            else if (re.CurrentStatus == "Rejected")
            {
                var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "Reject");
                db.SaveChanges();
                return Ok(updateemprecord);
            }

            else
            {
                return Ok("already done");
            }

            
        }
    }
}
