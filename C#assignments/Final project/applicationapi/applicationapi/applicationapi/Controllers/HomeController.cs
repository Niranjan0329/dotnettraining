using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using applicationapi.Models;
using System.Net.Http;

namespace applicationapi.Controllers
{
    public class HomeController : Controller
    {
        //newtravelbookingsystemEntities db = new newtravelbookingsystemEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

       
       
    }
}
