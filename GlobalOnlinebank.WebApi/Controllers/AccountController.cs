using GlobalOnlinebank.Application.DTOs;
using GlobalOnlinebank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalOnlinebank.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
        {
            var contragents = await _accountService.GetAllAsync();
            return Ok(contragents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetById(int id)
        {
            var contragent = await _accountService.GetByIdAsync(id);
            return Ok(contragent);
        }
        
        [HttpGet("by-contragent/{contragentId:long}")]
        public async Task<ActionResult<AccountDto>> GetByContragent(long contragentId)
        {
            var contragent = await _accountService.GetByContragentID(contragentId);
            return Ok(contragent);
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> Create(CreateAccountDto dto)
        {
            var contragent = await _accountService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = contragent.Id }, contragent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAccountDto dto)
        {
            await _accountService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteAsync(id);
            return NoContent();
        }
    }
}
