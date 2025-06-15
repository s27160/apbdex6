using apbdex6.Models;

namespace apbdex6.DTO;

public class CreatePrescription
{
    public CreatePatient CreatePatient { get; set; }
    public Doctor Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionMedicament> Medicaments { get; set; }
}