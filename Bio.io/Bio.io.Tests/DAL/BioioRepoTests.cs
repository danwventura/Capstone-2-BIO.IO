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
    }
}
