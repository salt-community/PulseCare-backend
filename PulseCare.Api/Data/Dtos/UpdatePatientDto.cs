public class UpdatePatientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BloodType { get; set; } = string.Empty;
}