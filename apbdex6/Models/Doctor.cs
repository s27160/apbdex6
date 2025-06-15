namespace apbdex6.Models;

public class Doctor
{
    public int IdDoctor { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}