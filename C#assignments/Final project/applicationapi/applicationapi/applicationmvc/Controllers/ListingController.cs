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
    public class ListingController : Controller
    {
        // GET: Listing
        //public ActionResult Index()
        //{
        //    return View();
        //}
        Uri baseAddress = new Uri("https://localhost:44342//api");
        HttpClient client;

        public ListingController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()

        {
                List<Managermodel> req = new List<Managermodel>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Travellist").Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    req = JsonConvert.DeserializeObject<List<Managermodel>>(data);

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View(req);

        }

    }
}