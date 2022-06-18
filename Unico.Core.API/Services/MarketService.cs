﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.Models;
using Unico.Core.API.RepositoryInterface;
using Unico.Core.API.ServicesInterface;

namespace Unico.Core.API.Services
{
    public  class MarketService : IMarketServices
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMarketRepository _marketRepository;

        public MarketService(IMapper mapper, ILogger<MarketService> logger, IMarketRepository marketRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _marketRepository = marketRepository;
        }

        public async Task<(IEnumerable<MarketResponse> markets, bool IsSuccess, string MsgError)> GetMarketsAsync()
        {
            _logger?.LogInformation("GetMarketsAsync was called");
            try
            {
                var result = await _marketRepository.GetMarketsAsync();
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
                var response = await _marketRepository.AddMarketAsync(model);
                _logger?.LogInformation("CreateMarketAsync was called and Market was created");

                var responseMapped = _mapper.Map<MarketResponse>(response);
                return (responseMapped, true, null);

            }catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, string MsgError)> DeleteMarketAsync(int id)
        {
            try
            {
                _logger?.LogInformation("DeleteMarketAsync was called");
                Market market = await _marketRepository.GetMarketByIdAsync(id);

                if(market != null)
                {
                    await _marketRepository.DeleteMarketAsync(market);
                    _logger?.LogInformation($"DeleteMarketAsync was called and item with codReg {id} was Deleted");
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

                Market market = await _marketRepository.GetMarketByIdAsync(Id);

                if (market != null)
                {
                    await _marketRepository.UpdateMarketAsync(_mapper.Map(marketRequest, market));

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

        public async Task<(IEnumerable<MarketResponse> marketResponse, bool IsSuccess, string MsgError)> GetMarketsByNameAync(string marketName)
        {
            try
            {
                _logger?.LogInformation("GetMarketsByNameAync was called");
                var markets = await _marketRepository.GetMarketsByNameAsync(marketName);

                if (markets.Any())
                {
                    var response = _mapper.Map<IEnumerable<MarketResponse>>(markets);

                    return (response, true, null);
                }
                _logger?.LogInformation("GetMarketsByNameAync was called, but items not found");
                return (null, false, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }
            
        }
        public async Task<(IEnumerable<MarketCsv> marketResponse, bool IsSuccess, string MsgError)> UploadCsvToCreateMarkets(IEnumerable<MarketCsv> request)
        {
            try
            {
                _logger?.LogInformation("UploadCsvToCreateMarkets was called");

                IEnumerable<Market> marketRequest = _mapper.Map<IEnumerable<Market>>(request);

                IEnumerable<Market> marketResponse = await _marketRepository.AddRangeMarketsAsync(marketRequest);

                IEnumerable<MarketCsv> marketResponseMapped = _mapper.Map<IEnumerable<MarketCsv>>(marketRequest);

                return (marketResponseMapped, true, null);
                    
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (null, false, ex.Message);
            }
        }
    }
}