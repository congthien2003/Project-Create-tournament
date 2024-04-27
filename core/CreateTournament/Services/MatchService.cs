using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository<Match> _matchRepo;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository<Match> matchRepository,
                            IMapper mapper)
        {
            _matchRepo = matchRepository;
            _mapper = mapper;
        }

        public async Task<MatchDTO> CreateAsync(MatchDTO matchDTO)
        {
            var match = _mapper.Map<Match>(matchDTO);
            await _matchRepo.CreateAsync(match);
            return _mapper.Map<MatchDTO>(match);
        }

        public async Task<List<MatchDTO>> CreateListMatchAsync(int idTournament)
        {
            var matches = await _matchRepo.CreateListMatchAsync(idTournament);
            return _mapper.Map<List<MatchDTO>>(matches);
        }

        public async Task<List<MatchDTO>> GetAllMatchesByIdTournamentAsync(int idTournament)
        {
            var matches = await _matchRepo.GetAllMatchesByIdTournamentAsync(idTournament);
            return _mapper.Map<List<MatchDTO>>(matches);
        }

        public async Task<MatchDTO> GetMatchByIdAsync(int id)
        {
            var match = await _matchRepo.GetMatchByIdAsync(id);
            return _mapper.Map<MatchDTO>(match);
        }

        public async Task<MatchDTO> UpdateMatchAsync(int id, MatchDTO matchDTO)
        {
            var match = _mapper.Map<Match>(matchDTO);
            await _matchRepo.UpdateMatchByIdAsync(id,match);
            return _mapper.Map<MatchDTO>(match);
        }
    }
}
