using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Models;

namespace Unico.Core.API.Services
{
    public  class MarketService
    {
        private readonly AppDbContext _dbContext;

        public MarketService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(IEnumerable<Market> markets, bool IsSuccess, string MsgError)> GetMarketsAsync()
        {
            
            var response = await _dbContext.Markets.ToListAsync();

            if(response.Any())
            {
                return (response, true, null);
            }
            return (null, false, "No Data");

        }
    }
}