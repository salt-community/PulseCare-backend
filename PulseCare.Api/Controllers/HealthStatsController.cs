using Microsoft.AspNetCore.Mvc;

namespace PulseCare.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthStatsController : ControllerBase
{
    private readonly HealthStatsRepository _repository;
    public HealthStatsController(HealthStatsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<HealthStatsDto>>> GetHealthStats(Guid id)
    {
        return await _repository.GetHealthStatsAsync(id);
    }
}

