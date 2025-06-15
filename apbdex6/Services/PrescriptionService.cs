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
        if (dto.CreatePatient.IdPatient != null)
        {
            patient = _db.Patients.Find(dto.CreatePatient.IdPatient);
        }
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.CreatePatient.FirstName,
                LastName  = dto.CreatePatient.LastName,
                BirtDate = dto.CreatePatient.BirtDate
            };
            _db.Patients.Add(patient);
            _db.SaveChanges();
        }

        var medsIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
        var existCount =  _db.Medicaments
            .Count(m => medsIds.Contains(m.Id));
        if (existCount != medsIds.Count)
            throw new Exception("Jeden lub więcej lek nie istnieje.");

        var prescription = new Prescription
        {
            Date        = dto.Date,
            DueDate     = dto.DueDate,
            IdPatient   = patient.IdPatient,
            IdDoctor    = dto.Doctor.IdDoctor,
            PrescriptionMedicaments = dto.Medicaments
                .Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose         = m.Dose,
                    Details      = m.Details,
                })
                .ToList()
        };

        _db.Prescriptions.Add(prescription);
        _db.SaveChanges();

        return prescription.IdPrescription;
    }

    public PatientDetails GetPatient(int idPatient)
    {
        var patient = _db.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .FirstOrDefault(p => p.IdPatient == idPatient);

        if (patient == null)
            throw new Exception("Pacjent nie istnieje.");

        var result = new PatientDetails()
        {
            IdPatient   = patient.IdPatient,
            FirstName   = patient.FirstName,
            LastName    = patient.LastName,
            BirthDate   = patient.BirtDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new Prescription()
                {
                    IdPrescription = pr.IdPrescription,
                    Date           = pr.Date,
                    DueDate        = pr.DueDate,
                    Doctor = new Doctor()
                    {
                        IdDoctor  = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName  = pr.Doctor.LastName
                    },
                    PrescriptionMedicaments = pr.PrescriptionMedicaments
                        .Select(pm => new PrescriptionMedicament()
                        {
                            IdMedicament = pm.IdMedicament,
                            Dose         = pm.Dose,
                            Details  = pm.Details
                        })
                        .ToList()
                })
                .ToList()
        };

        return result;
    }
}