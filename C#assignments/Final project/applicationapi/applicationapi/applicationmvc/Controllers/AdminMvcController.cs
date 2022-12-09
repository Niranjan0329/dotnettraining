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
    public class AdminMvcController : Controller
    {
        // GET: AdminMvc
        //public ActionResult Index()
        //{
        //    return View();
        //}
        Uri baseAddress = new Uri("https://localhost:44342//api/");
        HttpClient client;

        public AdminMvcController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Display()
        {
            
            try
            {

                IEnumerable<User1> prodlist = null;
                using (var webclient = new HttpClient())
                {
                    webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                    var responsetask = webclient.GetAsync("Admin");
                    responsetask.Wait();
                    var result = responsetask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultdata = result.Content.ReadAsStringAsync().Result;
                        prodlist = JsonConvert.DeserializeObject<List<User1>>(resultdata);
                    }
                    else
                    {
                        prodlist = Enumerable.Empty<User1>();
                        ModelState.AddModelError(string.Empty, "Some Error Occured.. Try Later");
                    }
                   
                }
                return View(prodlist);
            }
             
            catch (Exception e)
            {
                Console.WriteLine("Invalid", e);
            }
            return View();
           
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        // [Authorize]
        public ActionResult Create(User1 prod)
        {
            try
            {


                using (var webclient = new HttpClient())
                {
                    webclient.BaseAddress = new Uri("https://localhost:44342/api/");
                    var posttask = webclient.PostAsJsonAsync<User1>("Admin", prod);
                    posttask.Wait();
                    var dataresult = posttask.Result;
                    if (dataresult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Display");
                    }
                }
                ModelState.AddModelError(string.Empty, "Some Error Occured..");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);

            }
            return View(prod);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           


                HttpClient hc1 = new HttpClient();
                hc1.BaseAddress = new Uri("https://localhost:44342/api/");
                User1 model = new User1();
                var emp = hc1.GetAsync("Admin?id=" + id.ToString()).Result;
            try
            {
                if (emp.IsSuccessStatusCode)
                {
                    string data = emp.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<User1>(data);
                }
               
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View("Create", model);


        }

        [HttpPost]

        public ActionResult Edit(User1 product)
        {


            //HttpClient hc2= new HttpClient();
            //hc2.BaseAddress = new Uri("https://localhost:44342/api/");
            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = client.PutAsync(client.BaseAddress + "Admin/" + product.User_id, content).Result;
            //var res = hc2.PutAsync("Admin",content);
            try
            {

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Display");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View();

          }


        public ActionResult Delete(int id)
        {

            User1 user = new User1();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44342/api/");
            var response = client.DeleteAsync("Admin/" + id).Result;
            try
            {

                if (response.IsSuccessStatusCode)
                {
                    //string data = response.Content.ReadAsStringAsync().Result;
                    //user = JsonConvert.DeserializeObject<User1>(data);
                    return RedirectToAction("Display");

                }
                else
                {
                    Console.Write("Error");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View(user);
        }

    }
}