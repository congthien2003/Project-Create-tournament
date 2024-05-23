﻿using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface IPlayerStatRepository<TPlayerStat> where TPlayerStat : PlayerStats
    {
        Task<TPlayerStat> CreateAsync(TPlayerStat playerStats);
        Task<TPlayerStat> UpdateByIdAsync(int id, TPlayerStat playerStats, bool includeDeleted = false);
        Task<TPlayerStat> GetByIdAsync(int id, bool inculdeDeleted = false);
        Task<List<TPlayerStat>> GetAllByIdMatchAsync(int id, bool includeDeleted = false);
        Task<List<TPlayerStat>> GetAllByIdPlayerAsync(int id, bool includeDeleted = false);
        Task<TPlayerStat> GetByIdMatchAndIdPlayerAsync(int idPlayer, int idMatch, bool inculdeDeleted = false);
        Task<List<TPlayerStat>> GetAllByIdTournamentAsync(int id, bool includeDeleted = false);
        Task<List<TPlayerStat>> GetAllByIdPlayerScoreAsynsc(int id, bool includeDeleted = false);

    }
}