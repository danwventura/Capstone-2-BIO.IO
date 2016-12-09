using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bio.io.Models;
using System.Collections.Generic;
using Bio.io.DAL;
using System.Linq;
using System.Data.Entity;


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
            mock_users.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(query_users.GetEnumerator());

            mock_devices.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(query_devices.Provider);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(query_devices.Expression);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(query_devices.ElementType);
            mock_devices.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(query_devices.GetEnumerator());

            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Provider).Returns(query_datapoints.Provider);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.Expression).Returns(query_datapoints.Expression);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.ElementType).Returns(query_datapoints.ElementType);
            mock_datapoints.As<IQueryable<DataPoint>>().Setup(m => m.GetEnumerator()).Returns(query_datapoints.GetEnumerator());

            mock_routes.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(query_routes.Provider);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(query_routes.Expression);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(query_routes.ElementType);
            mock_routes.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(query_routes.GetEnumerator());

            mock_images.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(query_images.Provider);
            mock_images.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(query_images.Expression);
            mock_images.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(query_images.ElementType);
            mock_images.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(query_images.GetEnumerator());

            mock_context.Setup(c => c.BioioUsers).Returns(mock_users.Object);
            mock_context.Setup(c => c.Devices).Returns(mock_devices.Object);
            mock_context.Setup(c => c.DataPoints).Returns(mock_datapoints.Object);
            mock_context.Setup(c => c.Routes).Returns(mock_routes.Object);
            mock_context.Setup(c => c.Images).Returns(mock_images.Object);
        }
    }
}
