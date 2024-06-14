using AutoMapper;
using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class SportTypeRepository : ISportTypeRepository<SportType>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public SportTypeRepository(DataContext dataContext, IMapper mapper) 
        {
            this._dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<SportType> Create(SportType sportType)
        {
            var newSportType = mapper.Map<SportType>(sportType);
            _dataContext.SportTypes.Add(newSportType);
            await _dataContext.SaveChangesAsync();
            return newSportType;
        }

        public async Task<SportType> Delete(int id)
        {
            var sportType = await _dataContext.SportTypes
                .Where(x=>x.Id == id)
                .FirstOrDefaultAsync();
            sportType.IsDeleted = true;
            await _dataContext.SaveChangesAsync();
            return sportType;
        }

        public async Task<SportType> Update(SportType sportType, bool incluDeleted = false)
        {
            var exists = await _dataContext.SportTypes
                .FirstOrDefaultAsync(x=>x.Id == sportType.Id && x.IsDeleted == incluDeleted);
            if (exists == null)
            {
                return null;
            }
            else
            {
                exists.Name = sportType.Name;
            }
            await _dataContext.SaveChangesAsync();
            return exists;

        }

        public async Task<List<SportType>> GetAll(bool incluDeleted = false)
        {
            return await _dataContext.SportTypes
                .Where(x=>x.IsDeleted==incluDeleted)
                .ToListAsync();
        }

        public async Task<SportType> GetByIdSportType(int id, bool incluDeleted = false)
        {
            return await _dataContext.SportTypes
                .FirstOrDefaultAsync(x=>x.Id==id && x.IsDeleted==incluDeleted);
        }
    }
}
