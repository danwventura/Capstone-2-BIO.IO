using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bio.io.Models;
using System.Collections.Generic;
using Bio.io.DAL;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Bio.io.Tests.DAL
{
    [TestClass]
    public class BioioRepoTests
    {
        private Mock<DbSet<User>> mock_users {get;set;}
        private Mock<DbSet<Device>> mock_devices {get;set;}
        private Mock<DbSet<DataPoint>> mock_datapoints {get;set;}
        private Mock<DbSet<Route>> mock_routes {get;set;}
        private Mock<DbSet<Image>> mock_images {get;set;}
        private Mock<BioioContext> mock_context { get; set; }
        private BioioRepository Repo { get; set; }
        private List<User> users {get; set;}
        private List<Device> devices { get; set; }
        private List<DataPoint> datapoints { get; set; }
        private List<Route> routes { get; set; }
        private List<Image> images { get; set; }


        //Device first_device = new Device { DeviceID = 1, Name = "terminator" };
        //Device second_device = new Device { DeviceID = 2, Name = "Johnny5" };

        //DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321 };
        //DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567 };
        //DataPoint datapoint_3 = new DataPoint { DataPointID = 3, Latitude = 3.456789, Longitude = 9.876543 };
        //DataPoint datapoint_4 = new DataPoint { DataPointID = 4, Latitude = 9.876543, Longitude = 3.456789 };
        //List<DataPoint> route1_datapoints = new List<DataPoint> { datapoint_1, datapoint_2 };
        //List<DataPoint> route2_datapoints = new List<DataPoint> { datapoint_3, datapoint_4 };
        //Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
        //Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
        //Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };
        //Image image_4 = new Image { ImageID = 4, URL = "www.yourthing.com" };
        //List<Image> route1_images = new List<Image> { image_1, image_2 };
        //List<Image> route2_images = new List<Image> { image_3, image_4 };

        //Route route_1 = new Route { RouteID = 1, Coordinates = route1_datapoints, Snapshots = route1_images };
        //Route route_2 = new Route { RouteID = 2, Coordinates = route2_datapoints, Snapshots = route2_images };



        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<BioioContext>();
            mock_users = new Mock<DbSet<User>>();
            mock_devices = new Mock<DbSet<Device>>();
            mock_datapoints = new Mock<DbSet<DataPoint>>();
            mock_routes = new Mock<DbSet<Route>>();
            mock_images = new Mock<DbSet<Image>>();
            users = new List<User>
            {
                new User {
                    UserID = 1,
                    BaseUser = new ApplicationUser {Id= "1", UserName = "dan@theman.com", Email="dan@theman.com" },
                    
                },
                new User {
                    UserID = 1,
                    BaseUser = new ApplicationUser {Id = "2", UserName = "tim@honey.com", Email="tim@honey.com" },
                    
                }
            };
            devices = new List<Device>();
            routes = new List<Route>();
            datapoints = new List<DataPoint>();
            images = new List<Image>();

            ConnectMocksToDatastore();
            Repo = new BioioRepository(mock_context.Object);
        }

        public void ConnectMocksToDatastore()
        {
            var query_users = users.AsQueryable();
            var query_devices = devices.AsQueryable();
            var query_datapoints = datapoints.AsQueryable();
            var query_routes = routes.AsQueryable();
            var query_images = images.AsQueryable();

            mock_users.As<IQueryable<User>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<User>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_devices.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(query_devices.Provider);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(query_devices.Expression);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(query_devices.ElementType);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(() =>query_devices.GetEnumerator());

            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Provider).Returns(query_datapoints.Provider);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Expression).Returns(query_datapoints.Expression);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.ElementType).Returns(query_datapoints.ElementType);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.GetEnumerator()).Returns(() => query_datapoints.GetEnumerator());

            mock_routes.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(query_routes.Provider);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(query_routes.Expression);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(query_routes.ElementType);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(() => query_routes.GetEnumerator());

            mock_images.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(query_images.Provider);
            mock_images.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(query_images.Expression);
            mock_images.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(query_images.ElementType);
            mock_images.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(() => query_images.GetEnumerator());

            mock_context.Setup(c => c.BioioUsers).Returns(mock_users.Object);
            mock_context.Setup(c => c.Devices).Returns(mock_devices.Object);
            mock_context.Setup(c => c.DataPoints).Returns(mock_datapoints.Object);
            mock_context.Setup(c => c.Routes).Returns(mock_routes.Object);
            mock_context.Setup(c => c.Images).Returns(mock_images.Object);



            mock_devices.Setup(d => d.Add(It.IsAny<Device>())).Callback((Device d) => devices.Add(d));
            mock_devices.Setup(d => d.Remove(It.IsAny<Device>())).Callback((Device d) => devices.Remove(d));

            mock_datapoints.Setup(e => e.Add(It.IsAny<DataPoint>())).Callback((DataPoint e) => datapoints.Add(e));
            mock_datapoints.Setup(e => e.Remove(It.IsAny<DataPoint>())).Callback((DataPoint e) => datapoints.Remove(e));

            mock_routes.Setup(f => f.Add(It.IsAny<Route>())).Callback((Route f) => routes.Add(f));
            mock_routes.Setup(f => f.Remove(It.IsAny<Route>())).Callback((Route f) => routes.Remove(f));

            mock_images.Setup(d => d.Add(It.IsAny<Image>())).Callback((Image g) => images.Add(g));
            mock_images.Setup(d => d.Remove(It.IsAny<Image>())).Callback((Image g) => images.Remove(g));


        }

        [TestMethod]
        public void EnsureCanCreateInstanceOfRepo()
        {
            //Arrange
            BioioRepository repository = new BioioRepository();
            //Act

            //Assert
            Assert.IsNotNull(repository);
        }

        /////////////////////////////////
        /////////GET ALL INSTANCES///////
        ///////////////////////////////

        [TestMethod]
        public void EnsureCanGetAllUsers()
        {

            //Arrange
            //Act
            List<User> all_users = Repo.GetAllUsers();
            int expected_count = 2;
            int actual_count = all_users.Count;
            
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanGetAllDevices()
        {
            //Arrange
            Device first_device = new Device { DeviceID = 1, Name = "terminator" };
            Device second_device = new Device { DeviceID = 2, Name = "Johnny5" };
            //Act
            Repo.AddDevice(first_device);
            Repo.AddDevice(second_device);
            int expected_count = 2;
            int actual_count = Repo.GetAllDevices().Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanGetAllDataPoints()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };

            DataPoint datapoint_1 = new DataPoint {DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567, Snapshot = image_2 };
            //Act
            Repo.AddDataPoint(datapoint_1);
            Repo.AddDataPoint(datapoint_2);
            int expected_count = 2;
            int actual_count = Repo.GetAllDataPoints().Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanGetAllRoutes()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };
            Image image_4 = new Image { ImageID = 4, URL = "www.yourthing.com" };
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567, Snapshot = image_2 };
            DataPoint datapoint_3 = new DataPoint { DataPointID = 3, Latitude = 3.456789, Longitude = 9.876543, Snapshot = image_3 };
            DataPoint datapoint_4 = new DataPoint { DataPointID = 4, Latitude = 9.876543, Longitude = 3.456789, Snapshot = image_4 };
            List<DataPoint> route1_datapoints = new List<DataPoint> { datapoint_1, datapoint_2 };
            List<DataPoint> route2_datapoints = new List<DataPoint> { datapoint_3, datapoint_4 };          

            Route route_1 = new Route { RouteID = 1, Coordinates = route1_datapoints};
            Route route_2 = new Route { RouteID = 2, Coordinates = route2_datapoints};

            //Act
            Repo.AddNewRoute(route_1);
            Repo.AddNewRoute(route_2);
            int expected_count = 2;
            int actual_count = Repo.GetAllRoutes().Count();

            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanGetAllImages()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };

            //Act
            Repo.AddNewImage(image_1);
            Repo.AddNewImage(image_2);
            Repo.AddNewImage(image_3);
            int expected_count = 3;
            int actual_count = Repo.GetAllImages().Count();

            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        /////////////////////////////////
        /////////ADDING INSTANCES///////
        ///////////////////////////////


        [TestMethod]
        public void EnsureCanAddNewUser()
        {
            //Arrange
            User test_1 = new User {UserID = 10, BaseUser = new ApplicationUser { Id = "10", UserName = "dan@van.com", Email = "dan@van.com" } };
            User test_2 = new User { UserID = 12, BaseUser = new ApplicationUser { Id = "12", UserName = "zach@moneysack.com", Email = "zach@moneysack.com" } };
            //Act
            Repo.AddNewUser(test_1);
            Repo.AddNewUser(test_2);
            int expected_count = 2;
            int actual_count = Repo.GetAllUsers().Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);


        }

        [TestMethod]
        public void EnsureCanAddNewDevice()
        {
            //Arrange
            Device first_device = new Device { DeviceID = 1, Name = "terminator" };
            Device second_device = new Device { DeviceID = 2, Name = "Johnny5" };
            //Act
            Repo.AddDevice(first_device);
            Repo.AddDevice(second_device);
            int expected_count = 2;
            int actual_count = Repo.Context.Devices.Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanAddNewDataPoint()
        {
            
            //Arrange
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567 };
            //Act
            Repo.AddDataPoint(datapoint_1);
            Repo.AddDataPoint(datapoint_2);
            int expected_count = 2;
            int actual_count = Repo.Context.DataPoints.Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanAddNewRoute()
        {
            //Arrange

            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };
            Image image_4 = new Image { ImageID = 4, URL = "www.yourthing.com" };
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567, Snapshot = image_2 };
            DataPoint datapoint_3 = new DataPoint { DataPointID = 3, Latitude = 3.456789, Longitude = 9.876543, Snapshot = image_3 };
            DataPoint datapoint_4 = new DataPoint { DataPointID = 4, Latitude = 9.876543, Longitude = 3.456789, Snapshot = image_4 };
            List<DataPoint> route1_datapoints = new List<DataPoint> {datapoint_1, datapoint_2};
            List<DataPoint> route2_datapoints = new List<DataPoint> { datapoint_3, datapoint_4};
           

            Route route_1 = new Route {RouteID = 1, Coordinates = route1_datapoints};
            Route route_2 = new Route {RouteID = 2, Coordinates = route2_datapoints};
            //Act
            Repo.AddNewRoute(route_1);
            Repo.AddNewRoute(route_2);
            int expected_count = 2;
            int actual_count = Repo.Context.Routes.Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanAddNewImage()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            //Act
            Repo.AddNewImage(image_1);
            Repo.AddNewImage(image_2);
            int expected_count = 2;
            int actual_count = Repo.Context.Images.Count();
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        /////////////////////////////////
        /////////REMOVING INSTANCES///////
        ///////////////////////////////

        [TestMethod]
        public void EnsureCanRemoveDevice()
        {
            //Arrange
            Device first_device = new Device { DeviceID = 1, Name = "terminator" };
            Device second_device = new Device { DeviceID = 29, Name = "Johnny5" };
            //Act
            Repo.AddDevice(first_device);
            Repo.AddDevice(second_device);
            Repo.RemoveDevice(1);

            int expected_count = 1;
            int actual_count = Repo.GetAllDevices().Count();
            int expected_id = 29;
            int actual_id = Repo.GetDeviceByID(29).DeviceID;


            //Assert
            Assert.AreEqual(expected_count, actual_count);
            Assert.AreEqual(expected_id, actual_id);
        }

        [TestMethod]
        public void EnsureCanRemoveDataPoint()
        {

            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            DataPoint datapoint_1 = new DataPoint { DataPointID = 24, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 25, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_2 };
            //Act
            Repo.AddDataPoint(datapoint_1);
            Repo.AddDataPoint(datapoint_2);
            Repo.RemoveDataPoint(24);
            int expected_count = 1;
            DataPoint found_datapoint = Repo.GetDataPointByID(25);
            int actual_count = Repo.GetAllDataPoints().Count();
            //Assert
            Assert.IsNotNull(found_datapoint);
            Assert.AreEqual(expected_count, actual_count);

        }

        [TestMethod]
        public void EnsureCanRemoveRoute()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };
            Image image_4 = new Image { ImageID = 4, URL = "www.yourthing.com" };
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567, Snapshot = image_2 };
            DataPoint datapoint_3 = new DataPoint { DataPointID = 3, Latitude = 3.456789, Longitude = 9.876543, Snapshot = image_3 };
            DataPoint datapoint_4 = new DataPoint { DataPointID = 4, Latitude = 9.876543, Longitude = 3.456789, Snapshot = image_4 };
            List<DataPoint> route1_datapoints = new List<DataPoint> { datapoint_1, datapoint_2 };
            List<DataPoint> route2_datapoints = new List<DataPoint> { datapoint_3, datapoint_4 };

            Route route_1 = new Route { RouteID = 99, Coordinates = route1_datapoints };
            Route route_2 = new Route { RouteID = 100, Coordinates = route2_datapoints };
            //Act
            Repo.AddNewRoute(route_1);
            Repo.AddNewRoute(route_2);
            Repo.RemoveRoute(99);
            int expected_count = 1;
            Route found_route = Repo.GetRouteByID(100);
            int actual_count = Repo.GetAllRoutes().Count();
            //Assert
            Assert.IsNotNull(found_route);
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanRemoveImage()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 84, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 85, URL = "www.thatthing.com" };
            //Act
            Repo.AddNewImage(image_1);
            Repo.AddNewImage(image_2);
            Repo.RemoveImage(85);
            int expected_count = 1;
            int actual_count = Repo.GetAllImages().Count();
            Image found_image = Repo.GetImageByID(84);


            //Assert
            Assert.IsNotNull(found_image);
            Assert.AreEqual(expected_count, actual_count);
        }

        

        /////////////////////////////////
        /////////GET INSTANCE BY ID///////
        ///////////////////////////////


        [TestMethod]
        public void EnsureCanGetUserByID() 
        {
            //Arrange
            ConnectMocksToDatastore();
            //Act
            int expected_userID = 1;
            User found_user = Repo.GetUserByID("1");
            int actual_userID = found_user.UserID;
            //Assert
            Assert.IsNotNull(found_user);
            Assert.AreEqual(expected_userID, actual_userID);
        }

        [TestMethod]
        public void EnsureCanGetDeviceByID()
        {

            //Arrange
            Device first_device = new Device { DeviceID = 1, Name = "terminator" };
            Device second_device = new Device { DeviceID = 2, Name = "Johnny5" };
            //Act
            Repo.AddDevice(first_device);
            Repo.AddDevice(second_device);
            int expected_id = 1;
            Device found_device = Repo.GetDeviceByID(1);
            int actual_id = found_device.DeviceID;
            //Assert
            Assert.IsNotNull(found_device);
            Assert.AreEqual(expected_id, actual_id);
        }

        [TestMethod]
        public void EnsureCanGetDataPointByID()
        {   
            //Arrange
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567 };
            //Act
            Repo.AddDataPoint(datapoint_1);
            Repo.AddDataPoint(datapoint_2);
            int expected_id = 1;
            DataPoint found_datapoint = Repo.GetDataPointByID(1);
            int actual_id = found_datapoint.DataPointID;
            //Assert
            Assert.IsNotNull(found_datapoint);
            Assert.AreEqual(expected_id, actual_id);
        }

        [TestMethod]
        public void EnsureCanGetRouteByID()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 1, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 2, URL = "www.thatthing.com" };
            Image image_3 = new Image { ImageID = 3, URL = "www.mything.com" };
            Image image_4 = new Image { ImageID = 4, URL = "www.yourthing.com" };
            DataPoint datapoint_1 = new DataPoint { DataPointID = 1, Latitude = 1.234567, Longitude = 7.654321, Snapshot = image_1 };
            DataPoint datapoint_2 = new DataPoint { DataPointID = 2, Latitude = 7.654321, Longitude = 1.234567, Snapshot = image_2 };
            DataPoint datapoint_3 = new DataPoint { DataPointID = 3, Latitude = 3.456789, Longitude = 9.876543, Snapshot = image_3 };
            DataPoint datapoint_4 = new DataPoint { DataPointID = 4, Latitude = 9.876543, Longitude = 3.456789, Snapshot = image_4 };
            List<DataPoint> route1_datapoints = new List<DataPoint> { datapoint_1, datapoint_2 };
            List<DataPoint> route2_datapoints = new List<DataPoint> { datapoint_3, datapoint_4 };
            
           

            Route route_1 = new Route { RouteID = 1, Coordinates = route1_datapoints};
            Route route_2 = new Route { RouteID = 2, Coordinates = route2_datapoints};
            //Act
            Repo.AddNewRoute(route_1);
            Repo.AddNewRoute(route_2);
            int expected_id = 2;
            Route found_route = Repo.GetRouteByID(2);
            int actual_id = found_route.RouteID;
            //Assert
            Assert.IsNotNull(found_route);
            Assert.AreEqual(expected_id, actual_id);
        }

        [TestMethod]
        public void EnsureCanGetImageByID()
        {
            //Arrange
            Image image_1 = new Image { ImageID = 23, URL = "www.thisthing.com" };
            Image image_2 = new Image { ImageID = 46, URL = "www.thatthing.com" };
            //Act
            Repo.AddNewImage(image_1);
            Repo.AddNewImage(image_2);
            int expected_id = 46;
            Image found_image = Repo.GetImageByID(46);
            int actual_id = found_image.ImageID;
            //Assert
            Assert.IsNotNull(found_image);
            Assert.AreEqual(expected_id, actual_id);
        }
    }
}
