using CreateTournament.DTOs.Auth;
using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        Task<TUser> Delete(int id);
        Task<TUser> GetByEmail(string email, bool includeDeleted = false);

        Task<TUser> CreateUser(RegisterDTO register);
        Task<TUser> GetByLogin(string email, string password);

        Task<List<TUser>> GetList(int currentPage = 1, int pageSize = 5, bool includeDeleted = false);
        Task<int> GetCountList(bool includeDeleted = false);

        Task<TUser> GetById(int id, bool includeDeleted = false);
        Task<TUser> Edit(TUser user);
        Task<TUser> UpdatePassword(int id, string password, bool includeDeleted = false);
    }
}
