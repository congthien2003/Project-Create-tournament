using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using CreateTournament.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if(player == null)
            {
                return BadRequest("Cầu thủ không tồn tại");
            }
            var match = await _matchService.GetMatchByIdAsync(matchResult.MatchId);
            if(match == null)
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
                if (playerStatsDTO.Assits > matchResult.ScoreT1)
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
                if (playerStatsDTO.Assits > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
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
        [HttpPut("id")]
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
            
            if(player.TeamId != match.IdTeam1 && player.TeamId != match.IdTeam2)
            {
                return BadRequest("Cầu thủ không tồn tại trong team này");
            }
            if (player.TeamId == match.IdTeam1)
            {
                if (playerStatsDTO.Score > matchResult.ScoreT1)
                {
                    return BadRequest("Số bàn thắng lớn hơn kết quả trận đấu");
                }
                if (playerStatsDTO.Assits > matchResult.ScoreT1)
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
                if (playerStatsDTO.Assits > matchResult.ScoreT2 || playerStatsDTO.Score < 0)
                {
                    return BadRequest("Số kiến tạo nhiều hơn kết quả trận đấu");
                }
            }
            var playerStat = await _playerStatService.UpdateByIdAsync(id,playerStatsDTO);
            var result = await _playerStatService.GetByIdAsync(id);
            if (playerStat == null)
            {
                return BadRequest("Cập nhật thất bại");
            }
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var playerStat = await _playerStatService.GetByIdAsync(id);
            if(playerStat == null)
            {
                return BadRequest("Không tồn tại thông tin");
            }
            return Ok(playerStat);
        }
        [HttpGet("idmatchresult")]
        public async Task<ActionResult> GetAllByIdMatch(int id)
        {
            var playerStats = await _playerStatService.GetAllByIdMatchAsync(id);
            if(playerStats == null)
            {
                return BadRequest("Trận đấu chưa diễn ra");
            }
            return Ok(playerStats);
        }
        [HttpGet("idplayer")]
        public async Task<ActionResult> GetAllByIdPlayer(int id)
        {
            var playerStats = await _playerStatService.GetAllByIdPlayerAsync(id);
            if (playerStats == null)
            {
                return BadRequest("Cầu thủ chưa tham gia thi đấu");
            }
            return Ok(playerStats);
        }
        [HttpGet("idmap")]
        public async Task<ActionResult> GetAllByIdPlayerAndMatch(int idMatch, int idPlayer)
        {
            if(idMatch <= 0 || idPlayer <= 0)
            {
                return BadRequest("Thông tin truyền vô không hợp lệ");
            }
            var playerStat = await _playerStatService.GetByIdMatchAndIdPlayerAsync(idMatch, idPlayer);
            if( playerStat == null)
            {
                return BadRequest("Cầu thủ không tham gia trận đấu này");
            }
            
            return Ok(playerStat);
        }
        [HttpGet("idTournament")]
        public async Task<ActionResult> GetAllByIdTournament(int id)
        {
            var playerStats = await _playerStatService.GetAllByIdTournamentAsync(id);
            if (playerStats == null)
            {
                return BadRequest("Giải đấu chưa diễn ra");
            }
            return Ok(playerStats);
        }

        [HttpGet("{idtour:int}")]
        public async Task<ActionResult> GetAllByIdPlayerScore(int idtour)
        {
            var tour = await _tournamentService.GetByIdTournament(idtour);
            if (tour == null)
            {
                return BadRequest("khong tim thay giai đấu");

            }
            var playerstats = await _playerStatService.GetAllByIdPlayerScoreAsync(idtour);
            var result = new
            {
                tour,
                playerstats,
            };
            return Ok(result);
        }

        [HttpGet("toplist")]
        public async Task<IActionResult> GetAllPlayStats(int idtour, string pageNumber = "1", string pageSize = "10", string? sortColumn= "", string ascendingOrder = "false")
        {
            var tour = await _tournamentService.GetByIdTournament(idtour);
            if (tour == null)
            {
                return BadRequest("khong tim thay giai đấu");

            }
            int _currentPage = int.Parse(pageNumber);
            int _pageSize = int.Parse(pageSize);
            bool _ascendingOrder = ascendingOrder == "true";
            var playstats = await _playerStatService.GetAllPlayStats(false,_currentPage, _pageSize, sortColumn, _ascendingOrder);
            var result = new
            {
                tour,
                playstats,
                _currentPage,
                _pageSize,
                _ascendingOrder,
            };
            return Ok(result);
        }
    }
}
