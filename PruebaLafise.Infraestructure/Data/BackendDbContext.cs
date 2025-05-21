using Microsoft.EntityFrameworkCore;
using PruebaLafise.Application;
using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Infraestructure.Data
{
    public class BackendDbContext(DbContextOptions<BackendDbContext> options) : DbContext(options), IApplicationDbContext
    {
        //Catalogos
        public DbSet<Genres> Genres { get; set; }
        public DbSet<TransactionTypes> TransactionTypes { get; set; }
        public DbSet<Currencies> Currencies { get; set; }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<AccountMovements> AccountMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>(userEntity =>
            {
                userEntity.HasKey(x => x.Id);
                userEntity.Property(x => x.Name)
                          .IsRequired()
                          .HasMaxLength(500);
                userEntity.Property(x => x.BirthDate)
                          .IsRequired();
                userEntity.Property(x => x.BirthDate)
                          .IsRequired();
                userEntity.Property(x => x.Income)
                          .IsRequired();
            });

            modelBuilder.Entity<BankAccount>(bankEntity =>
            {
                bankEntity.HasKey(x => x.Id);
                bankEntity.Property(x => x.UserProfile)
                          .IsRequired();
                bankEntity.Property(x => x.AccountNumber)
                          .IsRequired()
                          .HasMaxLength(500);
                bankEntity.Property(x => x.InitialIncome)
                          .IsRequired();
            });

            modelBuilder.Entity<AccountMovements>(movementsEntity =>
            {
                movementsEntity.HasKey(x => x.TransactionId);
                movementsEntity.Property(x => x.AccountNumber)
                         .IsRequired()
                         .HasMaxLength(500);
                movementsEntity.Property(x => x.MovementDate)
                          .IsRequired();
                movementsEntity.Property(x => x.MovementAmmount)
                          .IsRequired();
                movementsEntity.Property(x => x.TransactionType)
                         .IsRequired();
                movementsEntity.Property(x => x.CurrentBalance)
                         .IsRequired();
                movementsEntity.Property(x => x.IsAuthorized);
            });
        }
    }
}
