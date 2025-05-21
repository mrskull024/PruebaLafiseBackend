using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Domain.Interfaces
{
    public interface ICatalogRepository
    {
        Task<List<Genres>> GetGenres();
        Task<List<Currencies>> GetCurrencies();
        Task<List<TransactionTypes>> GetTransactionTypes();
    }
}
