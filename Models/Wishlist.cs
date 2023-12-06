namespace Steamity.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public string? UserId { get; set; }
        public SteamityUser? User { get; set; }

        public ICollection<WishlistGame>? WishlistGames { get; set; }
        
    }
}
