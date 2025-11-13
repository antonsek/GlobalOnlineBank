using GlobalOnlinebank.Application.Interfaces;
using GlobalOnlinebank.Domain.Entities;
using GlobalOnlinebank.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalOnlinebank.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController: ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult<TransferResponseDto>> Create([FromBody] TransferRequestDto transaction, CancellationToken ct)
        {
            var createdTransaction = await _service.ExecuteAsync(transaction, ct);
            if (createdTransaction == null)
                return BadRequest("Transaction could not be created.");
 
            return Ok(createdTransaction);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<Transaction>> GetById(long id, CancellationToken ct)
        {
            var transaction = await _service.GetByIdAsync(id, ct);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpGet("current-month")]
        public async Task<ActionResult<List<Transaction>>> GetFromCurrentMonth(CancellationToken ct)
        {
            var transactions = await _service.GetFromCurrentMonthAsync(ct);
            return Ok(transactions);
        }

        [HttpGet("current-quarter")]
        public async Task<ActionResult<List<Transaction>>> GetFromCurrentQuarter(CancellationToken ct)
        {
            var transactions = await _service.GetFromCurrentQuarterAsync(ct);
            return Ok(transactions);
        }

        [HttpGet("period")]
        public async Task<ActionResult<List<Transaction>>> GetByPeriod(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken ct)
        {
            if (from > to)
                return BadRequest("Invalid date range");

            var transactions = await _service.GetByPeriodAsync(from, to, ct);
            return Ok(transactions);
        }
    }
}
