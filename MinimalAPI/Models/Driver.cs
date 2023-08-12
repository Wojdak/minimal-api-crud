namespace MinimalAPI.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Nationality { get; set; } = string.Empty;
        public required int RacingNumber { get; set; }
        public string Team { get; set; } = string.Empty;
    }
}
