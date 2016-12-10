using Bio.io.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bio.io.DAL
{
    public class BioioRepository
    {
        public BioioContext Context { get; set; }
        public BioioRepository(BioioContext _context)
        {
            Context = _context;
        }

        public BioioRepository()
        {
            Context = new BioioContext();
        }


        //public void AddDevice(Device a_device)
        //{
        //    Context.Devices.Add(a_device);
        //    Context.SaveChanges();
        //}

        public List<User> GetAllUsers()
        {
           return Context.BioioUsers.ToList();
        }

        public User GetUserByID(int userID) //Do I need this?
        {
            BioioRepository repo = new BioioRepository();
            List<User> all_users = repo.GetAllUsers();
            User found_user = all_users.FirstOrDefault( u => u.UserID == userID);
            return found_user;
        }

        public void AddDevice(Device device)
        {
            int i = 0;
            Device this_device = device;
            
            Context.Devices.Add(device); //Why is this showing up as null
            Context.SaveChanges();
            
        }
    }
}