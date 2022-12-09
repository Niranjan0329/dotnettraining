using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;

namespace applicationapi.Controllers
{
    public class ManagerRepotController : ApiController
    {
        newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();
        public IHttpActionResult getEditing()
        {
            var customer = from cust in db.TravelRequests
                           where cust.CurrentStatus == "TicketConformed" || cust.CurrentStatus == "TicketnotConformed" || cust.CurrentStatus == "Pending" || cust.CurrentStatus == "Approved" || cust.CurrentStatus == "Rejected"
                           select cust;

            return Ok(customer.ToList());
            //    IEnumerable<employeelist_Result> result = db.employeelist();
            //    return Ok(result);
        }
    }
}
