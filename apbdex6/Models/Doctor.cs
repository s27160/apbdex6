using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace apbdex6.Models;

[PrimaryKey(nameof(IdDoctor))]
public class Doctor
{
    public int IdDoctor { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }

    [JsonIgnore]
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}