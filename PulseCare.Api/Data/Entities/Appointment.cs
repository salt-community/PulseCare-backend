using PulseCare.API.Data.Enums;

namespace PulseCare.API.Data.Entities;

public class Appointment
{
    public string Id { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public AppointmentType Type { get; set; }

    public AppointmentStatusType Status { get; set; }

    public string? Notes { get; set; }
}

