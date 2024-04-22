using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class TeamRepository : ITeamRepository<Team>
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Team> CreateAsync(Team team)
        {
            _context.Teams.Add(team); 
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<List<Team>> GetAllByIdTournamentAsync(int IdTournament,bool includeDeleted = false)
        {
            IQueryable<Team> teamsQuery = _context.Teams
            .Where(t => t.TournamentId == IdTournament && t.IsDeleted == includeDeleted);
            List<Team> teams = await teamsQuery.ToListAsync();
            if(teams.Count == 0)
            {
                return null;
            }
            return teams;
        }

        public async Task<Team> UpdateAsync(int id, string name, bool includeDeleted = false)
        {
            var exits = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
            if (exits == null)
            {
                return null;
            }
            else
            {
                exits.Name = name;
            }
            await _context.SaveChangesAsync();
            return exits;
        }
    }
}
