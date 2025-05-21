using PruebaLafise.Domain.Entities;

namespace PruebaLafise.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> Create(BankAccount account);
        Task<BankAccount> CheckAccountNumber(string accountNumber);
        Task<decimal> GetBalance(string accountNumber);
        Task<decimal> UpdateBalance(string accountNumber, decimal newBalance);
        Task<bool> Deposit(AccountMovements movement);
        Task<bool> Withdrawal(AccountMovements movement);
        Task<TransactionResume> Resume(int userId, string accountNumber);
    }
}
