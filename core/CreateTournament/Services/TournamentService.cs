using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Repositories;

namespace CreateTournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository<Tournament> _tournamentRepository;
        private readonly IMapper mapper;

        public TournamentService(ITournamentRepository<Tournament> tournamentRepository,IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            this.mapper = mapper;
        }

        public async Task<TournamentDTO> Create(TournamentDTO tournamentDTO)
        {
            var newTournament = mapper.Map<Tournament>(tournamentDTO);
            var tournament = await _tournamentRepository.Create(newTournament);

            var createTournament = mapper.Map<TournamentDTO>(tournament);
            return createTournament;
        }

        public async Task<bool> Delete(int id)
        {
            await _tournamentRepository.Delete(id);
            return true;
        }


        public async Task<TournamentDTO> GetByIdTournament(int id, bool incluDeleted = false)
        {
            var tournament = await _tournamentRepository.GetByIdTournament(id,incluDeleted);
            return mapper.Map<TournamentDTO>(tournament);
        }

        public async Task<List<TournamentDTO>> GetTourByUserId(int userId, bool incluDeleted = false)
        {
            var tournament = await _tournamentRepository.GetTourByUserId(userId,incluDeleted);
            return mapper.Map<List<TournamentDTO>>(tournament) ;
        }

        public async Task<TournamentDTO> Update(TournamentDTO tournamentDTO, bool incluDeleted = false)
        {
            var tournament = mapper.Map<Tournament>(tournamentDTO);
            var editTournament = await _tournamentRepository.Update(tournament);
            return mapper.Map<TournamentDTO>(editTournament);
            
        }

        public async Task<TournamentDTO> UpdateView(TournamentDTO tournamentDTO, bool incluDeleted = false)
        {
            var tournament = mapper.Map<Tournament>(tournamentDTO);
            var editTournament = await _tournamentRepository.UpdateView(tournament);
            return mapper.Map<TournamentDTO>(editTournament);

        }
        public async Task<List<TournamentDTO>> SearchTournaments(string searchTerm = "", bool incluDeleted = false)
        {
            var tournaments = await _tournamentRepository.SearchTournaments(searchTerm, incluDeleted);
            return mapper.Map<List<TournamentDTO>>(tournaments);
        }

        public async Task<List<TournamentDTO>> GetList(int currentPage = 1, int pageSize = 5, string searchTerm = "", string sortColumn = "", bool ascSort = true, bool incluDeleted = false)
        {
            var tournaments = await _tournamentRepository.GetList(currentPage, pageSize, searchTerm, sortColumn, ascSort);
            return mapper.Map<List<TournamentDTO>>(tournaments);

        }

        public async Task<int> GetCountList(string searchTerm = "", bool incluDeleted = false)
        {
            var count = await _tournamentRepository.GetCount (searchTerm);
            return count;
        }
    }
}
