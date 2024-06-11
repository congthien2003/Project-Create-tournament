using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class PlayerRepository : IPlayerRepository<Player>
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext dataContext)
        {
            _context = dataContext;

        }
        public async Task<Player> CreateAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player> DeleteAsync(int id)
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == id);
            if (player == null)
            {
                return null;
            }
            player.IsDeleted = true;
            await _context.SaveChangesAsync();
            return player;
            
        }

        public async Task<List<Player>> GetPlayerByName(string name, bool includeDeleted = false)
        {
            var players = await _context.Players.Where(x => x.Name.Contains(name) && x.IsDeleted == includeDeleted).ToListAsync();
            if(players.Count == 0)
            {
                return null;
            }
            return players;
        }

        public async Task<List<Player>> GetAllByIdTeamAsync(int idTeam, bool includeDeleted = false)
        {
            var players = await _context.Players.Where(x => x.TeamId == idTeam && x.IsDeleted == includeDeleted).ToListAsync();
            if(players.Count == 0)
            {
                return null;
            }
            return players;
        }

        public async Task<Player> UpdateAsync(int id, string name, bool includeDeleted = false)
        {
            var exits = await _context.Players.FirstOrDefaultAsync(x=> x.Id == id && x.IsDeleted == includeDeleted);
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

        public async Task<Player> GetPlayerById(int id, bool includeDeleted = false)
        {
            var exits = await _context.Players.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == includeDeleted);
            if(exits == null)
            {
                return null;
            }
            return exits;
        }

        public async Task<List<int>> GetTeamIdByTournamentAsync(int idtour)
        {
            var teams = await _context.Teams
                             .Where(t => t.TournamentId == idtour)
                             .Select(t => t.Id)
                             .ToListAsync();
            return teams;
        }
    }
}
