using APBD10.DTOs;
using APBD10.Entities;

namespace APBD10.UseCases;

public interface IGetPatientUseCase
{
    Task<PatientWithPrescriptionsDTO> GetPatient(int idPatient);
}