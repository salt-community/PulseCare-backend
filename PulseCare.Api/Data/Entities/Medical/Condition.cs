 namespace PulseCare.API.Data.Entities.Medical;
 public class Condition
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Guid PatientId { get; set; }
    }