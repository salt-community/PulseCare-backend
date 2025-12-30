using PulseCare.API.Data.Entities.Medical;

public class AppointmentsRepository : IAppointmentsRepository
{
    public Task<IEnumerable<Appointment>> GetAppointmentsById(Guid id)
    {
        throw new NotImplementedException();
    }

}