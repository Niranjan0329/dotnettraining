using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using applicationapi.Models;
namespace applicationapi.Controllers
{
    public class UserController : ApiController
    {
        newtravelbookingsystemEntities db1 = new newtravelbookingsystemEntities();

        public List<User> GetUser()
        {
            var list = db1.Users.ToList();
            return list;
        }
        [HttpPut]

        public HttpResponseMessage AddUpdateUser(User user)
        {
            try
            {
                if (user.User_id == 0)
                {
                    db1.Users.Add(user);
                }
                else
                {
                    var model = db1.Users.Where(a => a.User_id == user.User_id).FirstOrDefault();
                    model.Login_Id = user.Login_Id;
                    model.Manageruserid = user.Manageruserid;
                    model.Name = user.Name;
                    model.Password = user.Password;
                    model.User_type_id = user.User_type_id;
                }
                db1.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}
