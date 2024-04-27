using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface ITeamService
    {
        Task<TeamDTO> CreateAsync(TeamDTO teamDTO);
        Task<TeamDTO> UpdateAsync(int id, string name);
        Task<List<TeamDTO>> GetAllByIdTournamentAsync(int IdTournament);
        Task<TeamDTO> GetTeamByIdAsync(int id);
        Task<TeamDTO> FindTeamByIdAsync(int id);

    }
}
