using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Unico.Core.Test.Services
{
    public class MarketRequestServiceTests
    {
        [Fact]
        public async void ShouldReturnMarket()
        {
            var _marketService = new MarketService();
           
            var result = await _marketService.GetMarketsAsync();

            Assert.NotNull(result);
        } 
    }
}
