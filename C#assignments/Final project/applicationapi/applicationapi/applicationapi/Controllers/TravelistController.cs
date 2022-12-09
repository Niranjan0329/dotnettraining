using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;

namespace applicationapi.Controllers
{
    public class TravelistController : ApiController
    {
        newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();

        
        public IHttpActionResult gettravel()
        {
            var customer = from cust in db.TravelRequests
                           where cust.CurrentStatus == "TicketConformed" || cust.CurrentStatus == "TicketnotConformed"
                           select cust;

            return Ok(customer.ToList());
            //return Ok();
        }
    }
}
