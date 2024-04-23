using CreateTournament.DTOs;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Interfaces.IServices;
using CreateTournament.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CreateTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SportTypeController : ControllerBase
    {
        private readonly ISportTypeService _sportTypeService;
        private readonly ISportTypeRepository<SportType> _sportTypeRepository;
        public SportTypeController(ISportTypeService sportTypeService, ISportTypeRepository<SportType> sportTypeRepository)
        {
            _sportTypeService = sportTypeService;
            _sportTypeRepository = sportTypeRepository;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var sportType = await _sportTypeService.GetAll();
            return Ok(sportType);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetByIdSportTypeAsync(int id)
        {
            var sportType = await _sportTypeService.GetByIdSportType(id);
            if(sportType == null)
            {
                return BadRequest();
            }
            return Ok(sportType);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(SportTypeDTO sportTypeDTO)
        {
            var newSportType = await _sportTypeService.Create(sportTypeDTO);
            return Ok(newSportType);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(SportTypeDTO sportTypeDTO) 
        {
            var editSportType = await _sportTypeService.Update(sportTypeDTO);
            if(editSportType == null)
            {
                return BadRequest();
            }
            return Ok(editSportType);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Deleted(int id)
        {
            var sportType = await _sportTypeRepository.GetByIdSportType(id);
            if (sportType == null || sportType.IsDeleted)
            {
                return BadRequest("Failed to delete FormatType");
            }
            else
            {
                await _sportTypeService.Delete(id);
                return Ok(true);
            }
        }
    }
}
