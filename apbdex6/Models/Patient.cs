using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace apbdex6.Models;

[PrimaryKey(nameof(IdPatient))]
public class Patient
{
    public int IdPatient { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public DateTime BirthDate { set; get; }

    [JsonIgnore]
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}