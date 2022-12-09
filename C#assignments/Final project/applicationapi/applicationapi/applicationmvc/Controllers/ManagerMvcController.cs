using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using applicationmvc.Models;
using Newtonsoft.Json;

namespace applicationmvc.Controllers
{
    public class ManagerMvcController : Controller
    {
        // GET: ManagerMvc
        //public ActionResult Index()
        //{
        //    return View();
        //}

        Uri baseAddress = new Uri("https://localhost:44342//api");
        HttpClient client;

        public ManagerMvcController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public List<User1> GetUser()
        {
            var users = new List<User1>();
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("http://localhost:44342/api/user");
                var responsetask = webclient.GetAsync("GetUser");
                responsetask.Wait();
                var result = responsetask.Result;
                try
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var resultdata = result.Content.ReadAsStringAsync().Result;
                        users = JsonConvert.DeserializeObject<List<User1>>(resultdata);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return users;
        }


        //manager report
        public ActionResult Report()

        {
            IEnumerable<Managermodel> req = new List<Managermodel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ManagerRepot").Result;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    req = JsonConvert.DeserializeObject<List<Managermodel>>(data);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(req.AsEnumerable());
        }

        //GET: Employee
        public ActionResult Index()
        
        {
            List<Managermodel> req = new List<Managermodel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Manager").Result;
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
        
        public ActionResult Approve(int id)
        {
           Managermodel empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44342/api/");

            var consumeapi = hc.GetAsync("Manager?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            try
            {
                if (readdata.IsSuccessStatusCode)
                {
                    var displayempdetails = readdata.Content.ReadAsAsync<Managermodel>();
                    displayempdetails.Wait();
                    empobj = displayempdetails.Result;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            HttpClient hc1 = new HttpClient();
            hc1.BaseAddress = new Uri("https://localhost:44342/api/Manager");

            empobj.CurrentStatus = "Pending";
            var updateapi = hc1.PutAsJsonAsync<Managermodel>("Manager", empobj);
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

        public ActionResult Reject(int id)
        {
            Managermodel empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44342/api/");

            var consumeapi = hc.GetAsync("Manager?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            try
            {
                if (readdata.IsSuccessStatusCode)
                {
                    var displayempdetails = readdata.Content.ReadAsAsync<Managermodel>();
                    displayempdetails.Wait();
                    empobj = displayempdetails.Result;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            HttpClient hc1 = new HttpClient();
            hc1.BaseAddress = new Uri("https://localhost:44342/api/Manager");

            empobj.CurrentStatus = "Rejected";
            var updateapi = hc1.PutAsJsonAsync<Managermodel>("Manager", empobj);
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
