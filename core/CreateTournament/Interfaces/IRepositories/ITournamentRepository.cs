using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface ITournamentRepository<TTournament> where TTournament : Tournament
    {
        Task<List<TTournament>> GetList(int currentPage = 1, int pageSize = 5, string searchTerm = "", int idSportType = -1, bool incluDeleted = false);

        Task<int> GetCount( string searchTerm = "", int idSportType = -1, bool incluDeleted = false);
        Task<TTournament> GetByIdTournament(int id, bool incluDeleted = false);
        Task<List<TTournament>> GetTourByUserId(int userId, bool incluDeleted = false);
        Task<TTournament> Create(TTournament tournament);
        Task<TTournament> Update(TTournament tournament,bool incluDeleted = false);
        Task<TTournament> UpdateView(TTournament tournament, bool incluDeleted = false);
        Task<TTournament> Delete(int id);
        Task<List<TTournament>> SearchTournaments(string searchTerm = "", int idSportType = -1 , bool incluDeleted = false);
    }
}
