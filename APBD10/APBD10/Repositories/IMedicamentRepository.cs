using APBD10.Contexts;
using APBD10.Entities;

namespace APBD10.Repositories;

public interface IMedicamentRepository
{
    Task<Medicament?> GetMedicament(Medicament medicament);
    
}