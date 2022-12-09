using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace applicationmvc.Models
{
    public class Managermodel
    {
        public int Requestid { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public Nullable<System.DateTime> RequestDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public Nullable<int> UserId { get; set; }
        public string CurrentStatus { get; set; }
    }

   
}