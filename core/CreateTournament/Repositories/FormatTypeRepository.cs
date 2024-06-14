using AutoMapper;
using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class FormatTypeRepository : IFormatTypeRepository<FormatType>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        public FormatTypeRepository( DataContext dataContext, IMapper mapper) 
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<FormatType> Create(FormatType formatType)
        {
            var newFormatType = mapper.Map<FormatType>(formatType);
            _dataContext.FormatTypes.Add(newFormatType);
            await _dataContext.SaveChangesAsync();
            return newFormatType;
        }

        public async Task<FormatType> Delete(int id)
        {
            var formatType = await _dataContext.FormatTypes
                .Where(x=>x.Id == id)
                .FirstOrDefaultAsync();
            formatType.IsDeleted = true;
            await _dataContext.SaveChangesAsync();
            return formatType;
        }

        public async Task<FormatType> Update(FormatType formatType, bool incluDeleted = false)
        {
            var exists = await _dataContext.FormatTypes
                .FirstOrDefaultAsync(x => x.Id == formatType.Id && x.IsDeleted == incluDeleted); 
            if (exists == null)
            {
                return null;
            }
            else
            {
                exists.Name = formatType.Name;
            }
            await _dataContext.SaveChangesAsync();
            return exists;
        }
            
        public async Task<List<FormatType>> GetAll(bool incluDeleted = false)
        {
            return await _dataContext.FormatTypes
                .Where(x=>x.IsDeleted == incluDeleted)
                .ToListAsync();
        }

        public async Task<FormatType> GetByIdFormatType(int id, bool includeDeleted = false)
        {
            return await _dataContext.FormatTypes
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
        }

    }
}
