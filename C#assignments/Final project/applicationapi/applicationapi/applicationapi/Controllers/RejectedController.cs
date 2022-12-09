using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;
namespace applicationapi.Controllers
{
    public class RejectedController : ApiController
    {
        newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();

        public IHttpActionResult getnumerious()
        {



            //return Ok(db.r.ToList()); 
            //var res = db.TravelRequests.ToList();
            //return Ok(res);
            var queryLondonCustomers = from cust in db.TravelRequests
                                       where cust.CurrentStatus=="Approved" 
                                       select cust;
            
            return Ok(queryLondonCustomers.ToList());
        }


        

       


        public IHttpActionResult put(TravelRequest re)
        {
            if (re.CurrentStatus == "Approved")
            {
                
                    var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "BookTicket");
                    db.SaveChanges();
                    return Ok(updateemprecord.FirstOrDefault());
                
               
            }
            else if (re.CurrentStatus == "Rejected")
            {
                var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "RejectTicket");
                db.SaveChanges();
                return Ok(updateemprecord.FirstOrDefault());
            }
            else
            {
                return Ok("alerday done");
            }
            //public IHttpActionResult Put(TravelRequest re)
            //{
            //    if (re.CurrentStatus == "Pending")
            //    {
            //        var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "Reject");
            //        db.SaveChanges();
            //        return Ok(updateemprecord);
            //    }
            //    //else if (re.CurrentStatus == "Rejected")
            //    //{
            //    //    var updateemprecord = db.sp_Storeprocuser(re.Requestid, re.RequestDate, re.FromLocation, re.ToLocation, re.UserId, re.CurrentStatus, "Reject");
            //    //    db.SaveChanges();
            //    //    return Ok(updateemprecord);
            //    //}
            //    else
            //    {
            //        return Ok("already done");
            //    }


        }
    }
}
