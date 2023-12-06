using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steamity.Models
{
    public class SteamityUser : IdentityUser
    {

        public string? BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public double WalletBalance { get; set; }

        public Wishlist? Wishlist { get; set; }
        public ICollection<BoughtGame>? BoughtGames { get; set; }
    }
}
