using Microsoft.EntityFrameworkCore;
using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;
using PruebaLafise.Infraestructure.Data;

namespace PruebaLafise.Infraestructure.Repository
{
    public class BankAccountRepository(BackendDbContext dbContext) : IBankAccountRepository
    {
        private readonly BackendDbContext _dbContext = dbContext;

        public async Task<BankAccount> Create(BankAccount account)
        {
            await _dbContext.BankAccounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> CheckAccountNumber(string accountNumber)
        {
            return await _dbContext.BankAccounts.AsNoTracking()
                .FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
        }

        public async Task<decimal> GetBalance(string accountNumber)
        {
            return await _dbContext.BankAccounts.AsNoTracking()
                .Where(u => u.AccountNumber == accountNumber)
                .Select(u => u.InitialIncome)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> UpdateBalance(string accountNumber, decimal newBalance)
        {
            return await _dbContext.BankAccounts
                .Where(model => model.AccountNumber == accountNumber)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.InitialIncome, newBalance));
        }

        public async Task<bool> Deposit(AccountMovements movement)
        {
            await _dbContext.AccountMovements.AddAsync(movement);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> Withdrawal(AccountMovements movement)
        {
            await _dbContext.AccountMovements.AddAsync(movement);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<TransactionResume> Resume(int userId, string accountNumber)
        {
            return new TransactionResume
            {
                Profile = await _dbContext.Users.AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Id == userId),
                BanckAccount = await _dbContext.BankAccounts.AsNoTracking()
                                .FirstOrDefaultAsync(b => b.UserProfile == userId),
                Movements = await _dbContext.AccountMovements.AsNoTracking()
                                .Where(m => m.AccountNumber == accountNumber)
                                .OrderByDescending(m => m.MovementDate)
                                .ToListAsync()
            };
        }
    }
}
