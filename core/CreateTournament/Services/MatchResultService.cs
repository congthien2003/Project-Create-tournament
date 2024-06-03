using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Repositories;

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

        public async Task<List<MatchResultDTO>> GetAllMatchesAsync(int idtour)
        {
            var matchresult = await _matchResultRepo.GetAllMatchResults(idtour);
            return _mapper.Map<List<MatchResultDTO>>(matchresult);
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
        public async Task<List<MatchResultDTO>> GetAllMatchResult(int idtour, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false)
        {
            var matchresultList = await _matchResultRepo.Getlist(idtour,includeDeleted, currentPage, pageSize, sortColumn, ascendingOrder);
            return _mapper.Map<List<MatchResultDTO>>(matchresultList);
        }
        public int GetCountAllMatchResult(int idtour, string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false)
        {
            var count = _matchResultRepo.GetCount(idtour,sortColumn, ascendingOrder, incluDeleted);
            return count;
        }
    }
}
