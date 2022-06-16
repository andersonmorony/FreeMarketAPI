using System;
using System.Threading.Tasks;

namespace Unico.Core.Test.Services
{
    internal class MarketService
    {
        public MarketService()
        {
        }

        public async Task<MarketRequest> GetMarketsAsync()
        {
            var response = new MarketRequest();

            return response;
        }
    }
}