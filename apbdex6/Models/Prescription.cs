namespace apbdex6.Models;

public class Prescription
{
    public int IdPrescription { set; get; }
    public DateTime Date { set; get; }
    public DateTime DueDate { set; get; }
    public int IdPatient { set; get; }
    public Patient Patient { set; get; }
    public int IdDoctor { set; get; }
    public Doctor Doctor { set; get; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}