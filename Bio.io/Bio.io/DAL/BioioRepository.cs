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

        
        //////////////////////////////////////
        ////////GET ALL INSTANCES OF TYPE/////
        /////////////////////////////////////
       
        
        public List<User> GetAllUsers()
        {
            
            return Context.BioioUsers.ToList();
        }

        public List<Device> GetAllDevices()
        {
            return Context.Devices.ToList();
        }

        public List<DataPoint> GetAllDataPoints()
        {
            return Context.DataPoints.ToList();
        }

        public List<Route> GetAllRoutes()
        {
            return Context.Routes.ToList();
        }

        public List<Image> GetAllImages()
        {
            return Context.Images.ToList();
        }

        //////////////////////////////////////
        ////////ADD NEW INSTANCE OF TYPE/////
        /////////////////////////////////////

        public void AddNewUser(string user_id)
        {
            ApplicationUser found_app_user = Context.Users.FirstOrDefault(u => u.Id == user_id);
            var new_user = new User { BaseUser = found_app_user };
            Context.BioioUsers.Add(new_user);
            Context.SaveChanges();

        }


        public void AddDataPoint(DataPoint datapoint)
        {
            Context.DataPoints.Add(datapoint);
            Context.SaveChanges();
        }

        public void AddDevice(Device device)
        {
            Context.Devices.Add(device); //Why is this showing up as null
            Context.SaveChanges();           
        }

        public void AddNewRoute(Route route)
        {
            Context.Routes.Add(route);
            Context.SaveChanges();
        }

        public void AddNewImage(Image image)
        {
            Context.Images.Add(image);
            Context.SaveChanges();
        }

        //////////////////////////////////////
        //////REMOVE INSTANCE OF TYPE/////////
        /////////////////////////////////////


        public void RemoveDevice(int deviceID)
        {
            Device found_device = GetDeviceByID(deviceID); //getting null here
            Context.Devices.Remove(found_device);
            Context.SaveChanges();
        }

        public void RemoveDataPoint(int datapointID)
        {
            DataPoint found_datapoint = GetDataPointByID(datapointID);
            Context.DataPoints.Remove(found_datapoint);
            Context.SaveChanges();
        }

        public void RemoveRoute(int routeID)
        {
            Route found_route = GetRouteByID(routeID);
            Context.Routes.Remove(found_route);
            Context.SaveChanges();
        }

        public void RemoveImage(int imageID)
        {
            Image found_image = GetImageByID(imageID);
            Context.Images.Remove(found_image);
            Context.SaveChanges();
        }

        //////////////////////////////////////
        //////GET INSTANCE OF TYPE BY ID/////
        /////////////////////////////////////

        public User GetUserByID(string user_id)
        {
            //int i = 0;
            ApplicationUser found_app_user = Context.Users.FirstOrDefault(u => u.Id == user_id);
            User found_user = GetUserFromUserName(found_app_user.UserName);
            return found_user;
        }

        public User GetUserFromUserName(string user_name)
        {
            User foundUser = Context.BioioUsers.FirstOrDefault(u => u.BaseUser.UserName == user_name);
            return foundUser;   //Broken here
        } 

        public Device GetDeviceByID(int deviceID)
        {
            Device found_device = Context.Devices.FirstOrDefault(d => d.DeviceID == deviceID); 
            return found_device;
        }

        public DataPoint GetDataPointByID(int datapointID)
        {
            DataPoint found_datapoint = Context.DataPoints.FirstOrDefault(p => p.DataPointID == datapointID);
            return found_datapoint;
        }

        public Route GetRouteByID(int routeID)
        {
            Route found_route = Context.Routes.FirstOrDefault(r => r.RouteID == routeID);
            return found_route;
        }

        public Image GetImageByID(int imageID)
        {
            Image found_image = Context.Images.FirstOrDefault(i => i.ImageID == imageID);
            return found_image;
        }

        
    }
}