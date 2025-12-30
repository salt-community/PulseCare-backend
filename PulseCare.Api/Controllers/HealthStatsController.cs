using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PulseCare.API.Context;
using PulseCare.API.Data.Enums;

namespace PulseCare.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthStatsController : ControllerBase
{
    private readonly PulseCareDbContext _context;
    public HealthStatsController(PulseCareDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<HealthStatResponseDto>>> GetHealthStats(Guid id)
    {
        var healthData = _context.HealthStats.Where(h => h.PatientId == id);

        var response = new List<HealthStatResponseDto>();

        foreach (var item in healthData)
        {
            response.Add(
                new HealthStatResponseDto
                    (
                        item.Id,
                        item.Type,
                        item.Value,
                        item.Unit,
                        item.Date,
                        item.Status
                    )
            );
        }

        return response;
    }
}

public record HealthStatResponseDto
(
    Guid Id,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    HealthStatType Type,
    string Value,
    string Unit,
    DateTime Date,

    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    HealthStatusType Status
);
