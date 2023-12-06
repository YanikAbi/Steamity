using Microsoft.EntityFrameworkCore;
using Steamity.Data;
using Steamity.Models;
using Steamity.Models.ViewModels.GameViewModels.WishlistViewModels;
using Steamity.Services.Contracts;

namespace Steamity.Services
{
    public class WishlistService : IWishlistService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishlistService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }



        public IEnumerable<WishlistGameViewModel> GetUserWishlist(string userId)
        {
            var user = GetUser(userId);
            var userGames = _dbContext.Wishlists
                            .Include(w => w.WishlistGames).ThenInclude(wg => wg.Game).Where(w => w.UserId == userId).ToList();


            var viewModel = userGames.SelectMany(w => w.WishlistGames.Select(wg => new WishlistGameViewModel
            {
                Id = wg.GameId,
                DateAdded = wg.AddedToWishlistDate,
                Title = wg.Game.Title,
                Description = wg.Game.Description,
            })).ToList();

            return viewModel;
        }

        public async Task AddGameToWishList(int id, string userId)
        {
            var game = await _dbContext.Games.FindAsync(id);

            WishlistGame wishlistGame = new WishlistGame
            {
                GameId = game.GameId,
                AddedToWishlistDate = DateTime.Now,
            };

            var user = _dbContext.SteamityUsers.Include(x => x.Wishlist).ThenInclude(wg => wg.WishlistGames).Where(x => x.Id == userId).FirstOrDefault();

            if (user.Wishlist != null)
            {
                user.Wishlist.WishlistGames.Add(wishlistGame);
            }
            else
            {
                user.Wishlist = new Wishlist();
                user.Wishlist.WishlistGames = new List<WishlistGame>
                {
                    wishlistGame
                };


            }

            await _dbContext.SaveChangesAsync();
        }

        private SteamityUser GetUser(string userId)
        {
            return _dbContext.SteamityUsers.Where(x => x.Id == userId).FirstOrDefault();
        }
    }
}
