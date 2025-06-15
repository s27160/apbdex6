using apbdex6.DTO;

namespace apbdex6.Services;

public interface IPrescriptionService
{
    int Create(CreatePrescription dto);
    PatientDetails GetPatient(int idPatient);
}