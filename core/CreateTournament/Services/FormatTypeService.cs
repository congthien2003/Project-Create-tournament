using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Repositories;

namespace CreateTournament.Services
{
    public class FormatTypeService : IFormatTypeService
    {
        private readonly IFormatTypeRepository<FormatType> _formatTypeRepository;
        private readonly IMapper mapper;
        public FormatTypeService(IFormatTypeRepository<FormatType> formatTypeRepository, IMapper mapper)
        {
            _formatTypeRepository = formatTypeRepository;
            this.mapper = mapper;
        }

        public async Task<FormatTypeDTO> Create(FormatTypeDTO formatTypeDTO)
        {
            var newFormatType = mapper.Map<FormatType>(formatTypeDTO);
            var formatType = await _formatTypeRepository.Create(newFormatType);
            // Map the created FormatType back to FormatTypeDTO
            var createdFormatTypeDTO = mapper.Map<FormatTypeDTO>(formatType);
            return createdFormatTypeDTO;
        }

        public async Task<bool> Delete(int id)
        {
            await _formatTypeRepository.Delete(id);
            return true;
        }

        public async Task<FormatTypeDTO> Update( FormatTypeDTO formatTypeDTO, bool isDeleted = false)
        {
            var formatType = mapper.Map<FormatType>(formatTypeDTO);
            var edit = await _formatTypeRepository.Update(formatType);
            return mapper.Map<FormatTypeDTO>(edit);

        }

        public async Task<List<FormatTypeDTO>> GetAll(bool includeDeleted = false)
        {
            var formatType = await _formatTypeRepository.GetAll(includeDeleted);
            return mapper.Map<List<FormatTypeDTO>>(formatType);
        }

        public async Task<FormatTypeDTO> GetByIdFormatType(int id, bool includeDeleted = false)
        {
            var formatType = await _formatTypeRepository.GetByIdFormatType(id,includeDeleted);
            return mapper.Map<FormatTypeDTO>(formatType);
        }
    }
}
