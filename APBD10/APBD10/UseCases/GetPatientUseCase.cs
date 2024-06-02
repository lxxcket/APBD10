using APBD10.DTOs;
using APBD10.Entities;
using APBD10.Exceptions;
using APBD10.Repositories;

namespace APBD10.UseCases;

public class GetPatientUseCase : IGetPatientUseCase
{
    private IPatientRepository _patientRepository;

    public GetPatientUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<PatientWithPrescriptionsDTO> GetPatient(int idPatient)
    {
        PatientWithPrescriptionsDTO? patient = await _patientRepository.GetPatientById(idPatient);
        if (patient == null)
        {
            throw new DomainException("Patient does not exist");
        }
        
        return patient;
    }
}