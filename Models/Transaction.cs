using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steamity.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public string? UserId { get; set; }
        public int GameId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal AmountPaid { get; set; }

        public SteamityUser? User { get; set; }
        public Game?  Game { get; set; }
    }
}