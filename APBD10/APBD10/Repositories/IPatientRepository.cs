using APBD10.DTOs;
using APBD10.Entities;

namespace APBD10.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetPatientByPatientDetails(Patient patient);

    Task<int> CreatePatient(Patient patient);

    Task<PatientWithPrescriptionsDTO?> GetPatientById(int id);
}