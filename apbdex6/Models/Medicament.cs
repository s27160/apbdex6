namespace apbdex6.Models;

public class Medicament
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }
    public string Type { set; get; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}