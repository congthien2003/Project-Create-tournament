using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository<Team> _teamRepo;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository<Team> teamRepository,
                            IMapper mapper)
        {
            _teamRepo = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamDTO> CreateAsync(TeamDTO teamDTO)
        {
            var team = _mapper.Map<Team>(teamDTO);
            await _teamRepo.CreateAsync(team);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<TeamDTO> FindTeamByIdAsync(int id)
        {
            var team = await _teamRepo.FindByIdAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<List<TeamDTO>> GetAllByIdTournamentAsync(int IdTournament)
        {
            var teams = await _teamRepo.GetAllByIdTournamentAsync(IdTournament);
            return _mapper.Map<List<TeamDTO>>(teams);
        }

        public async Task<TeamDTO> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepo.GetTeamByIdAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<TeamDTO> UpdateAsync(int id, string name)
        {
            var team = await _teamRepo.UpdateAsync(id, name);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<TeamDTO> UpdateImage(int id, string path)
        {
            var team = await _teamRepo.UpdateImage(id, path);
            return _mapper.Map<TeamDTO>(team);
        }
    }
}
