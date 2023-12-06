using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steamity.Models.InputModels;
using Steamity.Models.ViewModels.GameViewModels;
using Steamity.Services.Contracts;

namespace Steamity.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGamesService _gamesService;
        

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }
        public IActionResult AllGames()
        {
            var viewModel = new GamesListViewModel
            {
                Games = _gamesService.GetAll(),
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateGameInputModel();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            await this._gamesService.CreateAsync(input);
            
            return this.RedirectToAction("AllGames", "Games");
        }

        [Route("Games/id")]
        public IActionResult GetById(int id)
        {
            SingleGameViewModel game = _gamesService.GetById(id);

            if (game == null)
            {
                return this.NotFound();
            }

            return View(game);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var viewModel = _gamesService.GetEdit(id);


            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAsync(EditGameInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            await _gamesService.EditAsync(input, id);

            return RedirectToAction("AllGames", "Games");

        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var viewModel = _gamesService.GetEdit(id);


            if (viewModel == null)
            {
                return NotFound();
            }


            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _gamesService.Delete(id);

            return this.RedirectToAction("AllGames", "Games");

        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            var game = _gamesService.GetGameByTitle(searchTerm);

            if (game == null)
            {
                return NotFound();
            }

            return RedirectToAction("GetById", new { id = game.GameId });
        }

        [HttpPost]
        public IActionResult PartialSearchByTitle(string searchTerm)
        {
            var games = _gamesService.GetGamesByPartialTitle(searchTerm);

            if (games == null || !games.Any())
            {
                return NotFound();
            }

            var viewModel = new GamesListViewModel
            {
                Games = games
            };

            return PartialView("_GamesTable", viewModel);
        }

        [HttpPost]
        public IActionResult PartialSearch(string searchTerm, string releaseDate)
        {
            IEnumerable<GameListViewModel> games;

            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(releaseDate))
            {
                games = _gamesService.GetGamesByPartialTitleAndReleaseDate(searchTerm, releaseDate);
            }
            else if (!string.IsNullOrEmpty(searchTerm))
            {
                games = _gamesService.GetGamesByPartialTitle(searchTerm);
            }
            else if (!string.IsNullOrEmpty(releaseDate))
            {
                games = _gamesService.GetGamesByReleaseDate(releaseDate);
            }
            else
            {
                games = Enumerable.Empty<GameListViewModel>();
            }

            var viewModel = new GamesListViewModel
            {
                Games = games
            };

            return PartialView("_GamesTable", viewModel);
        }

    }
}
