using Microsoft.EntityFrameworkCore;
using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Application
{
    public interface IApplicationDbContext
    {
        DbSet<UserProfile> Users { get; set; }
        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<AccountMovements> AccountMovements { get; set; }

        //Catalogos
        DbSet<Genres> Genres { get; set; }
        DbSet<TransactionTypes> TransactionTypes { get; set; }
        DbSet<Currencies> Currencies { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
