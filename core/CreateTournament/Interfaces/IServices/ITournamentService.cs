using CreateTournament.DTOs;
using CreateTournament.Models;

namespace CreateTournament.Interfaces.IServices
{
    public interface ITournamentService
    {
        Task<List<TournamentDTO>> GetList(int currentPage = 1, int pageSize = 5, string searchTerm = "", int idSportType = -1, bool incluDeleted = false);
        Task<int> GetCountList(string searchTerm = "", int idSportType = -1, bool incluDeleted = false);
        Task<TournamentDTO> GetByIdTournament(int id, bool incluDeleted = false);
        Task<List<TournamentDTO>> GetTourByUserId(int userId, bool incluDeleted = false);
        Task<TournamentDTO> Create(TournamentDTO tournamentDTO);
        Task<TournamentDTO> Update(TournamentDTO tournamentDTO, bool incluDeleted = false);
        Task<TournamentDTO> UpdateView(TournamentDTO tournamentDTO, bool incluDeleted = false);

        Task<bool> Delete(int id);
        Task<List<TournamentDTO>> SearchTournaments(string searchTerm = "", int idSportType = -1, bool incluDeleted = false);
    }
}
