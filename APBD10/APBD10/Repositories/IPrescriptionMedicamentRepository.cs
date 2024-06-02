using APBD10.DTOs;

namespace APBD10.Repositories;

public interface IPrescriptionMedicamentRepository
{
    Task<int> AddPrescriptionMedicament(int idPrescription, MedicamentDoseDTO medicamentDoseDto);
}