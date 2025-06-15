namespace apbdex6.Models;

public class Patient
{
    public int IdPatient { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public DateTime BirtDate { set; get; }

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}