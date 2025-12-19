using PulseCare.API.Data.Enums;
using PulseCare.Domain;

namespace PulseCare.API.Data.Entities;

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

