using APBD10.Contexts;
using APBD10.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private MedicamentContext _medicamentContext;

    public DoctorRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }

    public async Task<Doctor?> GetAnyDoctor()
    {
        return await _medicamentContext.Doctors.FirstOrDefaultAsync();
    }
}