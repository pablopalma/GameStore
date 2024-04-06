using GameStore.Server.Models;

namespace GameStore.Server.Data.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<List<Game>> GetGamesAsync(string? filter);
        Task<Game> GetGameByIdAsync(int id);
        Task<int> AddGameAsync(Game game);
        Task<bool> UpdateGameAsync(int id, Game updatedGame);
        Task<bool> DeleteGameAsync(int id);
    }
}
