using Microsoft.AspNetCore.Mvc;

public interface IHealthStatsRepository
{
    Task<ActionResult<List<HealthStatsDto>>> GetHealthStatsAsync(Guid id);
}