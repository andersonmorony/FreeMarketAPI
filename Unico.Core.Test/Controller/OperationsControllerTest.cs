using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Unico.Core.API.Controllers;
using Unico.Core.API.Data;
using Unico.Core.API.Profiles;
using Unico.Core.API.Repository;
using Unico.Core.API.Services;
using Xunit;

namespace Unico.Core.Test.Controller
{

    public class OperationsControllerTest
    {
        private readonly Mapper _mapper;
        private readonly MarketService _marketService;
        public OperationsControllerTest()
        {
            //Mapper
            var marketProfile = new MarketProfile();
            var config = new MapperConfiguration(provider => provider.AddProfile(marketProfile));
            _mapper = new Mapper(config);

            //DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(MarketControllerTest)).Options;
            var dbContext = new AppDbContext(options);

            //Repository
            MarketRepository marketRepository = new MarketRepository(dbContext);

            //Service
            _marketService = new MarketService(_mapper, null, marketRepository);
        }

        [Fact]
        public void InsertCsv_IsInvalid()
        {
            // Arrange
            var filename = "Invalid.csv";

            // Act
            OperationsController operationsController = new OperationsController(_marketService);
            var response = operationsController.InsertCsv(filename).Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)response.StatusCode);
        }
    }
}
