using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace apbdex6.Models;

[PrimaryKey(nameof(IdMedicament))]
public class Medicament
{
    public int IdMedicament { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }
    public string Type { set; get; }

    [JsonIgnore]
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}