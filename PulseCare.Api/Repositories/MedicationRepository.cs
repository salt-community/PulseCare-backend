using PulseCare.API.Data.Entities.Medical;

public class MedicationRepository : IMedicationRepository
{
    public Task<IEnumerable<Medication>> GetMedicationsById(Guid id)
    {
        throw new NotImplementedException();
    }

}