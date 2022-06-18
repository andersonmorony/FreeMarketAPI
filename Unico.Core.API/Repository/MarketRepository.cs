using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.RepositoryInterface;

namespace Unico.Core.API.Repository
{
    public class MarketRepository : IMarketRepository
    {
        private readonly AppDbContext _dbContext;

        public MarketRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<Market> AddMarketAsync(Market marketRequest)
        {
            _dbContext.Markets.Add(marketRequest);
            await _dbContext.SaveChangesAsync();

            return marketRequest;
        }

        public async Task<IEnumerable<Market>> AddRangeMarketsAsync(IEnumerable<Market> markets)
        {
            await _dbContext.Markets.AddRangeAsync(markets);
            await _dbContext.SaveChangesAsync();

            return markets;
        }

        public async Task DeleteMarketAsync(Market marketRequest)
        {
            _dbContext.Markets.Remove(marketRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Market> GetMarketByIdAsync(int id)
        {
            return await _dbContext.Markets.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            return await _dbContext.Markets.ToListAsync();
        }

        public async Task<IEnumerable<Market>> GetMarketsByNameAsync(string name)
        {
            return await _dbContext.Markets.Where(m => m.NOME_FEIRA == name).ToListAsync();
        }

        public async Task UpdateMarketAsync(Market marketRequest)
        {
            _dbContext.Update(marketRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
