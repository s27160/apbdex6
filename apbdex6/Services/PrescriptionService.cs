using apbdex6.Data;
using apbdex6.DTO;
using apbdex6.Models;
using Microsoft.EntityFrameworkCore;

namespace apbdex6.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly ApplicationDbContext _db;

    public PrescriptionService(ApplicationDbContext db)
    {
        _db = db;
    }

    public int Create(CreatePrescription dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new Exception("Max. 10 leków na recepcie.");

        if (dto.DueDate < dto.Date)
            throw new Exception("DueDate musi być >= Date.");

        Patient patient = null;
        if (dto.Patient.IdPatient != null)
        {
            patient = _db.Patients.Find(dto.Patient.IdPatient);
        }

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                BirthDate = dto.Patient.BirthDate
            };
            _db.Patients.Add(patient);
            _db.SaveChanges();
        }

        var medsIds = dto.Medicaments.Select(m => m.Medicament.IdMedicament).ToList();
        var existCount = _db.Medicaments.Count(m => medsIds.Contains(m.IdMedicament));
        if (existCount != medsIds.Count) throw new Exception("Jeden lub więcej leków nie istnieje.");

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = dto.Doctor.IdDoctor,
        };

        _db.Prescriptions.Add(prescription);
        _db.SaveChanges();

        foreach (var createPrescriptionMedicament in dto.Medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = createPrescriptionMedicament.Medicament.IdMedicament,
                Dose = createPrescriptionMedicament.Dose,
                Details = createPrescriptionMedicament.Details
            };
            _db.PrescriptionMedicaments.Add(prescriptionMedicament);
            _db.SaveChanges();
        }

        return prescription.IdPrescription;
    }

    public PatientDetails GetPatient(int idPatient)
    {
        var patient = _db.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(prescriptionMedicament => prescriptionMedicament.Medicament)
            .FirstOrDefault(p => p.IdPatient == idPatient);

        if (patient == null)
            throw new Exception("Pacjent nie istnieje.");

        var result = new PatientDetails()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionDetails()
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new Doctor()
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName
                    },
                    Medicaments = pr.PrescriptionMedicaments
                        .Select(pm => new PrescriptionMedicamentDetails()
                        {
                            Medicament = pm.Medicament,
                            Dose = pm.Dose,
                            Details = pm.Details
                        })
                        .ToList()
                })
                .ToList()
        };

        return result;
    }
}