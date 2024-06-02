using APBD10.DTOs;
using APBD10.Entities;
using APBD10.Exceptions;
using APBD10.Repositories;

namespace APBD10.Policies;

public class PrescriptionAddingPolicy : IPrescriptionAddingPolicy
{
    private IPatientRepository _patientRepository;
    private IMedicamentRepository _medicamentRepository;

    public PrescriptionAddingPolicy(IPatientRepository patientRepository, IMedicamentRepository medicamentRepository)
    {
        _patientRepository = patientRepository;
        _medicamentRepository = medicamentRepository;
    }
    public async Task<bool> PatientExists(Patient patient)
    {
        return await _patientRepository.GetPatientByPatientDetails(patient) != null;
    }

    public async Task<bool> MedicamentExists(Medicament medicament)
    {
        if (await _medicamentRepository.GetMedicament(medicament) == null)
        {
            throw new DomainException("Medicament is not presented in the database");
        }

        return true;
    }

    public bool IsMedicamentListOver10Elements(List<MedicamentDoseDTO> medicaments)
    {
        return medicaments.Count > 10;
    }

    public bool IsDueDateGreaterThanDate(DateTime dueDate, DateTime date)
    {
        return dueDate >= date;
    }
}