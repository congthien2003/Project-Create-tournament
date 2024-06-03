using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface IMatchResultService
    {
        Task<MatchResultDTO> CreateAsync(MatchResultDTO matchResultDTO);
        Task<MatchResultDTO> UpdateAsync(int id,MatchResultDTO matchResultDTO);
        Task<MatchResultDTO> GetMatchResultById(int id);
        Task<MatchResultDTO> GetMatchResultByIdMatch(int id);
        Task<List<MatchResultDTO>> GetAllMatchesAsync(int idtour);
        Task<List<MatchResultDTO>> GetAllMatchResult(int idtour, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false);
        Int32 GetCountAllMatchResult(int idtour, string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false);

    }
}
