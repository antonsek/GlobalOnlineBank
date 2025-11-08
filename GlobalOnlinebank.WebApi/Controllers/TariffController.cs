using GlobalOnlinebank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalOnlinebank.WebApi.Controllers;

 [ApiController]
    [Route("api/[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        // GET /api/tariff/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var tariffs = await _tariffService.GetAllTariffsAsync(cancellationToken);
            return Ok(tariffs);
        }

        // GET /api/tariff/{id}
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            var tariff = await _tariffService.GetTariffByIdAsync(id, cancellationToken);
            if (tariff == null)
                return NotFound($"Tariff with id {id} not found");
            return Ok(tariff);
        }

        // GET /api/tariff/by-points/{points}
        [HttpGet("by-points/{points:int}")]
        public async Task<IActionResult> GetByPoints(int points, CancellationToken cancellationToken)
        {
            var tariff = await _tariffService.GetTariffByPointsAsync(points, cancellationToken);
            if (tariff == null)
                return NotFound("No tariff matches these points");
            return Ok(tariff);
        }

        // GET /api/tariff/calculate?amount=240000000&country=Китай&segment=Импорт
        [HttpGet("calculate")]
        public IActionResult Calculate([FromQuery] decimal amount, [FromQuery] string country, [FromQuery] string segment)
        {
            var points = _tariffService.CalculatePoints(amount, country, segment);
            return Ok(new { amount, country, segment, points });
        }

        // GET /api/tariff/benefit/{points}
        [HttpGet("benefit/{points:int}")]
        public async Task<IActionResult> GetBenefit(int points, CancellationToken cancellationToken)
        {
            var description = await _tariffService.GetClientBenefitDescriptionAsync(points, cancellationToken);
            return Ok(new { points, description });
        }
    }