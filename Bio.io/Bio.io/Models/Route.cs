using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bio.io.Models
{
    public class Route
    {   [Key]
        public int RouteID { get; set; }
        public List<DataPoint> Coordinates { get; set; }
        public virtual List<Image> Snapshots { get; set; }
        public DateTime Created { get; set; }
    }
}