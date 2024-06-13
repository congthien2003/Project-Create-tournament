using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreateTournament.Repositories
{
    public class MatchResultRepository : IMatchResultRepository<MatchResult>
    {
        private readonly DataContext _context;

        public MatchResultRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<MatchResult> CreateAsync(MatchResult matchResult)
        {
            await _context.MatchResults.AddAsync(matchResult);
            await _context.SaveChangesAsync();
            var match = await _context.Matches.Where(obj => obj.Id == matchResult.MatchId).FirstOrDefaultAsync();
            var tournament = await _context.Tournaments.Where(obj => obj.Id == match.TournamentId).FirstOrDefaultAsync();


            switch (match.round)
            {
                case 5:
                    {
                        tournament.FinishAt = new DateTime();
                        break;
                    }
                case 4:
                     await finalMatch(tournament.Id, match.STT, matchResult);
                    break;
                case 3:
                    await semiFinalsMatch(tournament.Id, match.STT, matchResult);
                    break;
                case 2:
                    await quaterFinalsMatch(tournament.Id, match.STT, matchResult);
                    break;
            }
            return matchResult;
        }
        public Expression<Func<MatchResult, object>> GetSortColumnExpression(string? sortColumn)
        {
            switch (sortColumn)
            {
                case "scoret1":
                    return x => x.ScoreT1;
                case "scoret2":
                    return x => x.ScoreT2;
                case "idteamwin":
                    return x => x.IdTeamWin;
                case null:
                    return x => x.Id;
                default:
                    return x => x.Id;
            }
        }
        public async Task<List<MatchResult>> GetAllMatchResults(int idtour, bool includeDeleted = false)
        {
            var tournament = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == idtour && obj.IsDeleted == includeDeleted);
            if (tournament == null)
            {
                return null;
            }
            var matchs = await _context.Matches.Where(obj => obj.TournamentId == idtour).ToListAsync();
            List<MatchResult> matchResults = new List<MatchResult>();
            for (int i = 0; i < matchs.Count; i++)
            {
                var matchResult = await _context.MatchResults.Where(obj => obj.MatchId == matchs[i].Id).ToListAsync();
                matchResults.AddRange(matchResult);
            }
            return matchResults;
        }

        public async Task<List<MatchResult>> GetAllMatchResultsScore(bool includeDeleted = false)
        {
            return await _context.MatchResults
            .Where(obj => includeDeleted || !obj.IsDeleted && obj.ScoreT1 > 0 && obj.ScoreT2 > 0)
            .OrderByDescending(p => p.ScoreT1).Include("PlayerStats")
            .ToListAsync();
        }

        public async Task<MatchResult> GetMatchResultById(int id, bool includeDeleted = false)
        {
            var matchResult = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (matchResult == null)
            {
                return null;
            }
            return matchResult;

        }

        public async Task<MatchResult> GetMatchResultByIdMatch(int id, bool includeDeleted = false)
        {
            var matchResult = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.MatchId == id && obj.IsDeleted == includeDeleted);
            if (matchResult == null)
            {
                return null;
            }
            return matchResult;
        }

        public async Task<MatchResult> UpdateAsync(int id, MatchResult matchResult, bool includeDeleted = false)
        {
            var exits = await _context.MatchResults.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == includeDeleted);
            if (exits == null)
            {
                return null;
            }
            else
            {
                exits.ScoreT1 = matchResult.ScoreT1;
                exits.ScoreT2 = matchResult.ScoreT2;
                exits.IdTeamWin = matchResult.IdTeamWin;
                var match = await _context.Matches.Where(obj => obj.Id == matchResult.MatchId).FirstOrDefaultAsync();

                switch (match.round)
                {
                    case 5:
                        {
                            var tournament = await _context.Tournaments.Where(obj => obj.Id == match.TournamentId).FirstOrDefaultAsync();
                            tournament.FinishAt = new DateTime();
                            await _context.SaveChangesAsync();
                            break;
                        }
                    case 4:
                        await finalMatch(match.TournamentId, match.STT, matchResult);
                        break;
                    case 3:
                        await semiFinalsMatch(match.TournamentId, match.STT, matchResult);
                        break;
                    case 2:
                        await quaterFinalsMatch(match.TournamentId, match.STT, matchResult);
                        break;
                }
                return matchResult;
            }

        }

        // Handle by Semifinal match result
        public async Task finalMatch(int idTournament, int stt, MatchResult matchResult)
        {
            switch (stt)
            {
                case 1:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.Where(obj => obj.TournamentId == idTournament && obj.round == 5).FirstOrDefaultAsync();

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 5;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 2:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.Where(obj => obj.TournamentId == idTournament && obj.round == 5).FirstOrDefaultAsync();

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 5;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }
            }
        }

        // Handle by Quaterfinals match
        public async Task semiFinalsMatch(int idTournament, int stt, MatchResult matchResult)
        {
            switch(stt)
            {
                case 1:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 4 && obj.STT == 1);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 4;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 2:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 4 && obj.STT == 1);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 4;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 3:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 4 && obj.STT == 2);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 4;
                            newMatchtemp.STT = 2;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 4:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 4 && obj.STT == 2);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 4;
                            newMatchtemp.STT = 2;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }
            }
        }
        
        // Handle by round 16
        public async Task quaterFinalsMatch(int idTournament, int stt, MatchResult matchResult)
        {
            switch (stt)
            {
                case 1:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 1);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 2:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 1);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 1;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 3:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 2);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 2;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 4:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 2);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 2;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }
                case 5:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 3);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 3;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 6:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 3);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 3;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }
                case 7:
                    {
                        int teamWin1 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 4);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam1 = teamWin1;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = teamWin1;
                            newMatchtemp.IdTeam2 = 9999;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 4;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }

                case 8:
                    {
                        int teamWin2 = matchResult.IdTeamWin;

                        Match matchExists = await _context.Matches.FirstOrDefaultAsync(obj => obj.TournamentId == idTournament && obj.round == 3 && obj.STT == 4);

                        if (matchExists != null)
                        {
                            matchExists.IdTeam2 = teamWin2;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var newMatchtemp = new Match();
                            newMatchtemp.TournamentId = idTournament;
                            newMatchtemp.IdTeam1 = 9999;
                            newMatchtemp.IdTeam2 = teamWin2;
                            newMatchtemp.round = 3;
                            newMatchtemp.STT = 4;
                            await _context.Matches.AddAsync(newMatchtemp);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    }
            }
        }
        public async Task<List<MatchResult>> Getlist(int idtour, bool includeDeleted = false, int currentPage = 1, int pageSize = 5, string sortColumn = "", bool ascendingOrder = false)
        {
            var tournament = await _context.Tournaments.FirstOrDefaultAsync(obj => obj.Id == idtour && obj.IsDeleted == includeDeleted);
            if (tournament == null)
            {
                return null;
            }

            var listMatchResults = _context.MatchResults.AsQueryable().Where(obj => obj.Match.TournamentId == idtour);

            if (!includeDeleted)
            {
                listMatchResults = listMatchResults.Where(obj => !obj.IsDeleted);
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    listMatchResults = listMatchResults.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    listMatchResults = listMatchResults.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                listMatchResults = listMatchResults.OrderBy(x => x.Id);
            }
            return await listMatchResults.Skip(pageSize * currentPage - pageSize).Take(pageSize).ToListAsync();
        }

        public Int32 GetCount(int idtour, string sortColumn = "", bool ascendingOrder = false, bool incluDeleted = false)
        {
            var matchresult = _context.MatchResults.AsQueryable().Where(obj => obj.Match.TournamentId == idtour);
            if (!incluDeleted)
            {
                matchresult = matchresult.Where(obj => !obj.IsDeleted);
            }
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (ascendingOrder)
                {
                    matchresult = matchresult.OrderByDescending(GetSortColumnExpression(sortColumn));
                }
                else
                {
                    matchresult = matchresult.OrderBy(GetSortColumnExpression(sortColumn));
                }
            }
            else
            {
                matchresult = matchresult.OrderBy(x => x.Id);
            }
            return matchresult.Count();
        }
    }
}
