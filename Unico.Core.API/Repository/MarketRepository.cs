using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Data;
using Unico.Core.API.RepositoryInterface;

namespace Unico.Core.API.Repository
{
    /// <summary>
    /// Repository from Market
    /// </summary>
    public class MarketRepository : IMarketRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Inject database 
        /// </summary>
        /// <param name="appDbContext"></param>
        public MarketRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        /// <summary>
        /// Method to add new market
        /// </summary>
        /// <param name="marketRequest"></param>
        /// <returns>the market created</returns>
        public async Task<Market> AddMarketAsync(Market marketRequest)
        {
            _dbContext.Markets.Add(marketRequest);
            await _dbContext.SaveChangesAsync();

            return marketRequest;
        }
        /// <summary>
        /// Method called to add more that one market
        /// </summary>
        /// <param name="markets"></param>
        /// <returns>All markets reveiced by param after inserted in database</returns>
        public async Task<IEnumerable<Market>> AddRangeMarketsAsync(IEnumerable<Market> markets)
        {
            await _dbContext.Markets.AddRangeAsync(markets);
            await _dbContext.SaveChangesAsync();

            return markets;
        }
        /// <summary>
        /// Method to delete a market
        /// </summary>
        /// <param name="marketRequest"></param>
        /// <returns></returns>
        public async Task DeleteMarketAsync(Market marketRequest)
        {
            _dbContext.Markets.Remove(marketRequest);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Method to get a market by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A market found</returns>
        public async Task<Market> GetMarketByIdAsync(int id)
        {
            return await _dbContext.Markets.FirstOrDefaultAsync(m => m.Id == id);
        }
        /// <summary>
        /// Method to Get all market in the database
        /// </summary>
        /// <returns>All markets found</returns>
        public async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            return await _dbContext.Markets.ToListAsync();
        }
        /// <summary>
        /// Method to return one or more Market filted by Name (NOME_FEIRA)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>All markets found</returns>
        public async Task<IEnumerable<Market>> GetMarketsByNameAsync(string name)
        {
            return await _dbContext.Markets.Where(m => m.NOME_FEIRA == name).ToListAsync();
        }
        /// <summary>
        /// Method to update market
        /// </summary>
        /// <param name="marketRequest"></param>
        /// <returns></returns>
        public async Task UpdateMarketAsync(Market marketRequest)
        {
            _dbContext.Update(marketRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
