using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace applicationmvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Authenticate");
        }

        public ActionResult About()
        {
            ViewBag.Message = "About our Employee Travel booking "; 

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "The way to contact us.";

            return View();
        }

        public ActionResult Logout()
        {

            return View("Index");
        }
    }
}