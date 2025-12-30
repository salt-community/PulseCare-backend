using PulseCare.API.Data.Entities.Medical;

public class MedicationsRepository : IMedicationsRepository
{
    public Task<IEnumerable<Medication>> GetMedicationsById(Guid id)
    {
        throw new NotImplementedException();
    }

}