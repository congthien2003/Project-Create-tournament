using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PlayerStatController : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService;
        private readonly IMatchResultService _matchResultService;
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly IMatchService _matchService;
        private readonly ITournamentService _tournamentService;

        public PlayerStatController(IPlayerStatService playerStatService,
                                    IMatchResultService matchResultService,
                                    IPlayerService playerService,
                                    ITeamService teamService,
                                    IMatchService matchService,
                                    ITournamentService tournamentService)
        {
            _playerStatService = playerStatService;
            _matchResultService = matchResultService;
            _playerService = playerService;
            _teamService = teamService;
            _matchService = matchService;
            _tournamentService = tournamentService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(PlayerStatsDTO playerStatsDTO)
        {
            var matchResult = await _matchResultService.GetMatchResultById(playerStatsDTO.MatchResultId);
            if (matchResult == null)
            {
                return BadRequest("Trận đấu chưa diễn ra");
            }
            var player = await _playerService.GetPlayerById(playerStatsDTO.PlayerId);
            if (player == null)
            {
                return BadRequest("Cầu thủ không tồn tại");
            }
            var match = await _matchService.GetMatchByIdAsync(matchResult.MatchId);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }

            if (player.TeamId != match.IdTeam1 && player.TeamId != match.IdTeam2)
            {
                return BadRequest("Cầu thủ không tồn tại trong team này");
            }
            if (player.TeamId == match.IdTeam1)
            {
                if (playerStatsDTO.Score > matchResult.ScoreT1)
                {
                    return BadRequest("Số bàn thắng lớn hơn kết quả trận đấu");
                }
                if (playerStatsDTO.Assists > matchResult.ScoreT1)
                {
                    return BadRequest("Số kiến tạo nhiều hơn kết quả trận đấu");
                }

            }
            else if (player.TeamId == match.IdTeam2)
            {
                if (playerStatsDTO.Score > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
                {
                    return BadRequest("Số bàn không hợp lệ");
                }
                if (playerStatsDTO.Assists > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
                {
                    return BadRequest("Số kiến tạo nhiều hơn kết quả trận đấu");
                }
            }
            var playerStat = await _playerStatService.CreateAsync(playerStatsDTO);
            if (playerStat == null)
            {
                return BadRequest("Tạo thất bại");
            }
            return Ok(playerStat);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateById(int id, PlayerStatsDTO playerStatsDTO)
        {
            var exits = await _playerStatService.GetByIdAsync(id);
            if (exits == null)
            {
                return BadRequest("Chỉ số người chơi trong trận không tồn tại");
            }
            var matchResult = await _matchResultService.GetMatchResultById(playerStatsDTO.MatchResultId);
            if (matchResult == null)
            {
                return BadRequest("Trận đấu chưa diễn ra");
            }
            var player = await _playerService.GetPlayerById(playerStatsDTO.PlayerId);
            if (player == null)
            {
                return BadRequest("Cầu thủ không tồn tại");
            }
            var match = await _matchService.GetMatchByIdAsync(matchResult.MatchId);
            if (match == null)
            {
                return BadRequest("Trận đấu không tồn tại");
            }

            if (player.TeamId != match.IdTeam1 && player.TeamId != match.IdTeam2)
            {
                return BadRequest("Cầu thủ không tồn tại trong team này");
            }
            if (player.TeamId == match.IdTeam1)
            {
                if (playerStatsDTO.Score > matchResult.ScoreT1)
                {
                    return BadRequest("Số bàn thắng lớn hơn kết quả trận đấu");
                }
                if (playerStatsDTO.Assists > matchResult.ScoreT1)
                {
                    return BadRequest("Số kiến tạo nhiều hơn kết quả trận đấu");
                }

            }
            else if (player.TeamId == match.IdTeam2)
            {
                if (playerStatsDTO.Score > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
                {
                    return BadRequest("Số bàn không hợp lệ");
                }
                if (playerStatsDTO.Assists > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
                {
                    return BadRequest("Số kiến tạo nhiều hơn kết quả trận đấu");
                }
            }
            var playerStat = await _playerStatService.UpdateByIdAsync(id, playerStatsDTO);
            var result = await _playerStatService.GetByIdAsync(id);
            if (playerStat == null)
            {
                return BadRequest("Cập nhật thất bại");
            }
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var playerStat = await _playerStatService.GetByIdAsync(id);
            if (playerStat == null)
            {
                return BadRequest("Không tồn tại thông tin");
            }
            return Ok(playerStat);
        }
        [HttpGet("idMatchResult")]
        public async Task<ActionResult> GetAllByIdMatch(int idmatchresult)
        {
            var playerStats = await _playerStatService.GetAllByIdMatchAsync(idmatchresult);
            if(playerStats == null)
            {
                return BadRequest("Trận đấu chưa diễn ra");
            }
            return Ok(playerStats);
        }
        [HttpGet("idPlayer")]
        public async Task<ActionResult> GetAllByIdPlayer(int idplayer)
        {
            var playerStats = await _playerStatService.GetAllByIdPlayerAsync(idplayer);
            if (playerStats == null)
            {
                return BadRequest("Cầu thủ chưa tham gia thi đấu");
            }
            return Ok(playerStats);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllByIdPlayerAndMatch(int idMatch, int idPlayer)
        {
            if (idMatch <= 0 || idPlayer <= 0)
            {
                return BadRequest("Thông tin truyền vô không hợp lệ");
            }
            var playerStat = await _playerStatService.GetByIdMatchAndIdPlayerAsync(idMatch, idPlayer);
            if (playerStat == null)
            {
                return BadRequest("Cầu thủ không tham gia trận đấu này");
            }

            return Ok(playerStat);
        }
        [HttpGet("idTournament")]
        public async Task<ActionResult> GetAllByIdTournament(int idTournament)
        {
            var playerStats = await _playerStatService.GetAllByIdTournamentAsync(idTournament);
            if (playerStats == null)
            {
                return BadRequest("Giải đấu chưa diễn ra");
            }
            return Ok(playerStats);
        }

        [HttpGet("Score/idTournament")]
        public async Task<ActionResult> GetAllByIdPlayerScore(int idTournament)
        {
            var tour = await _tournamentService.GetByIdTournament(idTournament);
            if (tour == null)
            {
                return BadRequest("Không tìm thấy giải đấu");

            }
            var playerstats = await _playerStatService.GetAllByIdPlayerScoreAsync(idTournament);
            var result = new
            {
                tour,
                playerstats,
            };
            return Ok(result);
        }

        [HttpGet("toplist")]
        public async Task<IActionResult> GetAllPlayStats(int idTournament, string currentPage = "1", string pageSize = "10", string? sortColumn= "", string ascendingOrder = "false")
        {
            var tour = await _tournamentService.GetByIdTournament(idTournament);
            if (tour == null)
            {
                return BadRequest("Không tìm thấy giải đấu");

            }
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _ascendingOrder = ascendingOrder == "true";
            var playstats = await _playerStatService.GetAllPlayStats(false, _currentPage, _pageSize, sortColumn, _ascendingOrder);
            var count = _playerStatService.GetCountAllPlayerStats(sortColumn, _ascendingOrder, false);
            int totalPage = count % _pageSize != 0 ? (count / _pageSize + 1) : (count / _pageSize);
            int totalRecords = playstats.Count;
            var result = new
            {
                tour,
                playstats,
                _currentPage,
                _pageSize,
                _ascendingOrder,
                totalPage,
                totalRecords,
                hasPrevious = _currentPage > 1,
                hasNext = _currentPage < totalPage,
            };
            return Ok(result);
        }

        [HttpGet("teamStats")]
        public async Task<ActionResult> GetTeamStatsByIdTour(int idTournament, string pageNumber = "1", string pageSize = "5", string? sortColumn = "", string ascendingOrder = "true")
        {
            var tour = await _tournamentService.GetByIdTournament(idTournament);
            if (tour == null)
            {
                return BadRequest("không tìm thấy giải đấu");
            }
            var teamIds = await _playerService.GetTeamIdByTournamentAsync(idTournament);
            if (teamIds == null || teamIds.Count == 0) {
                return BadRequest("không tìm thấy đội nào trong giải đấu này");
            }
            int _currentPage = int.Parse(pageNumber);
            int _pageSize = int.Parse(pageSize);
            bool _ascendingOrder = ascendingOrder == "true";

            var playerStats = await _playerStatService.GetPlayerStats(idTournament, false, sortColumn, _ascendingOrder);
            if (playerStats == null || playerStats.Count == 0)
            {
                return Ok(new { idTour = tour.Id, teams = new List<object>() });
            }

            var teamStats = teamIds.Select(teamId =>
            {
                var teamPlayerStats = playerStats.Where(ps => ps.Player.TeamId == teamId).ToList();
                int? score = teamPlayerStats.Sum(ps => ps.Score);
                int? assists = teamPlayerStats.Sum(ps => ps.Assists);
                int? yellowCard = teamPlayerStats.Sum(ps => ps.YellowCard);
                int? redCard = teamPlayerStats.Sum(ps => ps.RedCard);
                return new
                {
                    idteam = teamId,
                    Score = score,
                    Assists = assists,
                    YellowCard = yellowCard,
                    RedCard = redCard
                };
            }).AsQueryable();

            switch(sortColumn.ToLower())
            {
                case "score":
                    {
                        if (_ascendingOrder)
                        {
                            teamStats = teamStats.OrderByDescending(x => x.Score);
                        }
                        else
                        {
                            teamStats = teamStats.OrderBy(x => x.Score);
                        }
                        break;
                    }
                case "yellowcard":
                    {
                        if (_ascendingOrder)
                        {
                            teamStats = teamStats.OrderByDescending(x => x.YellowCard);
                        }
                        else
                        {
                            teamStats = teamStats.OrderBy(x => x.YellowCard);
                        }
                        break;
                    }
                case "redcard":
                    {
                        if (_ascendingOrder)
                        {
                            teamStats = teamStats.OrderByDescending(x => x.RedCard);
                        }
                        else
                        {
                            teamStats = teamStats.OrderBy(x => x.RedCard);
                        }
                        break;
                    }
                default:
                    {
                        if (_ascendingOrder)
                        {
                            teamStats = teamStats.OrderByDescending(x => x.Score);
                        }
                        else
                        {
                            teamStats = teamStats.OrderBy(x => x.Score);
                        }
                        break;
                    }
            }

            var result = new
            {   
                ListTeamStats = teamStats.Skip(_pageSize * _currentPage - _pageSize).Take(_pageSize),
            };

            return Ok(result);
        }

        [HttpGet("tourStats")]
        public async Task<ActionResult> GetTourStatsByIdTour(int idTournament)
        {
            var tour = await _tournamentService.GetByIdTournament(idTournament);
            if (tour == null)
            {
                return BadRequest("không tìm thấy giải đấu");
            }

            var playerStats = await _playerStatService.GetPlayerStats(idTournament);
            if (playerStats == null || playerStats.Count == 0)
            {
                return Ok(new { idTour = tour.Id, PlayerStatss = new { score = 0, assists = 0, yellowCard = 0, redCard = 0 } });
            }

            int? totalScore = playerStats.Sum(ps => ps.Score);
            int? totalAssists = playerStats.Sum(ps => ps.Assists);
            int? totalYellowCard = playerStats.Sum(ps => ps.YellowCard);
            int? totalRedCard = playerStats.Sum(ps => ps.RedCard);

            var result = new
            {
                TourStats = new
                {
                    score = totalScore,
                    assists = totalAssists,
                    yellowCard = totalYellowCard,
                    redCard = totalRedCard
                }
            };

            return Ok(result);
        }

        [HttpGet("playerStats")]
        public async Task<ActionResult> GetPlayerStatsByIdTour(int idTournament, string currentPage = "1", string pageSize = "5", string? sortColumn = "", string ascendingOrder = "true")
        {
            var tour = await _tournamentService.GetByIdTournament(idTournament);
            if (tour == null)
            {
                return BadRequest("Không tìm thấy giải đấu");
            }
            int _currentPage = int.Parse(currentPage);
            int _pageSize = int.Parse(pageSize);
            bool _ascendingOrder = ascendingOrder == "true";

            var playerStats = await _playerStatService.GetPlayerStats(idTournament, false, sortColumn, _ascendingOrder);
            if (playerStats == null || playerStats.Count == 0)
            {
                return Ok(new { idTour = tour.Id, playerStats = new List<object>() });
            }
            var playerStatsList = playerStats
                .GroupBy(ps => ps.PlayerId)
                .Select(group => new
                {
                    PlayerStatsId = group.Key,
                    Score = group.Sum(ps => ps.Score),
                    Assists = group.Sum(ps => ps.Assists),
                    YellowCard = group.Sum(ps => ps.YellowCard),
                    RedCard = group.Sum(ps => ps.RedCard)
                })
                .AsQueryable();

            var totalRecords = playerStatsList.ToList().Count();
            int totalPage = totalRecords % _pageSize != 0 ? (totalRecords / _pageSize + 1) : (totalRecords / _pageSize);


            switch (sortColumn.ToLower())
            {
                case "score":
                    {
                        if (_ascendingOrder)
                        {
                            playerStatsList = playerStatsList.OrderByDescending(x => x.Score);
                        }
                        else
                        {
                            playerStatsList = playerStatsList.OrderBy(x => x.Score);
                        }
                        break;
                    }
                case "yellowcard":
                    {
                        if (_ascendingOrder)
                        {
                            playerStatsList = playerStatsList.OrderByDescending(x => x.YellowCard);
                        }
                        else
                        {
                            playerStatsList = playerStatsList.OrderBy(x => x.YellowCard);
                        }
                        break;
                    }
                case "redcard":
                    {
                        if (_ascendingOrder)
                        {
                            playerStatsList = playerStatsList.OrderByDescending(x => x.RedCard);
                        }
                        else
                        {
                            playerStatsList = playerStatsList.OrderBy(x => x.RedCard);
                        }
                        break;
                    }
                default:
                    {
                        if (_ascendingOrder)
                        {
                            playerStatsList = playerStatsList.OrderByDescending(x => x.Score);
                        }
                        else
                        {
                            playerStatsList = playerStatsList.OrderBy(x => x.Score);
                        }
                        break;
                    }
            }



            var result = new
            {
                listplayerStats = playerStatsList.Skip(_pageSize * _currentPage - _pageSize).Take(_pageSize),
                _currentPage,
                _pageSize,
                _ascendingOrder,
                totalPage,
                totalRecords,
                hasPrevious = _currentPage > 1,
                hasNext = _currentPage < totalPage,
            };

            return Ok(result);
        }



    }
}
