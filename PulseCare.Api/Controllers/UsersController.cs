using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseCare.API.Data.Entities.Users;
using Repositories.IRepositories;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("sync")]
    [Authorize]
    public async Task<IActionResult> Sync()
    {
        var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(clerkUserId))
        {
            return Unauthorized();
        }

        var exists = await _userRepository.ExistsAsync(clerkUserId);

        if (!exists)
        {
            var newUser = CreateUserFromToken(clerkUserId);
            await _userRepository.CreateAsync(newUser);
        }

        return NoContent();
    }

    private User CreateUserFromToken(string clerkUserId)
    {
        var name = User.FindFirst("name")?.Value;
        var email = User.FindFirst("email")?.Value;

        return new User
        {
            ClerkId = clerkUserId,
            Name = name,
            Email = email
        };
    }
}