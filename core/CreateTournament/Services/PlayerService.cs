using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository<Player> _playerRepo;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository<Player> playerRepository,
                            IMapper mapper)
        {
            _playerRepo = playerRepository;
            _mapper = mapper;
        }

        public async Task<PlayerDTO> CreateAsync(PlayerDTO playerDTO)
        {
            var player = _mapper.Map<Player>(playerDTO);
            await _playerRepo.CreateAsync(player);
            return _mapper.Map<PlayerDTO>(player);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _playerRepo.DeleteAsync(id);
            return true;
        }

        public async Task<List<PlayerDTO>> GetAllByIdTeamAsync(int idTeam)
        {
            var players = await _playerRepo.GetAllByIdTeamAsync(idTeam);
            return _mapper.Map<List<PlayerDTO>>(players);
        }

        public async Task<PlayerDTO> GetPlayerById(int id)
        {
            var player = await _playerRepo.GetPlayerById(id);
            return _mapper.Map<PlayerDTO>(player);
        }

        public async Task<List<PlayerDTO>> GetPlayerByName(string name)
        {
            var players = await _playerRepo.GetPlayerByName(name);
            return _mapper.Map<List<PlayerDTO>>(players);
        }

        public async Task<List<int>> GetTeamIdByTournamentAsync(int idtour)
        {
            var players = await _playerRepo.GetTeamIdByTournamentAsync(idtour);
            return _mapper.Map<List<int>>(players);
        }

        public async Task<PlayerDTO> UpdateAsync(int id, string name)
        {
            var player = await _playerRepo.UpdateAsync(id,name);
            return _mapper.Map<PlayerDTO>(player);
        }
    }
}
