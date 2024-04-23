using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;

namespace CreateTournament.Services
{
    public class SportTypeService : ISportTypeService
    {
        private readonly ISportTypeRepository<SportType> _sportTypeRepository;
        private readonly IMapper mapper;
        public SportTypeService(ISportTypeRepository<SportType> sportTypeRepository, IMapper mapper) 
        {
            _sportTypeRepository = sportTypeRepository;
            this.mapper = mapper;
        }

        public async Task<SportTypeDTO> Create(SportTypeDTO sportTypeDTO)
        {
            var newSportType = mapper.Map<SportType>(sportTypeDTO);
            var sportType = await _sportTypeRepository.Create(newSportType);
            var createSportType = mapper.Map<SportTypeDTO>(sportType);
            return createSportType;

        }

        public async Task<bool> Delete(int id)
        {
            await _sportTypeRepository.Delete(id);
            return true;
        }

        public async Task<SportTypeDTO> Update(SportTypeDTO sportTypeDTO, bool incluDeleted = false)
        {
            var sportType = mapper.Map<SportType>(sportTypeDTO);
            var edit = await _sportTypeRepository.Update(sportType);
            return mapper.Map<SportTypeDTO>(edit);
        }

        public async Task<List<SportTypeDTO>> GetAll(bool incluDeleted = false)
        {
            var sportType = await _sportTypeRepository.GetAll(incluDeleted);
            return mapper.Map<List<SportTypeDTO>>(sportType);
        }

        public async Task<SportTypeDTO> GetByIdSportType(int id, bool incluDeleted = false)
        {
            var sportType = await _sportTypeRepository.GetByIdSportType(id,incluDeleted);
            return mapper.Map<SportTypeDTO>(sportType);
        }
    }
}
