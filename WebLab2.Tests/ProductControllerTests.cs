using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebLab2.Controllers;
using WebLab2.DAL.Data;
using WebLab2.DAL.Entities;
using Xunit;

namespace WebLab2.Tests
{
    public class ProductControllerTests
    {
        DbContextOptions<ApplicationDbContext> _options;
        public ProductControllerTests()
        {
            _options =
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            //.UseInMemoryDatabase(databaseName: "aspnet-WebLab2-Data")
            .Options;
        }

        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            var controllerContext = new ControllerContext();
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                // Act
                var result = controller.Index(group: null, pageNo: page) as ViewResult;
                var model = result?.Model as List<Transport>;
                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].TransportId);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
        [Fact]
        public void ControllerSelectsGroup()
        {
            var controllerContext = new ControllerContext();
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                var comparer = Comparer<Transport>.GetComparer((d1, d2) => d1.TransportId.Equals(d2.TransportId));
                // act
                var result = controller.Index(2) as ViewResult;
                var model = result.Model as List<Transport>;
                // assert
                Assert.Equal(2, model.Count);
                Assert.Equal(context.Transports
                .ToArrayAsync()
                .GetAwaiter()
                .GetResult()[2], model[0], comparer);
            }

            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }

        }
    }
}
