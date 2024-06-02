using APBD10.Entities;

namespace APBD10.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetAnyDoctor();
}