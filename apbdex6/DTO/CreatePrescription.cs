using apbdex6.Models;

namespace apbdex6.DTO;

public class CreatePrescription
{
    public CreatePatient Patient { get; set; }
    public CreateDoctor Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<CreatePrescriptionMedicament> Medicaments { get; set; }
}