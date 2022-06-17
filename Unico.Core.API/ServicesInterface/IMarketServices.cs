using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Models;

namespace Unico.Core.API.ServicesInterface
{
    public interface IMarketServices
    {
        Task<(IEnumerable<MarketResponse> markets, bool IsSuccess, string MsgError)> GetMarketsAsync();
        Task<(MarketResponse markets, bool IsSuccess, string MsgError)> CreateMarketAsync(MarketRequest request);
        Task<(bool IsSuccess, string MsgError)> DeleteMarketAsync(int id);
        Task<(MarketResponse marketResponse, bool IsSuccess, string MsgError)> EditMarketAsync(int Id, MarketRequest marketRequest);
        Task<(IEnumerable<MarketResponse> marketResponse, bool IsSuccess, string MsgError)> GetMarketsByNameAync(string marketName);

        Task<(IEnumerable<MarketCsv> marketResponse, bool IsSuccess, string MsgError)> UploadCsvToCreateMarkets(IEnumerable<MarketCsv> request);
    }
}
