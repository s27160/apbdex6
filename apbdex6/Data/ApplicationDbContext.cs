using apbdex6.Models;
using Microsoft.EntityFrameworkCore;

namespace apbdex6.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Patient>().ToTable("Patient");
        mb.Entity<Doctor>().ToTable("Doctor");
        mb.Entity<Medicament>().ToTable("Medicament");
        mb.Entity<Prescription>().ToTable("Prescription");
        mb.Entity<PrescriptionMedicament>().ToTable("Prescription_Medicament");

        mb.Entity<Patient>().HasKey(p => p.IdPatient);
        mb.Entity<Doctor>().HasKey(d => d.IdDoctor);
        mb.Entity<Medicament>().HasKey(m => m.IdMedicament);
        mb.Entity<Prescription>().HasKey(p => p.IdPrescription);

        mb.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);

        mb.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(pat => pat.Prescriptions)
            .HasForeignKey(p => p.IdPatient);

        mb.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        mb.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        mb.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);
    }
}

