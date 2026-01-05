using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminDashboardController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository) : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    // GET: /{patientId}/dashboard
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<AdminDashboardDto>> GetAdminDashboard(Guid doctorId) //doctor ID
    {
        var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("sub")?.Value;

        var patients = await _patientRepository.GetAllPatientsAsync();
        var appointments = await _appointmentRepository.GetAllAppointmentsAsync();

        var dashboardDto = new AdminDashboardDto
        {
            TotalPatients = patients.Count(),
            UnreadMessages = 0, // Placeholder for unread messages count
            TodayAppointments = appointments.Count(a => a.Date.Date == DateTime.Today),
            RecentPatients = appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.Date + a.Time >= DateTime.Now)
                .OrderBy(a => a.Date).ThenBy(a => a.Time)
                .Take(3)
                .Select(a => new PatientDto
            {
                Name = a.Patient.User?.Name,
                Email = a.Patient.User?.Email,
                Conditions = a.Patient.Conditions.Select(c => c.Name).ToList()
            }).ToList(),
            UpcomingAppointments = appointments
                .Where(a => a.DoctorId == doctorId)
                .Where(a => a.Date + a.Time < DateTime.Now)
                .OrderByDescending(a => a.Date)
                .ThenByDescending(a => a.Time)
                .Take(3)
                .Select(a => new AppointmentDto
            {
                PatientName = a.Patient.User?.Name,
                Date = a.Date,
                Time = a.Time.ToString(@"hh\:mm"),
                Type = a.Type.ToString(),
            }).ToList()
        };

        return Ok(dashboardDto);
    }
}