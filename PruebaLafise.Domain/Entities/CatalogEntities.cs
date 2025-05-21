namespace PruebaLafise.Domain.Entities
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Currencies
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class TransactionTypes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
