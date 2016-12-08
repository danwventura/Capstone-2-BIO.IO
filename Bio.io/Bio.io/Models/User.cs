using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bio.io.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Device> Devices { get; set; }
        public List<Route> Routes { get; set; }
        public DateTime MemberSince { get; set; }
    }
}