using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaLafise.Domain.Entities
{
    public class AccountMovements
    {
        public Guid TransactionId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; }
        public decimal MovementAmmount { get; set; }
        public int TransactionType { get; set; }
        [NotMapped]
        public string? TransactionDescription { get; set; }
        public decimal CurrentBalance { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
