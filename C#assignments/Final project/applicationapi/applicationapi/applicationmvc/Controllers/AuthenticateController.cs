using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using applicationmvc.Models;
using Newtonsoft.Json;

namespace applicationmvc.Controllers
{
    public class AuthenticateController : Controller
    {
        // GET: Authenticate
        DbContext dbContext = new DbContext();

        // GET: Authenticate
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User1 user)
        {

            try
            {

                var us1 = dbContext.GetUser();

                var us = us1.Where(a => a.Login_Id == user.Login_Id && a.Password == user.Password);

                if (us.Count() > 0 && us.FirstOrDefault().User_type_id == 1)
                {
                    return RedirectToAction("Display", "AdminMvc");
                }
                if (us.Count() > 0 && us.FirstOrDefault().User_type_id == 2)
                {
                    return RedirectToAction("List", "Travel", new { userTypeId = us.FirstOrDefault().User_type_id.ToString() });
                }
                if (us.Count() > 0 && us.FirstOrDefault().User_type_id == 3)
                {

                    return RedirectToAction("Index", "ManagerMvc", new { userTypeId = us.FirstOrDefault().User_type_id.ToString() });
                }
                if (us.Count() > 0 && us.FirstOrDefault().User_type_id == 4)
                {

                    return RedirectToAction("Index", "Travel", new { usertTypeId = us.FirstOrDefault().User_type_id.ToString() });
                }
            }
             catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View();
        }
    }
}