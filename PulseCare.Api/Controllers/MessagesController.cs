using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/conversations")]
[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IHubContext<PulseCareHub> _hub;

    public MessagesController(IMessageRepository messageRepository, IConversationRepository conversationRepository, IHubContext<PulseCareHub> hub)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _hub = hub;
    }

    [HttpPost("{conversationId:guid}/messages")]
    public async Task<IActionResult> SendMessage(
        Guid conversationId,
        [FromBody] SendMessageRequest request)
    {
        var conversation = await _conversationRepository.GetByIdAsync(conversationId);
        if (conversation is null)
        {
            return NotFound($"Conversation {conversationId} not found.");
        }

        var messageEntity = await _messageRepository.CreateMessageAsync(
            conversationId,
            request.Subject,
            request.Content,
            request.FromPatient
        );

        var dto = new MessageDto(
            messageEntity.Id,
            messageEntity.Subject,
            messageEntity.Content,
            messageEntity.Date,
            messageEntity.Read,
            messageEntity.FromPatient,
            messageEntity.ConversationId
        );

        await _hub.Clients.Group(conversationId.ToString())
            .SendAsync("ReceiveMessage", dto);

        return Ok(dto);
    }

    [HttpGet("{conversationId:guid}/messages")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages(Guid conversationId)
    {
        var messages = await _messageRepository.GetMessagesAsync(conversationId);

        var dto = messages
            .OrderBy(m => m.Date)
            .Select(m => new MessageDto(
                m.Id,
                m.Subject,
                m.Content,
                m.Date,
                m.Read,
                m.FromPatient,
                m.ConversationId
            ))
            .ToList();

        return Ok(dto);
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartConversation(
        [FromBody] StartConversationRequest request)
    {
        var conversation = await _conversationRepository
            .GetOrCreateForPatientAndDoctorAsync(request.PatientId, request.DoctorId);

        var messageEntity = await _messageRepository.CreateMessageAsync(
            conversation.Id,
            request.Subject,
            request.Content,
            request.FromPatient
        );

        var dto = new MessageDto(
            messageEntity.Id,
            messageEntity.Subject,
            messageEntity.Content,
            messageEntity.Date,
            messageEntity.Read,
            messageEntity.FromPatient,
            messageEntity.ConversationId
        );

        await _hub.Clients.Group(conversation.Id.ToString())
            .SendAsync("ReceiveMessage", dto);

        return Ok(new
        {
            conversationId = conversation.Id,
            message = dto
        });
    }

    [HttpPut("{conversationId:guid}/messages/{messageId:guid}/read")]
    public async Task<IActionResult> MarkMessageAsRead(Guid conversationId, Guid messageId)
    {
        var success = await _messageRepository.MarkMessageAsReadAsync(messageId, conversationId);

        if (!success)
            return NotFound();

        await _hub.Clients.Group(conversationId.ToString())
            .SendAsync("MessageRead", messageId);

        return NoContent();
    }
}