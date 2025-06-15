using apbdex6.Models;
using apbdex6.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbdex6.Controllers;

[ApiController]
[Route("api/patient")]
public class PatientController : ControllerBase
{
    private readonly IPrescriptionService _svc;
    public PatientController(IPrescriptionService svc) => _svc = svc;

    [HttpGet]
    public ActionResult<Patient> Get([FromBody] int patientId)
    {
        var dto = _svc.GetPatient(patientId);
        return Ok(dto);
    }
}