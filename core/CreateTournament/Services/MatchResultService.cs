using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class MatchResultService : IMatchResultService
    {
        private readonly IMatchResultRepository<MatchResult> _matchResultRepo;
        private readonly IMapper _mapper;

        public MatchResultService(IMatchResultRepository<MatchResult> matchResultRepository,
                                    IMapper mapper)
        {
            _matchResultRepo = matchResultRepository;
            _mapper = mapper;
        }
        public async Task<MatchResultDTO> CreateAsync(MatchResultDTO matchResultDTO)
        {
            var matchResult = _mapper.Map<MatchResult>(matchResultDTO);
            await _matchResultRepo.CreateAsync(matchResult);
            return _mapper.Map<MatchResultDTO>(matchResult);
        }

        public async Task<MatchResultDTO> GetMatchResultById(int id)
        {
            var matchResult = await _matchResultRepo.GetMatchResultById(id);
            return _mapper.Map<MatchResultDTO>(matchResult);
        }

        public async Task<MatchResultDTO> GetMatchResultByIdMatch(int id)
        {
            var matchResult = await _matchResultRepo.GetMatchResultByIdMatch(id);
            return _mapper.Map<MatchResultDTO>(matchResult);
        }

        public async Task<MatchResultDTO> UpdateAsync(int id, MatchResultDTO matchResultDTO)
        {
            var matchResult = _mapper.Map<MatchResult>(matchResultDTO);
            await _matchResultRepo.UpdateAsync(id, matchResult);
            return _mapper.Map<MatchResultDTO>(matchResult);
        }
    }
}
