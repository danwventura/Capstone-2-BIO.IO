﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bio.io.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string URL { get; set; }
        public DateTime Created { get; set; }

    }
}