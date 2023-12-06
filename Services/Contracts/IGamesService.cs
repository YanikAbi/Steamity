using Steamity.Models;
using Steamity.Models.InputModels;
using Steamity.Models.ViewModels.GameViewModels;

namespace Steamity.Services.Contracts
{
    public interface IGamesService
    {
        public Task CreateAsync(CreateGameInputModel input);

        public IEnumerable<GameListViewModel> GetAll();

        int GetCount();

        public EditGameInputModel GetEdit(int id);
        public Task EditAsync(EditGameInputModel input, int id);

        public SingleGameViewModel GetById(int id);

        public Task Delete(int id);

        Game GetGameByTitle(string title);

        IEnumerable<GameListViewModel> GetGamesByPartialTitle(string partialTitle);

        IEnumerable<GameListViewModel> GetGamesByPartialTitleAndReleaseDate(string partialTitle, string releaseDate);

        IEnumerable<GameListViewModel> GetGamesByReleaseDate(string releaseDate);
    }
}
