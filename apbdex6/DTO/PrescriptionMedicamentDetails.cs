using apbdex6.Models;

namespace apbdex6.DTO;

public class PrescriptionMedicamentDetails
{
    public Medicament Medicament { set; get; }
    public int Dose { set; get; }
    public string Details { set; get; }
}