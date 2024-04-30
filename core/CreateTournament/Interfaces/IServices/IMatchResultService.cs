using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface IMatchResultService
    {
        Task<MatchResultDTO> CreateAsync(MatchResultDTO matchResultDTO);
        Task<MatchResultDTO> UpdateAsync(int id,MatchResultDTO matchResultDTO);
        Task<MatchResultDTO> GetMatchResultById(int id);
        Task<MatchResultDTO> GetMatchResultByIdMatch(int id);
    }
}
