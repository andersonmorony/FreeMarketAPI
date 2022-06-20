using System.Collections.Generic;
using System.Threading.Tasks;
using Unico.Core.API.Data;

namespace Unico.Core.API.RepositoryInterface
{
    public interface IMarketRepository
    {
        Task<IEnumerable<Market>> GetMarketsAsync();
        Task<Market> GetMarketByIdAsync(int id);
        Task<Market> AddMarketAsync(Market marketRequest);
        Task DeleteMarketAsync(Market marketRequest);
        Task UpdateMarketAsync(Market marketRequest);
        Task<IEnumerable<Market>> GetMarketsByNameAsync(string name);
        Task<IEnumerable<Market>> AddRangeMarketsAsync(IEnumerable<Market> markets);
    }
}
