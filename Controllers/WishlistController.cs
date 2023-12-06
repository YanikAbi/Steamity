using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Steamity.Models;
using Steamity.Models.ViewModels.GameViewModels.WishlistViewModels;
using Steamity.Services.Contracts;

namespace Steamity.Controllers
{
    public class WishlistController : Controller
    {
        private readonly UserManager<SteamityUser> _userManager;
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService, UserManager<SteamityUser> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> GetWishlist()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var viewModel = new WishlistGamesViewModel
            {
                WishlistGames = _wishlistService.GetUserWishlist(user.Id),
            };

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);

        }

        [Authorize]
        public async Task<IActionResult> AddGameToWishlist(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _wishlistService.AddGameToWishList(id, user.Id);

            return RedirectToAction("GetWishlist", "Wishlist");
        }
    }
}
