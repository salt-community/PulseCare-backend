using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMedicationRepository _medicationRepository;

    public MedicationsController(IMedicationRepository medicationRepository)
    {
        _medicationRepository = medicationRepository;
    }

    [HttpGet("{patientId}")]
    public async Task<ActionResult<IEnumerable<MedicationDto>>> GetPatientMedications(Guid patientId)
    {
        var medications = await _medicationRepository.GetMedicationsById(patientId);

        if (medications == null)
            return NotFound();

        var medicationsDto = medications.Select(m => new MedicationDto
        {
            Name = m.Name,
            Dosage = m.Dosage,
            Frequency = m.Frequency,
            Instructions = m.Instructions
        }).ToList();

        return Ok(medicationsDto);
    }
}