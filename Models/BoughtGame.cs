using System.ComponentModel.DataAnnotations;

namespace Steamity.Models
{
    public class BoughtGame
    {
        [Key]
        public int BoughtGameId { get; set; }
        public string? UserId { get; set; }
        public SteamityUser? User { get; set; }

        public int GameId { get; set; }
        public Game? Game { get; set; }

        public DateTime BoughtOnDate { get; set; }
    }
}
