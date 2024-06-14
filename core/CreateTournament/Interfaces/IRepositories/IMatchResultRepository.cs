using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IMatchResultRepository<TMatchResult> where TMatchResult : MatchResult
    {
        Task<TMatchResult> CreateAsync(TMatchResult matchResult);
        Task<TMatchResult> UpdateAsync(int id ,TMatchResult matchResult, bool includeDeleted = false);
        Task<TMatchResult> GetMatchResultById(int id, bool includeDeleted = false);
        Task<TMatchResult> GetMatchResultByIdMatch(int id, bool includeDeleted = false);
        Task<List<TMatchResult>> GetAllMatchResults(int idtour, bool includeDeleted = false);
        Task<List<TMatchResult>> Getlist(int idtour, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false);
        Int32 GetCount(int idtour, string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false);

    }
}
