using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IFormatTypeRepository<TFormatType> where TFormatType : FormatType
    {
        Task<TFormatType> GetByIdFormatType(int id, bool includeDeleted = false);
        Task<List<TFormatType>> GetAll(bool incluDeleted = false);
        Task<TFormatType> Update(TFormatType formatType, bool incluDeleted = false);
        Task<TFormatType> Create(TFormatType formatType);
        Task<TFormatType> Delete(int id);

    }
}
