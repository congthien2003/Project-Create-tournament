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
        Task<List<PlayerStatsDTO>> GetAllPlayStats(bool includeDeleted = false, int currentPage = 1, int pageSize = 10, string sortColumn = "", bool ascendingOrder = false);
        Int32 GetCountAllPlayerStats(string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false);

        Task<List<PlayerStatsDTO>> GetAllByIdTournamentTeamAsync(int id, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false);

        Task<List<PlayerStatsDTO>> GetAllPlayerStatsByTournamentAsync(int id, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false);

    }

}
