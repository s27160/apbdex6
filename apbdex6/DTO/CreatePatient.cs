namespace apbdex6.DTO;

public class CreatePatient
{
    public int? IdPatient { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public DateTime BirthDate { set; get; }
}