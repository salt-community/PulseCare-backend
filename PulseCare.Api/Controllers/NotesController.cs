using System.Security.Claims;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using PulseCare.API.Data.Entities.Users;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;

    public NotesController(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll()
    {
        var userId = await GetUserIdAsync();

        if (userId == null)
        {
            return NotFound("User not found");
        }

        return Ok();
    }

    private async Task<Guid?> GetUserIdAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return null;

        if (!Guid.TryParse(userId, out Guid result))
        {
            return null;
        }

        return result;
    }

}