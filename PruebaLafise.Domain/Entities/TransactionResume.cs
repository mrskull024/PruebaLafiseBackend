namespace PruebaLafise.Domain.Entities
{
    public class TransactionResume
    {
        public UserProfile? Profile { get; set; }
        public BankAccount? BanckAccount { get; set; }
        public List<AccountMovements>? Movements { get; set; }
    }
}
