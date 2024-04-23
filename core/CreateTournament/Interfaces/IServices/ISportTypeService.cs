using CreateTournament.DTOs;

namespace CreateTournament.Interfaces.IServices
{
    public interface ISportTypeService
    {
        Task<List<SportTypeDTO>> GetAll(bool incluDeleted = false);
        Task<SportTypeDTO> GetByIdSportType(int id, bool incluDeleted = false);
        Task<SportTypeDTO> Create(SportTypeDTO sportTypeDTO);
        Task<SportTypeDTO> Update(SportTypeDTO sportTypeDTO, bool incluDeleted = false);
        Task<bool> Delete(int id);
    }
}
