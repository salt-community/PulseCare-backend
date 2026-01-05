using PulseCare.API.Data.Entities.Users;

public interface IUserRepository
{
    Task CreateAsync(User request);
    Task<bool> ExistsAsync(string userId);
}