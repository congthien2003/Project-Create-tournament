using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IMatchResultRepository<TMatchResult> where TMatchResult : MatchResult
    {
        Task<TMatchResult> CreateAsync(TMatchResult matchResult);
        Task<TMatchResult> UpdateAsync(int id ,TMatchResult matchResult, bool includeDeleted = false);
        Task<TMatchResult> GetMatchResultById(int id, bool includeDeleted = false);
        Task<TMatchResult> GetMatchResultByIdMatch(int id, bool includeDeleted = false);

    }
}
