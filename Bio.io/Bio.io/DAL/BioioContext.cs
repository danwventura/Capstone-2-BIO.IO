using Bio.io.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bio.io.DAL
{
    public class BioioContext : ApplicationDbContext
    {
        public virtual DbSet<User> BioioUsers {get; set;}
        public virtual DbSet<Device> Devices {get; set;}
        public virtual DbSet<DataPoint> DataPoints {get; set;}
        public virtual DbSet<Route> Routes{get; set;}
        public virtual DbSet<Image> Images{get; set;}

    }
}