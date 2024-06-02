using APBD10.Entities;

namespace APBD10.Repositories;

public interface IPrescriptionRepository
{
    Task<int> AddPrescription(Prescription prescription);
}