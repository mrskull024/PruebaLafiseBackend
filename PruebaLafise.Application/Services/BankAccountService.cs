using PruebaLafise.Domain.Entities;
using PruebaLafise.Domain.Interfaces;

namespace PruebaLafise.Application.Services
{
    public class BankAccountService(IBankAccountRepository repository) : IBankAccountService
    {
        private readonly IBankAccountRepository _repository = repository;

        public Task<BankAccount> Create(BankAccount account)
        {
            return _repository.Create(account);
        }

        public Task<BankAccount> CheckAccountNumber(string accountNumber)
        {
            return _repository.CheckAccountNumber(accountNumber);
        }

        public Task<decimal> GetBalance(string accountNumber)
        {
            return _repository.GetBalance(accountNumber);
        }

        public Task<decimal> UpdateBalance(string accountNumber, decimal newBalance)
        {
            return _repository.UpdateBalance(accountNumber, newBalance);
        }

        public Task<bool> Deposit(AccountMovements movement)
        {
            return _repository.Deposit(movement);
        }

        public Task<bool> Withdrawal(AccountMovements movement)
        {
            return _repository.Withdrawal(movement);
        }

        public Task<TransactionResume> Resume(int userId, string accountNumber)
        {
            return _repository.Resume(userId, accountNumber);
        }
    }
}
