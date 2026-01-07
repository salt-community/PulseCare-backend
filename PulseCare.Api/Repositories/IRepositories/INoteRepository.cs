using PulseCare.API.Data.Entities.Communication;

public interface INoteRepository
{
    Task AddNoteAsync(Note note);
    Task<IEnumerable<Note>> GetAllByClerkUserIdAsync(string clerkUserId);
}