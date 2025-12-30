using PulseCare.API.Data.Entities.Medical;
public interface IMedicationRepository
{
    Task<IEnumerable<Medication>> GetMedicationsById(Guid id);
}