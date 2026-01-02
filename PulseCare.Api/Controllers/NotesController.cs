using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

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
        return Ok();
    }
}