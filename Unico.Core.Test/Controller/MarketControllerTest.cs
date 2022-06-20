using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unico.Core.API.Controllers;
using Unico.Core.API.Data;
using Unico.Core.API.Models;
using Unico.Core.API.Profiles;
using Unico.Core.API.Repository;
using Unico.Core.API.Services;
using Xunit;

namespace Unico.Core.Test.Controller
{
    public class MarketControllerTest
    {
        private readonly Mapper _mapper;
        private readonly MarketService _marketService;

        public MarketControllerTest()
        {
            //Mapper
            var marketProfile = new MarketProfile();
            var config = new MapperConfiguration(provider => provider.AddProfile(marketProfile));
            _mapper = new Mapper(config);

            //DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(MarketControllerTest)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);

            //Repository
            MarketRepository marketRepository = new MarketRepository(dbContext);

            //Service
            _marketService = new MarketService(_mapper, null, marketRepository);

        }

        [Fact]
        public void GetMarkets_IsSuccess()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);

            // Act
            var result = _marketController.GetAllMarkets().Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

        }
        [Fact]
        public void GetMarkets_IsFail()
        {
            // Arrange
            var _marketController = new MarketController(null);

            // Act
            var result = _marketController.GetAllMarkets().Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);

        }
        [Fact]
        public void GetMarketByName_IsSuccess()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var param = "VILA SAO MIGUEL";
            var expectResult = new List<MarketResponse>()
                {
                    new MarketResponse() { Id = 2, LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "SAO MIGUEL", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" }
                };

            // Act
            var result = _marketController.GetMarketByName(param).Result as ObjectResult;
            var response = result.Value as List<MarketResponse>;

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotEmpty(response);
            Assert.Equal("SAO MIGUEL", response[0].DISTRITO);
        }
        [Fact]
        public void GetMarketByName_IsFail_InvalidName()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var invalidParam = "INVALID_NAME";

            // Act
            var response = _marketController.GetMarketByName(invalidParam).Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)response.StatusCode);
        }
        [Fact]
        public void CreateMarket_IsSuccess()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var param = new MarketRequest() { LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "SAO MIGUEL", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" };

            // Act
            var result = _marketController.CreateMarket(param).Result as ObjectResult;
            var response = result.Value as MarketResponse;

            // Assert
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(response);
            Assert.Equal("SAO MIGUEL", response.DISTRITO);
        }
        [Fact]
        public void CreateMarket_ShouldFail()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var param = new MarketRequest() { LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "SAO MIGUEL", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" };

            // Act
            var result = _marketController.CreateMarket(param).Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }
        [Fact]
        public void EditMarket_ShouldEdit()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var param = new MarketRequest() { LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "NEW DESTRITO", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" };

            // Act
            var result = _marketController.EditMarket(1, param).Result;

            // Assert
            Assert.Equal("NEW DESTRITO", result.Value.DISTRITO);
        }
        [Fact]
        public void EditMarket_ShouldFail()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);
            var param = new MarketRequest() { LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "DESTRITO", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" };

            // Act
            var result = _marketController.EditMarket(-1, param).Result;
            var objResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)objResult.StatusCode);
        }
        [Fact]
        public void EditMarket_ShouldThrowError()
        {
            // Arrange
            var _marketController = new MarketController(null);
            var param = new MarketRequest() { LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "DESTRITO", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" };

            // Act
            var result = _marketController.EditMarket(1, param).Result;
            var objResult = result.Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)objResult.StatusCode);
        }
        [Fact]
        public void DeleteMarket_IsSuccess()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);

            // Act
            var response = _marketController.DeleteMarket(1).Result as ObjectResult;

            // Assert
            Assert.Null(response);
        }
        [Fact]
        public void DeleteMarket_ShouldNotDeleteInvalidParam()
        {
            // Arrange
            var _marketController = new MarketController(_marketService);

            // Act
            var response = _marketController.DeleteMarket(-1).Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)response.StatusCode);
        }
        [Fact]
        public void DeleteMarket_ShouldTrowError()
        {
            // Arrange
            var _marketController = new MarketController(null);

            // Act
            var response = _marketController.DeleteMarket(1).Result as ObjectResult;

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)response.StatusCode);
        }
        private void seedData(AppDbContext _dbContext)
        {
            if (!_dbContext.Markets.Any())
            {
                _dbContext.Markets.AddRange(new List<Market>()
                {
                    new Market() { Id = 1, LONG = "-46550164", SETCENS = 355030885000091, AREAP = 3550308005040, CODDIST = 87, DISTRITO = "VILA", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA FONSECA", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" },
                    new Market() { Id = 3, LONG = "-46550164", SETCENS = 355030885000091, AREAP = 3550308005040, CODDIST = 87, DISTRITO = "VILA", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA FONSECA", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" },
                    new Market() { Id = 2, LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "SAO MIGUEL", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-2", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "", REFERENCIA = "" }
                });
                _dbContext.SaveChanges();
            }
        }
    }
}
