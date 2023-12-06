using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steamity.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Genre { get; set; }

        public string? ReleaseDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

        public string? Platform { get; set; }

        public string? Image { get; set; }

        public ICollection<WishlistGame>? WishlistGames { get; set; }

        public ICollection<BoughtGame>? BoughtByUser { get; set; }

    }
}
