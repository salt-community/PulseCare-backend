  namespace PulseCare.API.Data.Entities.Medical;
  public class EmergencyContact
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }        
        public required string Relationship { get; set; }
        public Guid PatientId { get; set; }
    }