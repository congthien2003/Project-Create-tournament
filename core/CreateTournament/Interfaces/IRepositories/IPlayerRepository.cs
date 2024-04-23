using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IPlayerRepository<TPlayer> where TPlayer : Player
    {
        Task<TPlayer> CreateAsync(TPlayer player);
        Task<TPlayer> UpdateAsync(int id,string name, bool includeDeleted = false);
        Task<List<TPlayer>> GetAllByIdTeamAsync(int idTeam, bool includeDeleted = false);
        Task<TPlayer> DeleteAsync(int id);
        Task<List<TPlayer>> GetPlayerByName(string name, bool includeDeleted = false);
        Task<TPlayer> GetPlayerById(int id, bool includeDeleted = false);
    }
}
