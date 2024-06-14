using CreateTournament.Data;
using CreateTournament.DTOs;
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
            team.Eliminated = false;
            team.Point = 0;
            _context.Teams.Add(team); 
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> FindByIdAsync(int Id, bool includeDeleted = false)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == Id && t.IsDeleted == includeDeleted);
            if (team == null)
            {
                return null;
            }
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

        public async Task<Team> GetTeamByIdAsync(int Id, bool includeDeleted = false)
        {
            var exits = await _context.Teams.FirstOrDefaultAsync(t => t.Id == Id);
            if(exits == null)
            {
                return null;
            }
            return exits;
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

        public async Task<Team> UpdateImage(int id, string pathImg, bool includeDeleted = false)
        {
            var exits = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
            if (exits == null)
            {
                return null;
            }
            else
            {
                if (!string.IsNullOrEmpty(pathImg))
                {
                    exits.ImageTeam = pathImg;
                }
            }
            await _context.SaveChangesAsync();
            return exits;
        }
        public async Task<List<Team>> CreateListTeamAsync(int quantity, int idTournament)
        {
            if(quantity <= 0)
            {
                return null;
            }
            var teams = new List<Team>();
            for (int i = 1; i <= quantity; i++)
            {
                var team = new Team
                {
                    Name = $"#{i}",
                    TournamentId = idTournament,
                };
                teams.Add(team);
            }
            await _context.AddRangeAsync(teams);
            await _context.SaveChangesAsync();
            return teams;
        }
    }
}
