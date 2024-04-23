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
    public class FormatTypeController : ControllerBase
    {
        private readonly IFormatTypeService _formatTypeService;
        private readonly IFormatTypeRepository<FormatType> _formatTypeRepository;
        public FormatTypeController(IFormatTypeService formatTypeService, IFormatTypeRepository<FormatType> formatTypeRepository)
        {
            _formatTypeService = formatTypeService;
            _formatTypeRepository = formatTypeRepository;
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetAll() 
        {
            var formatType = await _formatTypeService.GetAll();
            return Ok(formatType);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetFormatTypeByIdAsync(int id) 
        {
            var formatType = await _formatTypeService.GetByIdFormatType(id);
            if(formatType == null)
            {
                return BadRequest();
            }
            return Ok(formatType);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(FormatTypeDTO formatTypeDTO)
        {
            var newformatType = await _formatTypeService.Create(formatTypeDTO);
            return Ok(newformatType);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(FormatTypeDTO formatTypeDTO)
        {
            var editformatType = await _formatTypeService.Update(formatTypeDTO);
            if(editformatType == null)
            {
                return BadRequest();
            }
            return Ok(editformatType);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Deleted(int id)
        {
            var formatType = await _formatTypeRepository.GetByIdFormatType(id);
            if (formatType == null || formatType.IsDeleted)
            {
                return BadRequest("Failed to delete FormatType");
            }
            else
            {
                await _formatTypeService.Delete(id);
                return Ok(true);
            }
        }
    }
}
