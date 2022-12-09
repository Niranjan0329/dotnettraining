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
    public class TravelController : Controller
    {
        // GET: Travel

        Uri baseAddress = new Uri("https://localhost:44342//api");
        HttpClient client;

        public TravelController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        //employee adding
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // [Authorize]
        public ActionResult Create(Managermodel prod)
        {
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                var posttask = webclient.PostAsJsonAsync<Managermodel>("employee", prod);
                posttask.Wait();
                var dataresult = posttask.Result;
                try
                {
                    if (dataresult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            ModelState.AddModelError(string.Empty, "Some Error Occured..");
            return View(prod);
        }
        //GET: Employee
        public ActionResult Index()
        
        {
            List<Managermodel> req = new List<Managermodel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Rejected").Result;
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

        public ActionResult travl()

        {
            List<Managermodel> req = new List<Managermodel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Travelist").Result;
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



        public ActionResult List()
        
        {

               IEnumerable<Managermodel> prodlist = null;
                using (var webclient = new HttpClient())
                {
                    webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                    var responsetask = webclient.GetAsync("employee");
                    responsetask.Wait();
                    var result = responsetask.Result;
                try
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var resultdata = result.Content.ReadAsStringAsync().Result;
                        prodlist = JsonConvert.DeserializeObject<List<Managermodel>>(resultdata);
                    }
                    else
                    {
                        prodlist = Enumerable.Empty<Managermodel>();
                        ModelState.AddModelError(string.Empty, "Some Error Occured.. Try Later");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                    return View(prodlist);
                }
                

            }

        
        public ActionResult BookTicket(int id)
        {
           
            
            Managermodel prodlist = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                var responsetask = webclient.GetAsync("Rejected?id=" + id.ToString());
                responsetask.Wait();
                var result = responsetask.Result;
                try
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var resultdata = result.Content.ReadAsStringAsync().Result;
                        prodlist = JsonConvert.DeserializeObject<List<Managermodel>>(resultdata).Where(a => a.Requestid == id).FirstOrDefault();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }


            HttpClient hc1 = new HttpClient();
            hc1.BaseAddress = new Uri("https://localhost:44342/api/Rejected");

            var updateapi = hc1.PutAsJsonAsync<Managermodel>("Rejected", prodlist);
            updateapi.Wait();

            var savedata = updateapi.Result;
            try
            {
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("Index");

        }
        public ActionResult RejectTicket(int id)
        {
            //Managermodel empobj = null;
            //HttpClient hc = new HttpClient();
            //hc.BaseAddress = new Uri("https://localhost:44342/api/");
            Managermodel prodlist = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                var responsetask = webclient.GetAsync("Rejected?id=" + id.ToString());
                responsetask.Wait();
                var result = responsetask.Result;
                try
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var resultdata = result.Content.ReadAsStringAsync().Result;
                        prodlist = JsonConvert.DeserializeObject<List<Managermodel>>(resultdata).Where(a => a.Requestid == id).FirstOrDefault();
                    }
                }
                catch(Exception e )
                {
                    Console.WriteLine(e);
                }
            }


            HttpClient hc1 = new HttpClient();
            hc1.BaseAddress = new Uri("https://localhost:44342/api/Rejected");
            prodlist.CurrentStatus = "Rejected";
            var updateapi = hc1.PutAsJsonAsync<Managermodel>("Rejected", prodlist);
            updateapi.Wait();

            var savedata = updateapi.Result;
            try
            {

                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View("Index");

        }
    }
}