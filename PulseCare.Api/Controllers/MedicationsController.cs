using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMedicationsRepository _medicationsRepository;

    public MedicationsController(IMedicationsRepository medicationsRepository)
    {
        _medicationsRepository = medicationsRepository;
    }

    [HttpGet("{patientId}")]
    public async Task<ActionResult<IEnumerable<MedicationDto>>> GetPatientMedications(Guid patientId)
    {
        var medications = await _medicationsRepository.GetMedicationsById(patientId);

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