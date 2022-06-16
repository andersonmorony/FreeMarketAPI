using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Services;
using Xunit;

namespace Unico.Core.Test.Services
{
    public class MarketRequestServiceTests
    {
        private readonly AppDbContext _dbContext;

        public MarketRequestServiceTests()
        {
           
            
        }

        [Fact]
        public async void ShouldReturnMarket()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(nameof(ShouldReturnMarket)).Options;
            var dbContext = new AppDbContext(options);
            seedData(dbContext);
            var _marketService = new MarketService(dbContext);

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

            var _marketService = new MarketService(dbContext);

            var result = await _marketService.GetMarketsAsync();

            Assert.False(result.IsSuccess);
            Assert.Null(result.markets);
            Assert.NotNull(result.MsgError);
        }
        private void seedData(AppDbContext _dbContext)
        {
            if (!_dbContext.Markets.Any())
            {
                _dbContext.Markets.AddRange(new List<Market>()
                {
                    new Market() { Id = 1, LONG = "-46550164", SETCENS = 355030885000091, AREAP = 3550308005040, CODDIST = 87, DISTRITO = "VILA", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA FONSECA", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = 15, REFERENCIA = "" },
                    new Market() { Id = 2, LONG = "-4655024", SETCENS = 35503020091, AREAP = 1308005040, CODDIST = 17, DISTRITO = "SAO MIGUEL", CODSUBPREF = 26, SUBPREFE = "VILA PRUDENTE", REGIAO5 = "LESTE", REGIAO8 = "LESTE 1", NOME_FEIRA = "VILA SAO MIGUEL", REGISTRO = "4041-0", LOGRADOURO = "RUA X", BAIRRO = "SP", LAT = "-23558733", NUMERO = 15, REFERENCIA = "" }
                });
                _dbContext.SaveChanges();
            }
        }
    }
}
