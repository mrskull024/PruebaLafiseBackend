using Microsoft.EntityFrameworkCore;
using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;
using PruebaLafise.Infraestructure.Data;

namespace PruebaLafise.Infraestructure.Repository
{
    public class CatalogRepository(BackendDbContext dbContext) : ICatalogRepository
    {
        private readonly BackendDbContext _dbContext = dbContext;

        public async Task<List<Currencies>> GetCurrencies()
        {
            return await _dbContext.Currencies.ToListAsync();
        }

        public async Task<List<Genres>> GetGenres()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<List<TransactionTypes>> GetTransactionTypes()
        {
            return await _dbContext.TransactionTypes.ToListAsync();
        }
    }
}
