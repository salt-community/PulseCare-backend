using PulseCare.API.Data.Enums;
public class HealthTipDto
{
    public HealthTipCategoryType Category { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Icon { get; set; }
}
