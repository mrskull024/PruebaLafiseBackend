namespace PruebaLafise.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Genre { get; set; }
        public decimal Income { get; set; }
    }
}
