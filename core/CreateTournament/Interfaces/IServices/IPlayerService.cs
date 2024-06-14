using CreateTournament.DTOs;
using CreateTournament.Models;

namespace CreateTournament.Interfaces.IServices
{
    public interface IPlayerService
    {
        Task<PlayerDTO> CreateAsync(PlayerDTO playerDTO);
        Task<PlayerDTO> UpdateAsync(int id, string name);
        Task<List<PlayerDTO>> GetAllByIdTeamAsync(int idTeam);
        Task<bool> DeleteAsync(int id);
        Task<List<PlayerDTO>> GetPlayerByName(string name);
        Task<PlayerDTO> GetPlayerById(int id);
        Task<List<int>> GetTeamIdByTournamentAsync(int idtour);

    }
}
