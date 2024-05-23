using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class PlayerStatService : IPlayerStatService
    {
        private readonly IPlayerStatRepository<PlayerStats> _playerStatRepo;
        private readonly IMapper _mapper;
        private readonly ITournamentRepository<Tournament> _tournamentRepository;

        public PlayerStatService(IPlayerStatRepository<PlayerStats> playerStatRepository,
                                 IMapper mapper,
                                 ITournamentRepository<Tournament> tournamentRepository)
        {
            _playerStatRepo = playerStatRepository;
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }
        public async Task<PlayerStatsDTO> CreateAsync(PlayerStatsDTO playerStatsDTO)
        {
            var playerStat = _mapper.Map<PlayerStats>(playerStatsDTO);
            await _playerStatRepo.CreateAsync(playerStat);
            return _mapper.Map<PlayerStatsDTO>(playerStat);
        }

        public async Task<List<PlayerStatsDTO>> GetAllByIdMatchAsync(int id)
        {
            var playerStats = await _playerStatRepo.GetAllByIdMatchAsync(id);
            return _mapper.Map<List<PlayerStatsDTO>>(playerStats);
        }

        public async Task<List<PlayerStatsDTO>> GetAllByIdPlayerAsync(int id)
        {
            var playerStats = await _playerStatRepo.GetAllByIdPlayerAsync(id);
            return _mapper.Map<List<PlayerStatsDTO>>(playerStats);
        }

        public async Task<List<PlayerStatsDTO>> GetAllByIdPlayerScoreAsync(int id)
        {
            var playerStats = await _playerStatRepo.GetAllByIdPlayerScoreAsynsc(id);
            return _mapper.Map<List<PlayerStatsDTO>>(playerStats);
        }

        public async Task<List<PlayerStatsDTO>> GetAllByIdTournamentAsync(int id)
        {
            var playerStats = await _playerStatRepo.GetAllByIdTournamentAsync(id);
            return _mapper.Map<List<PlayerStatsDTO>>(playerStats);
        }

        public async Task<PlayerStatsDTO> GetByIdAsync(int id)
        {
            var playerStat = await _playerStatRepo.GetByIdAsync(id);
            return _mapper.Map<PlayerStatsDTO>(playerStat);
        }

        public async Task<PlayerStatsDTO> GetByIdMatchAndIdPlayerAsync(int idMatch, int idPlayer)
        {
            var playerStat = await _playerStatRepo.GetByIdMatchAndIdPlayerAsync(idMatch,idPlayer);
            return _mapper.Map<PlayerStatsDTO>(playerStat);
        }

        public async Task<PlayerStatsDTO> UpdateByIdAsync(int id, PlayerStatsDTO playerStats)
        {
            var playerStat = _mapper.Map <PlayerStats>(playerStats);
            await _playerStatRepo.UpdateByIdAsync(id, playerStat);
            return _mapper.Map<PlayerStatsDTO>(playerStat);
        }
    }
}
