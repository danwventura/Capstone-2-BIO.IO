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
        public virtual ApplicationUser BaseUser { get; set; }
        public virtual List<Device> Devices { get; set; }
        public virtual List<Route> Routes { get; set; }

        
    }
}