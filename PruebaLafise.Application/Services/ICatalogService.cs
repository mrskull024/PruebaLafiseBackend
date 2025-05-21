using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Application.Services
{
    public interface ICatalogService
    {
        Task<List<Genres>> GetGenres();
        Task<List<Currencies>> GetCurrencies();
        Task<List<TransactionTypes>> GetTransactionTypes();
    }
}
