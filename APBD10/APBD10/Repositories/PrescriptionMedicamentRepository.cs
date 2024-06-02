using APBD10.Contexts;
using APBD10.DTOs;
using APBD10.Entities;

namespace APBD10.Repositories;

public class PrescriptionMedicamentRepository : IPrescriptionMedicamentRepository
{
    private MedicamentContext _medicamentContext;

    public PrescriptionMedicamentRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }
    public async Task<int> AddPrescriptionMedicament(int idPrescription, MedicamentDoseDTO medicamentDoseDto)
    {
        var entity = new PrescriptionMedicament()
        {
            IdPrescription = idPrescription,
            IdMedicament = medicamentDoseDto.IdMedicament,
            Details = medicamentDoseDto.Details,
            Dose = medicamentDoseDto.Dose
        };
        await _medicamentContext.PrescriptionMedicaments.AddAsync(entity);
        return await _medicamentContext.SaveChangesAsync();
    }
}