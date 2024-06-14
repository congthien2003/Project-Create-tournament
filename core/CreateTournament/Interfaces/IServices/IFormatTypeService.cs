using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface IFormatTypeService
    {
        Task<FormatTypeDTO> GetByIdFormatType(int id, bool includeDeleted = false);
        Task<List<FormatTypeDTO>> GetAll(bool includeDeleted = false);
        Task<FormatTypeDTO> Create(FormatTypeDTO formatTypeDTO);
        Task<FormatTypeDTO> Update(FormatTypeDTO formatTypeDTO, bool isDeleted = false);
        Task<bool> Delete(int id);
    }
}
