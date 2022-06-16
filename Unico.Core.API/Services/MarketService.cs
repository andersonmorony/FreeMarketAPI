using System;
using System.Threading.Tasks;
using Unico.Core.API.Models;

namespace Unico.Core.API.Services
{
    public  class MarketService
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