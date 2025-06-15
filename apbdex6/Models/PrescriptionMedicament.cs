using Microsoft.EntityFrameworkCore;

namespace apbdex6.Models;

[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    public int IdMedicament { set; get; }
    public Medicament Medicament { set; get; }

    public int IdPrescription { set; get; }
    public Prescription Prescription { set; get; }

    public int Dose { set; get; }
    public string Details { set; get; }
}