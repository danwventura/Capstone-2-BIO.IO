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

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<BioioContext>();
            mock_users = new Mock<DbSet<User>>();
            mock_devices = new Mock<DbSet<Device>>();
            mock_datapoints = new Mock<DbSet<DataPoint>>();
            mock_routes = new Mock<DbSet<Route>>();
            mock_images = new Mock<DbSet<Image>>();
            devices = new List<Device>();
            users = new List<User>
            {
                new User {
                    UserID = 1,
                    BaseUser = new ApplicationUser {UserName = "dan@theman.com", Email="dan@theman.com" },
                    Name = "Dan"
                },

                new User {
                    UserID = 2,
                    BaseUser = new ApplicationUser {UserName = "tim@honey.com", Email="tim@honey.com" },
                    Name = "Tim"
                }
            };

            ConnectMocksToDatastore();
            Repo = new BioioRepository(mock_context.Object);
        }

        public void ConnectMocksToDatastore()
        {
            var query_users = users.AsQueryable();
            var query_devices = devices.AsQueryable();
            //var query_datapoints = datapoints.AsQueryable();
            //var query_routes = routes.AsQueryable();
            //var query_images = images.AsQueryable();

            mock_users.As<IQueryable<User>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<User>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_devices.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(query_devices.Provider);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(query_devices.Expression);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(query_devices.ElementType);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(() =>query_devices.GetEnumerator());

            //mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Provider).Returns(query_datapoints.Provider);
            //mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Expression).Returns(query_datapoints.Expression);
            //mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.ElementType).Returns(query_datapoints.ElementType);
            //mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.GetEnumerator()).Returns(() =>query_datapoints.GetEnumerator());

            //mock_routes.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(query_routes.Provider);
            //mock_routes.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(query_routes.Expression);
            //mock_routes.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(query_routes.ElementType);
            //mock_routes.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(() =>query_routes.GetEnumerator());

            //mock_images.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(query_images.Provider);
            //mock_images.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(query_images.Expression);
            //mock_images.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(query_images.ElementType);
            //mock_images.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(() =>query_images.GetEnumerator());

            mock_context.Setup(c => c.BioioUsers).Returns(mock_users.Object);
            mock_context.Setup(c => c.Devices).Returns(mock_devices.Object);
            //mock_context.Setup(c => c.DataPoints).Returns(mock_datapoints.Object);
            //mock_context.Setup(c => c.Routes).Returns(mock_routes.Object);
            //mock_context.Setup(c => c.Images).Returns(mock_images.Object);
            


            mock_devices.Setup(d => d.Add(It.IsAny<Device>())).Callback((Device d) => devices.Add(d));
            mock_devices.Setup(d => d.Remove(It.IsAny<Device>())).Callback((Device d) => devices.Remove(d));

            //mock_datapoints.Setup(e => e.Add(It.IsAny<DataPoint>())).Callback((DataPoint e) => datapoints.Add(e));
            //mock_datapoints.Setup(e => e.Remove(It.IsAny<DataPoint>())).Callback((DataPoint e) => datapoints.Remove(e));

            //mock_routes.Setup(f => f.Add(It.IsAny<Route>())).Callback((Route f) => routes.Add(f));
            //mock_routes.Setup(f => f.Remove(It.IsAny<Route>())).Callback((Route f) => routes.Remove(f));

            //mock_images.Setup(d => d.Add(It.IsAny<Image>())).Callback((Image g) => images.Add(g));
            //mock_images.Setup(d => d.Remove(It.IsAny<Image>())).Callback((Image g) => images.Remove(g));


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
        public void EnsureCanGetUserById() //Do I need to generate an Add User method so I can test this?
        {
            //Arrange
            //User user1 = new User {UserID = 3, BaseUser = new ApplicationUser {UserName = "Bob@Steve.com", Email = "Bob@Steve.com" }, Name = "Bob" };
            //User user2 = new User {UserID = 4, BaseUser = new ApplicationUser {UserName = "Steve@Bob.com", Email = "Steve@Bob.com" }, Name = "Steve" };
            
            //Act
            int expected_userID = 1;
            User found_user = Repo.GetUserByID(1);
            int actual_userID = found_user.UserID;
            //Assert
            Assert.AreEqual(expected_userID, actual_userID);
        }
        /////////////////////////////////
        /////////ADDING INSTANCES///////
        ///////////////////////////////



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
    }
}
