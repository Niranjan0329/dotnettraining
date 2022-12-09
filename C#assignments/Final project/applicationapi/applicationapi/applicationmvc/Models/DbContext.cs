using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using applicationmvc.Models;
using Newtonsoft.Json;

namespace applicationmvc.Models
{
    public class DbContext
    {
        public List<User1> GetUser()
        {
            
            var users = new List<User1>();
            using (var webclient = new HttpClient())
            {

                webclient.BaseAddress = new Uri("https://localhost:44342/api/User/");
                var responsetask = webclient.GetAsync("GetUser");
                responsetask.Wait();
                var result = responsetask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var resultdata = result.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<User1>>(resultdata);
                }
            }
            return users;
        }
    }
}