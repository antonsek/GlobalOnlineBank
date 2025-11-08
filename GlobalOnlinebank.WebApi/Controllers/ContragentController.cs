using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalOnlinebank.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContragentController : ControllerBase
    {
        private readonly IContragentService _contragentService;

        public ContragentController(IContragentService contragentService)
        {
            _contragentService = contragentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContragentDto>>> GetAll()
        {
            var contragents = await _contragentService.GetAllAsync();
            return Ok(contragents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContragentDto>> GetById(int id)
        {
            var contragent = await _contragentService.GetByIdAsync(id);
            return Ok(contragent);
        }

        [HttpPost]
        public async Task<ActionResult<ContragentDto>> Create(CreateContragentDto dto)
        {
            var contragent = await _contragentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = contragent.Id }, contragent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateContragentDto dto)
        {
            await _contragentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contragentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
