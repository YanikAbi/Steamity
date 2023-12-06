using Steamity.Models;

public class WishlistGame
{
    public int WishlistId { get; set; }
    public int GameId { get; set; }
    public DateTime AddedToWishlistDate { get; set; }

    public Wishlist? Wishlist { get; set; }
    public Game? Game { get; set; }
}