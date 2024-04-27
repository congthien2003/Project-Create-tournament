using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface IMatchService
    {
        Task<MatchDTO> CreateAsync(MatchDTO matchDTO);
        Task<List<MatchDTO>> CreateListMatchAsync(int idTournament );
        Task<MatchDTO> UpdateMatchAsync(int id,MatchDTO matchDTO);
        Task<MatchDTO> GetMatchByIdAsync(int id);
        Task<List<MatchDTO>> GetAllMatchesByIdTournamentAsync(int idTournament);
    }
}
