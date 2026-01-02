using PulseCare.API.Data.Enums;

namespace PulseCare.API.Data.Entities.Users;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ClerkId { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required string Name { get; set; }
    public UserRoleType Role { get; set; }
    public string? Avatar { get; set; }
}
