using CreateTournament.DTOs;
using CreateTournament.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayerController(IPlayerService playerService, ITeamService teamService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync(string name, int idTeam)
        {
            if (name == null || idTeam <=0)
            {
                return BadRequest("Invalid number team of player or null at name");
            }
            var team = await _teamService.GetTeamByIdAsync(idTeam);
            if(team == null)
            {
                return BadRequest("The team dont exits in the database");
            }
            var player = new PlayerDTO
            {
                Name = name,
                TeamId = idTeam
            };
            var result = await _playerService.CreateAsync(player);
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var exits = await _playerService.GetPlayerById(id);
            if (exits == null)
            {
                return BadRequest("The Player dont exits");
            }
            var player = await _playerService.DeleteAsync(id);
            if(player == false)
            {
                return BadRequest();
            }
            return Ok(player);
        }
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllByIdTeamAsync(int idTeam)
        {
            var players = await _playerService.GetAllByIdTeamAsync(idTeam);
            if(players.Count == 0)
            {
                return Ok();
            }
            return Ok(players);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, string name)
        {
            var player = await _playerService.UpdateAsync(id, name);
            if(player == null)
            {
                return BadRequest();
            }
            return Ok(player);
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetAllByIdTeamAsync(string name)
        {
            var players = await _playerService.GetPlayerByName(name);
            if (players.Count == 0)
            {
                return Ok(players);
            }
            return Ok(players);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerById(id);
            if (player == null)
            {
                return BadRequest();
            }
            return Ok(player);
        }

    }
}
