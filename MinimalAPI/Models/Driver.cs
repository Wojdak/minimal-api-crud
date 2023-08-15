using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Nationality { get; set; } = string.Empty;
        public required int RacingNumber { get; set; }
        public string Team { get; set; } = string.Empty;
    }
}
