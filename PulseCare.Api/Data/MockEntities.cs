using PulseCare.API.Data.Enums;

namespace PulseCare.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public UserRole Role { get; set; }
        public string? Avatar { get; set; }
    }

    public class Patient
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string BloodType { get; set; }

        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        public ICollection<Condition> Conditions { get; set; } = new List<Condition>();

        public EmergencyContact EmergencyContact { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Medication> Medications { get; set; } = new List<Medication>();
        public ICollection<HealthStat> HealthStats { get; set; } = new List<HealthStat>();
    }

    public class Allergy
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Guid PatientId { get; set; }
    }

    public class Condition
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Guid PatientId { get; set; }
    }

    public class EmergencyContact
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Relationship { get; set; }

        public Guid PatientId { get; set; }
    }

    public class Medication
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Dosage { get; set; }
        public required string Frequency { get; set; }
        public int TimesPerDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Instructions { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }

    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public AppointmentType Type { get; set; }
        public AppointmentStatusType Status { get; set; }
        public string? Comment  { get; set; }
        public ICollection<Note> AppointmentNotes  { get; set; } = new List<Note>();
    }

    public class HealthStat
    {
        public Guid Id { get; set; }
        public HealthStatType Type { get; set; }
        public required string Value { get; set; }
        public required string Unit { get; set; }
        public DateTime Date { get; set; }
        public HealthStatusType Status { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }

    public class Doctor
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required string Email { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }

    public class Message
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public required string Subject { get; set; }
        public required string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
        public bool FromPatient { get; set; }
    }

    public class Note
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public required string AppointmentDetails { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public required string Title { get; set; }
        public required string Diagnosis { get; set; }
        public required string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class HealthTip
    {
        public Guid Id { get; set; }
        public HealthTipCategory Category { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Icon { get; set; }
    }
}
