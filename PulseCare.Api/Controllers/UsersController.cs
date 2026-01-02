using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseCare.API.Data.Entities.Users;
using Repositories.IRepositories;

namespace Controllers;

[ApiController]
[Route("[controller]")]
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
        var clerkUserId = User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(clerkUserId))
        {
            return Unauthorized();
        }

        var exists = _userRepository.ExistsAsync(clerkUserId);

        if (!exists)
        {
            var newUser = CreateUserFromToken(clerkUserId);
            await _userRepository.CreateAsync(newUser);
        }

        return NoContent();
    }

    private User CreateUserFromToken(string clerkUserId)
    {
        return new User
        {
            // Id = clerkUserId, -> ändra user id till string? 
            Name = User.FindFirst(ClaimTypes.GivenName)?.Value,
            Email = User.FindFirst(ClaimTypes.Email)?.Value
        };
    }


}