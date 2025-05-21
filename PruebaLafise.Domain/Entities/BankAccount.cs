namespace PruebaLafise.Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int UserProfile { get; set; }
        public int Currency { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal InitialIncome { get; set; }
    }
}
