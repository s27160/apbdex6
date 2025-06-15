using apbdex6.Models;

namespace apbdex6.DTO;

public class PrescriptionDetails
{
    public int IdPrescription { set; get; }
    public DateTime Date { set; get; }
    public DateTime DueDate { set; get; }
    public Doctor Doctor { set; get; }
    public List<PrescriptionMedicamentDetails> Medicaments { set; get; }
}