using CreateTournament.DTOs;
using CreateTournament.Models;

namespace CreateTournament.Interfaces.IServices
{
    public interface IPlayerStatService
    {
        Task<PlayerStatsDTO> CreateAsync(PlayerStatsDTO playerStatsDTO);
        Task<PlayerStatsDTO> UpdateByIdAsync(int id, PlayerStatsDTO playerStats);
        Task<PlayerStatsDTO> GetByIdAsync(int id);
        Task<List<PlayerStatsDTO>> GetAllByIdMatchAsync(int id);
        Task<List<PlayerStatsDTO>> GetAllByIdPlayerAsync(int id);
        Task<PlayerStatsDTO> GetByIdMatchAndIdPlayerAsync(int idMatch, int idPlayer);
        Task<List<PlayerStatsDTO>> GetAllByIdTournamentAsync(int id);
        Task<List<PlayerStatsDTO>> GetAllByIdPlayerScoreAsync(int id);

    }

}
