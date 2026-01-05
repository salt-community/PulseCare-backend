using Microsoft.EntityFrameworkCore;
using PulseCare.API.Context;
using PulseCare.API.Data.Entities.Users;

public class UserRepository : IUserRepository
{
    private readonly PulseCareDbContext _context;

    public UserRepository(PulseCareDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User request)
    {
        _context.Users.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        return await _context.Users.AnyAsync(u => u.ClerkId == userId);
    }
}