using apbdex6.DTO;
using apbdex6.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbdex6.Controllers;

[ApiController]
[Route("api/prescription")]
public class PrescriptionController: ControllerBase
{
    private readonly IPrescriptionService _svc;
    public PrescriptionController(IPrescriptionService svc) => _svc = svc;

    [HttpPost("create")]
    public IActionResult Post([FromBody] CreatePrescription createPrescription)
    {
        var newId =  _svc.Create(createPrescription);
        var patientId = createPrescription.Patient.IdPatient ?? newId;
        var dto = _svc.GetPatient(patientId);
        return Ok(dto);
    }
}