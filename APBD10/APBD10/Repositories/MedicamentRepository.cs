using APBD10.Contexts;
using APBD10.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Repositories;

public class MedicamentRepository : IMedicamentRepository
{
    private MedicamentContext _medicamentContext;

    public MedicamentRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }

    public async Task<Medicament?> GetMedicament(Medicament medicament)
    {
        return await _medicamentContext.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicament.IdMedicament);
    }
}