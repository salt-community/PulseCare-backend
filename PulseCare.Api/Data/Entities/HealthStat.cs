namespace PulseCare.API.Data.Entities;

public class HealthStat
{
    public string Id { get; set; } = string.Empty;
    public HealthStatType Type { get; set; }

    public string Value { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public HealthStatusType Status { get; set; }
}

