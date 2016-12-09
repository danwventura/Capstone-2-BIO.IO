using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bio.io.Models
{
    public class Device
    {
        [Key]
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public DateTime LastTransmit { get; set; }
        //Do I need a UserID here?
    }
}