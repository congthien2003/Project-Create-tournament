using CreateTournament.DTOs;
using CreateTournament.Models;

namespace CreateTournament.Interfaces.IServices
{
    public interface ITeamService
    {
        Task<TeamDTO> CreateAsync(TeamDTO teamDTO);
        Task<TeamDTO> UpdateAsync(int id, string name);
        Task<TeamDTO> UpdateImage(int id, string path);

        Task<List<TeamDTO>> GetAllByIdTournamentAsync(int IdTournament);
        Task<TeamDTO> GetTeamByIdAsync(int id);
        Task<TeamDTO> FindTeamByIdAsync(int id);

    }
}
