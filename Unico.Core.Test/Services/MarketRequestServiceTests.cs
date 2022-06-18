﻿using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Models;
using Unico.Core.API.Profiles;
using Unico.Core.API.Services;
using Xunit;

namespace Unico.Core.Test.Services
{
    public class MarketRequestServiceTests
    {
        private readonly Mapper _mapper;

        public MarketRequestServiceTests()
        {
            var marketProfile = new MarketProfile();
            var config = new MapperConfiguration(provider => provider.AddProfile(marketProfile));
            _mapper = new Mapper(config);
        }

        [Fact]
        public async void ShouldReturnMarket()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldReturnMarket)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);
            var _marketService = new MarketService(dbContext, _mapper, null);

            var result = await _marketService.GetMarketsAsync();

            Assert.True(result.IsSuccess);

            Assert.True(result.markets.Any());

            Assert.Null(result.MsgError);

        }
        [Fact]
        public async void ShouldReturnFalseToNoData()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldReturnFalseToNoData)).Options;
            var dbContext = new AppDbContext(options);

            var _marketService = new MarketService(dbContext, _mapper, null);

            var result = await _marketService.GetMarketsAsync();

            Assert.False(result.IsSuccess);
            Assert.Null(result.markets);
            Assert.NotNull(result.MsgError);
        }
        [Fact]
        public async void ShouldCreateMarket()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldCreateMarket)).Options;

            var dbContext = new AppDbContext(options);

            var _marketService = new MarketService(dbContext, _mapper, null);

            var request = new MarketRequest() { LONG = "-46550164", SETCENS = 355030885000091, AREAP = 3550308005040, CODDIST = 87, DISTRITO = "VILA", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA FONSECA", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "15", REFERENCIA = "" };

            var result = await _marketService.CreateMarketAsync(request);

            Assert.True(result.IsSuccess);
            Assert.Null(result.MsgError);
            Assert.NotNull(result.markets);
            Assert.Equal(request.LONG, result.markets.LONG);
            Assert.Equal(1, result.markets.Id);

        }
        [Fact]
        public async void ShoudDeleteMarket()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShoudDeleteMarket)).Options;

            var dbContext = new AppDbContext(options);

            seedData(dbContext);

            var _marketService = new MarketService(dbContext, _mapper, null);

            var result = await _marketService.DeleteMarketAsync(1);

            Assert.True(result.IsSuccess);
            Assert.Null(result.MsgError);
        }
        [Fact]
        public async void ShoudNotDeleteMarket()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShoudNotDeleteMarket)).Options;

            var dbContext = new AppDbContext(options);

            seedData(dbContext);

            var _marketService = new MarketService(dbContext, _mapper, null);

            var result = await _marketService.DeleteMarketAsync(-1);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.MsgError);
            Assert.Equal("Not Found", result.MsgError);
        }
        [Fact]
        public async void ShouldEditItem()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldEditItem)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);

            var marketService = new MarketService(dbContext, _mapper, null);

            var request = new MarketRequest() { LONG = "-1111111", SETCENS = 355030885000091, AREAP = 3550308005040, CODDIST = 87, DISTRITO = "EDITED VILA", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA FONSECA", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = "15", REFERENCIA = "" };

            var result = await marketService.EditMarketAsync(1, request);

            Assert.True(result.IsSuccess);
            Assert.Null(result.MsgError);
            Assert.Equal(1, result.marketResponse.Id);
            Assert.Equal("-1111111", result.marketResponse.LONG);
            Assert.Equal(355030885000091, result.marketResponse.SETCENS);
            Assert.Equal("EDITED VILA", result.marketResponse.DISTRITO);

        }
        [Fact]
        public async void ShouldReturnMarketByParams()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldReturnMarketByParams)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);
            var marketService = new MarketService(dbContext, _mapper, null);

            var result = await marketService.GetMarketsByNameAync("VILA FONSECA");

            Assert.True(result.IsSuccess);
            Assert.Null(result.MsgError);
            Assert.Equal(2, result.marketResponse.Count());
        }
        [Fact]
        public async void ShouldNotReturnMarketByInvalidParams()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldReturnMarketByParams)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);
            var marketService = new MarketService(dbContext, _mapper, null);

            var result = await marketService.GetMarketsByNameAync("Invalid_Name");

            Assert.False(result.IsSuccess);
            Assert.Null(result.marketResponse);
            Assert.NotNull(result.MsgError);
            Assert.Equal("Not found", result.MsgError);
        }
        [Fact]
        public async void ShouldUploadCSV()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldUploadCSV)).Options;
            var dbContext = new AppDbContext(options);
            var filename = "DEINFO_AB_FEIRASLIVRES_2014.csv";

            List<MarketCsv> markets = new List<MarketCsv>();

            #region Read CSV
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            };

            var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("bin\\Debug\\net5.0", string.Empty));
            var path = $"{dirName}{@"\files"}" + "\\" + filename;

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var market = csv.GetRecord<MarketCsv>();
                    markets.Add(market);
                }
            }
            #endregion

            var marketService = new MarketService(dbContext, _mapper, null);

            var result = await marketService.UploadCsvToCreateMarkets(markets);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.marketResponse);
            Assert.Equal(880, result.marketResponse.Count());
            Assert.Null(result.MsgError);
        }
        [Fact]
        public async void ShoulNotdUploadCSV()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShoulNotdUploadCSV)).Options;
            var dbContext = new AppDbContext(options);
            var filename = "Invalid.csv";

            List<MarketCsv> markets = new List<MarketCsv>();

            try
            {
                #region Read CSV
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null
                };

                var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("bin\\Debug\\net5.0", string.Empty));
                var path = $"{dirName}{@"\files"}" + "\\" + filename;

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, config))
                {

                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var market = csv.GetRecord<MarketCsv>();
                        markets.Add(market);
                    }
                }
                #endregion

                var marketService = new MarketService(dbContext, _mapper, null);

                var result = await marketService.UploadCsvToCreateMarkets(markets);

            }
            catch (Exception ex)
            {
                Assert.NotNull(ex.Message);
            }

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
