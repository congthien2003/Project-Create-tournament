using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IMatchRepository<TMatch> where TMatch : Match
    {
        Task<TMatch> CreateAsync(TMatch match);
        Task<List<TMatch>> CreateListMatchAsync(int idTournament);
        Task<TMatch> UpdateMatchByIdAsync(int id, TMatch match, bool includeDeleted = false);
        Task<TMatch> GetMatchByIdAsync(int id, bool includeDeleted = false);
        Task<List<TMatch>> GetAllMatchesByIdTournamentAsync(int idTournament, bool includeDeleted = false);
    }
}
