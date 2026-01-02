using PulseCare.API.Data.Entities.Medical;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAppointmentsById(Guid id);
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<Appointment?> GetAppointmentById(Guid id);
    Task<Appointment> CreateAppointment(Appointment appointment);
    Task<Appointment?> UpdateAppointment(Appointment appointment);
    Task<bool> DeleteAppointment(Guid id);
}