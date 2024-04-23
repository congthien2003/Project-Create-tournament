using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface ISportTypeRepository<TSportType> where TSportType : SportType
    {
        Task<List<TSportType>> GetAll(bool incluDeleted = false);
        Task<TSportType> GetByIdSportType(int id, bool incluDeleted = false);
        Task<TSportType> Create(TSportType sportType);
        Task<TSportType> Update(TSportType sportType, bool incluDeleted = false);
        Task<TSportType> Delete(int id);
    }
}
