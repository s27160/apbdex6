using apbdex6.Models;

namespace apbdex6.DTO;

public class PatientDetails
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public DateTime BirthDate { get; set; }

    public List<PrescriptionDetails> Prescriptions { get; set; }
}