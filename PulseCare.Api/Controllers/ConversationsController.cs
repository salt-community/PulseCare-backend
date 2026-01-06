using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PulseCare.API.Data.Enums;

[ApiController]
[Route("api/conversations")]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IHubContext<PulseCareHub> _hub;


    public ConversationsController(IConversationRepository conversationRepository, IMessageRepository messageRepository, IHubContext<PulseCareHub> hub)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _hub = hub;
    }

    [HttpGet]
    public async Task<ActionResult<List<ConversationDto>>> GetConversations(
        [FromQuery] UserRoleType role,
        [FromQuery] Guid userId)
    {
        var conversations = role switch
        {
            UserRoleType.Patient => await _conversationRepository.GetConversationsForPatientAsync(userId),
            UserRoleType.Doctor => await _conversationRepository.GetConversationsForDoctorAsync(userId),
            _ => null
        };

        if (conversations is null)
            return BadRequest("Invalid role. Must be 'patient' or 'doctor'.");


        var dtos = conversations.Select(conv =>
        {
            var latest = conv.Messages
                .OrderByDescending(m => m.Date)
                .FirstOrDefault();

            var latestDto = latest is null
                ? null
                : new MessageDto(
                    latest.Id,
                    latest.Subject,
                    latest.Content,
                    latest.Date,
                    latest.Read,
                    latest.FromPatient,
                    latest.ConversationId
                );

            var unread = role switch
            {
                UserRoleType.Patient => conv.Messages.Count(m => !m.FromPatient && !m.Read),
                UserRoleType.Doctor => conv.Messages.Count(m => m.FromPatient && !m.Read),
                _ => 0
            };


            return new ConversationDto(
                conv.Id,
                conv.PatientId,
                conv.DoctorId,
                latestDto,
                unread
            );
        }).ToList();

        return Ok(dtos);
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

        return Ok(new StartConversationResponse(conversation.Id, dto));
    }
}
