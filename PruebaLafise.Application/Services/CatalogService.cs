using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;

namespace PruebaLafise.Application.Services
{
    public class CatalogService(ICatalogRepository repository) : ICatalogService
    {
        private readonly ICatalogRepository _repository = repository;
        public Task<List<Currencies>> GetCurrencies()
        {
            return _repository.GetCurrencies();
        }

        public Task<List<Genres>> GetGenres()
        {
            return _repository.GetGenres();
        }

        public Task<List<TransactionTypes>> GetTransactionTypes()
        {
            return _repository.GetTransactionTypes();
        }
    }
}
