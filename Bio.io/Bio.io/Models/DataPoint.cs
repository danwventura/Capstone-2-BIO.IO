using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bio.io.Models
{
    public class DataPoint
    {
        [Key]
        public int DataPointID { get; set; }
        public double Coords{ get; set; }
        public virtual Image Snapshot { get; set; }
        public Device TransmittedBy { get; set; }
        public DateTime Created { get; set; }
    }
}