using Steamity.Models.ViewModels.GameViewModels.WishlistViewModels;

namespace Steamity.Services.Contracts
{
    public interface IWishlistService
    {
        public IEnumerable<WishlistGameViewModel> GetUserWishlist(string userId);

        public Task AddGameToWishList(int id, string userId); 
    }
}
