using GameStore.Server.Data.Repositories.Interfaces;
using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Server.Data.Repositories.Implementation
{
    public class GameRepository : IGameRepository
    {
        private GameStorecontext _dbContext;
        public GameRepository(GameStorecontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Game>> GetGamesAsync(string? filter)
        {
            var games = _dbContext.Games.AsNoTracking();

            if (filter is not null)
            {
                games = games.Where(games => games.Name.Contains(filter) || games.Genre.Contains(filter));
            }

            return await games.ToListAsync();
        }
        public async Task<Game> GetGameByIdAsync(int id) => await _dbContext.Games.FindAsync(id);

        public async Task<int> AddGameAsync(Game game)
        {
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();
            return game.Id;
        }

        public async Task<bool> UpdateGameAsync(int id, Game updatedGame)
        {
            var game = await _dbContext.Games.FindAsync(id);

            if (game == null)
                return false;

            game.Name = updatedGame.Name;
            game.Genre = updatedGame.Genre;
            game.Price = updatedGame.Price;
            game.StockUnit = updatedGame.StockUnit;
            game.ReleaseDate = updatedGame.ReleaseDate;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await _dbContext.Games.FindAsync(id);

            if (game == null)
                return false;

            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
