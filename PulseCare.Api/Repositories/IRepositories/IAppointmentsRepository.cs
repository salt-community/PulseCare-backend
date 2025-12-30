using PulseCare.API.Data.Entities.Medical;

public interface IAppointmentsRepository
{
    Task<IEnumerable<Appointment>> GetAppointmentsById(Guid id);
}