using APBD10.DTOs;
using APBD10.Entities;
using APBD10.Repositories;

namespace APBD10.Policies;

public interface IPrescriptionAddingPolicy
{
    Task<bool> PatientExists(Patient patient);
    Task<bool> MedicamentExists(Medicament medicament);
    bool IsMedicamentListOver10Elements(List<MedicamentDoseDTO> medicaments);
    bool IsDueDateGreaterThanDate(DateTime dueDate, DateTime date);
}