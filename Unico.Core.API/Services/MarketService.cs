using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public MarketService(AppDbContext dbContext, IMapper mapper, ILogger<MarketService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(IEnumerable<MarketResponse> markets, bool IsSuccess, string MsgError)> GetMarketsAsync()
        {
            _logger?.LogInformation("GetMarketsAsync was called");
            try
            {
                var result = await _dbContext.Markets.ToListAsync();
                if (result.Any())
                {
                    var response = _mapper.Map<IEnumerable<MarketResponse>>(result);
                    return (response, true, null);
                }
                _logger?.LogInformation("GetMarketsAsync was called but no data");
                return (null, false, "No Data");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }
        }

        public async Task<(MarketResponse markets, bool IsSuccess, string MsgError)> CreateMarketAsync(MarketRequest request)
        {
            _logger?.LogInformation("CreateMarketAsync was called");
            var model = _mapper.Map<Market>(request);
            try
            {
                _dbContext.Markets.Add(model);
                _dbContext.SaveChanges();
                _logger?.LogInformation("CreateMarketAsync was called and Market was created");

                var response = _mapper.Map<MarketResponse>(model);
                return (response, true, null);

            }catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string MsgError)> DeleteMarketAsync(string codReg)
        {
            try
            {
                _logger?.LogInformation("DeleteMarketAsync was called");
                var market = _dbContext.Markets.FirstOrDefault(m => m.REGISTRO == codReg);

                if(market != null)
                {
                    _dbContext.Markets.Remove(market);
                    await _dbContext.SaveChangesAsync();
                    _logger?.LogInformation($"DeleteMarketAsync was called and item with codReg {codReg} was Deleted");
                    return (true, null);
                }
                return (false, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(MarketResponse marketResponse, bool IsSuccess, string MsgError)> EditMarketAsync(int Id, MarketRequest marketRequest)
        {
            try
            {
                _logger?.LogInformation("EditMarketAsync was called");

                var market = _dbContext.Markets.FirstOrDefault(m => m.Id == Id);

                if(market != null)
                {
                    _mapper.Map(marketRequest, market);
                    await _dbContext.SaveChangesAsync();

                    var marketEdited = _mapper.Map<MarketResponse>(market);
                    _logger?.LogInformation($"EditMarketAsync was called and Edite the market with Id {Id}");
                    return (marketEdited, true, null);
                }
                _logger?.LogInformation("EditMarketAsync was called, but item not found");
                return (null, false, "Not found");

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }
        }
    }
}