using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface ITournamentRepository<TTournament> where TTournament : Tournament
    {
        Task<List<TTournament>> GetAll(bool incluDeleted = false);
        Task<TTournament> GetByIdTournament(int id, bool incluDeleted = false);
        Task<List<TTournament>> GetTourByUserId(int userId, bool incluDeleted = false);
        Task<TTournament> Create(TTournament tournament);
        Task<TTournament> Update(TTournament tournament,bool incluDeleted = false);
        Task<TTournament> Delete(int id);
        Task<List<TTournament>> SearchTournaments(string searchTerm = "", int idSportType = -1 , bool incluDeleted = false);
    }
}
