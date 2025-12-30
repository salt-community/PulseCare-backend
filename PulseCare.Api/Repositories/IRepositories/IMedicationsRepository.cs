using PulseCare.API.Data.Entities.Medical;

public interface IMedicationsRepository
{
    Task<IEnumerable<Medication>> GetMedicationsById(Guid id);
}