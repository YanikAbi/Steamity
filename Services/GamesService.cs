using Steamity.Data;
using Steamity.Models;
using Steamity.Models.InputModels;
using Steamity.Models.ViewModels.GameViewModels;
using Steamity.Services.Contracts;

namespace Steamity.Services
{
    public class GamesService : IGamesService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GamesService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(CreateGameInputModel input)
        {
            var game = new Game()
            {
                Title = input.Title,
                Genre = input.Genre.ToString(),
                Price = input.Price,
                Description = input.Description,
                ReleaseDate = input.ReleaseDate.ToString(),
            };

            if (!_dbContext.Games.Any(x => x.Title == game.Title))
            {
                await _dbContext.Games.AddAsync(game);
            }

            await _dbContext.SaveChangesAsync();
        }

        //Gets the Edit (view) page of an game
        public EditGameInputModel GetEdit(int id)
        {
            var game = _dbContext.Games.Find(id);

            var viewModel = new EditGameInputModel()
            {
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Id = id
            };

            return viewModel;
        }

        // Updates the game information based on the input in the Edit page
        public async Task EditAsync(EditGameInputModel input, int id)
        {
            var game = await _dbContext.Games.FindAsync(id);

            if (game == null)
            {
                return;
            }

            game.Title = input.Title;
            game.Description = input.Description;
            game.Price = input.Price;
            game.ReleaseDate = input.ReleaseDate;


            _dbContext.Games.Update(game);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var game = await _dbContext.Games.FindAsync(id);

            _dbContext.Games.Remove(game);

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<GameListViewModel> GetAll()
        {
            var games = _dbContext.Games.ToList();
            var result = games.Select(x => new GameListViewModel
            {
                Id = x.GameId,
                Title = x.Title,
                Price = x.Price,
                ReleaseDate = x.ReleaseDate,
                ImageId = x.Image
                
            });

            return result;
        }

        public SingleGameViewModel GetById(int id)
        {
            var game = _dbContext.Games.Where(x => x.GameId == id);
            var result = game.Select(x => new SingleGameViewModel
            {
                Id = x.GameId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                ReleaseDate = x.ReleaseDate
            }).FirstOrDefault();

            return result;

        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Game GetGameByTitle(string title)
        {
            return _dbContext.Games.FirstOrDefault(x => x.Title == title);
        }

        public IEnumerable<GameListViewModel> GetGamesByPartialTitle(string partialTitle)
        {
            return _dbContext.Games
                .Where(x => x.Title.Contains(partialTitle))
                .Select(x => new GameListViewModel
                {
                    Id = x.GameId,
                    Title = x.Title,
                    ImageId = x.Image,
                    Price = x.Price,
                    ReleaseDate = x.ReleaseDate
                });
        }

        public IEnumerable<GameListViewModel> GetGamesByPartialTitleAndReleaseDate(string partialTitle, string releaseDate)
        {
            var query = _dbContext.Games.AsQueryable();

            if (!string.IsNullOrEmpty(partialTitle))
            {
                query = query.Where(x => x.Title.Contains(partialTitle));
            }

            if (!string.IsNullOrEmpty(releaseDate))
            {
                query = query.Where(x => x.ReleaseDate == releaseDate); // Adjust the comparison based on the actual string format
            }

            return query.Select(x => new GameListViewModel
            {
                Id = x.GameId,
                Title = x.Title,
                ImageId = x.Image,
                Price = x.Price,
                ReleaseDate = x.ReleaseDate
            });
        }

        public IEnumerable<GameListViewModel> GetGamesByReleaseDate(string releaseDate)
        {
            return _dbContext.Games
                .Where(x => x.ReleaseDate == releaseDate)
                .Select(x => new GameListViewModel
                {
                    Id = x.GameId,
                    Title = x.Title,
                    ImageId = x.Image,
                    Price = x.Price,
                    ReleaseDate = x.ReleaseDate
                });
        }

       
    }
}
