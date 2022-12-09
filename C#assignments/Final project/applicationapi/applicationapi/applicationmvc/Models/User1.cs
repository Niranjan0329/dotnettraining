using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace applicationmvc.Models
{
    public class User1
    {
        [Required(ErrorMessage = "Please Enter Valid Userid")]
        //[Display(Name = "USER ID ")]
       
        public int User_id { get; set; }
        
       [Required(ErrorMessage = "please Enter valid Username ")]
       //[Display(Name = "UserName ")]
        public string Login_Id { get; set; }
        
        //[Display(Name = "Password ")]
        [Required(ErrorMessage = "please Enter Valid Password ")]
        public string Password { get; set; }
        //[Display(Name = "UserTypeId ")]
        [Required(ErrorMessage = "please Enter Valid usertypeid ")]
        public Nullable<int> User_type_id { get; set; }
        //[Display(Name = "Name ")]
        [Required(ErrorMessage = "please Enter Valid user Name")]
        public string Name { get; set; }
        //[Display(Name = "ManagerUserId ")]
        [Required(ErrorMessage = "please Enter Valid Managerid")]
        public Nullable<int> Manageruserid { get; set; }
    }
}