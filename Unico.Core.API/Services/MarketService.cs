using AutoMapper;
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
        private readonly IMapper _mapper;

        public MarketService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public async Task<(Market markets, bool IsSuccess, string MsgError)> CreateMarketAsync(MarketRequest request)
        {
            var model = _mapper.Map<Market>(request);
            try
            {
                _dbContext.Markets.Add(model);
                _dbContext.SaveChanges();
                
                return (model, true, null);

            }catch(Exception ex)
            {
                return (null, false, ex.Message);
            }

        }
    }
}