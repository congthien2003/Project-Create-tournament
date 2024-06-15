using CreateTournament.DTOs;

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
        Task<List<TeamDTO>> CreateListTeamAsync(int quantity, int IdTournament);
        Task<List<TeamDTO>> GetListTeamSwap(int idTournament, int round);

    }
}
