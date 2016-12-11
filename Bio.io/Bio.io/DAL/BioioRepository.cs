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


        //////////////////////////////////////
        //////GET INSTANCE OF TYPE BY ID/////
        /////////////////////////////////////

        public User GetUserByID(int userID)
        {

            User found_user = Context.BioioUsers.FirstOrDefault(u => u.UserID == userID);
            return found_user;
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