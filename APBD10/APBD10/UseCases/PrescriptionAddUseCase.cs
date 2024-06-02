using APBD10.DTOs;
using APBD10.Entities;
using APBD10.Exceptions;
using APBD10.Policies;
using APBD10.Repositories;

namespace APBD10.UseCases;

public class PrescriptionAddUseCase : IPrescriptionAddUseCase
{
    private IPrescriptionAddingPolicy _policy;
    private IPrescriptionMedicamentRepository _prescriptionMedicamentRepository;
    private IPrescriptionRepository _prescriptionRepository;
    private IPatientRepository _patientRepository;
    private IDoctorRepository _doctorRepository;

    public PrescriptionAddUseCase(IPrescriptionAddingPolicy policy,
        IPrescriptionMedicamentRepository medicamentRepository, IPrescriptionRepository prescriptionRepository,
        IPatientRepository patientRepository, IDoctorRepository doctorRepository)
    {
        _policy = policy;
        _prescriptionMedicamentRepository = medicamentRepository;
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }
    
    public async Task<bool> AddPrescription(PrescriptionPostDTO prescriptionPostDto)
    {
        if (_policy.IsMedicamentListOver10Elements(prescriptionPostDto.Medicaments))
        {
            throw new DomainException("Prescription cannot have more than 10 medicaments");
        }

        
        foreach (var medicamentDoseDto in prescriptionPostDto.Medicaments)
        {
            try
            {
                await _policy.MedicamentExists(new Medicament()
                {
                    IdMedicament = medicamentDoseDto.IdMedicament,
                    Name = medicamentDoseDto.Name,
                    Description = medicamentDoseDto.Description,
                    Type = medicamentDoseDto.Type
                });
            }
            catch (DomainException e)
            {
                Console.WriteLine(e);
                throw new DomainException(e.Message);
            }
            
        }

        if (!_policy.IsDueDateGreaterThanDate(prescriptionPostDto.DueDate, prescriptionPostDto.Date))
            throw new DomainException("Cannot create prescription that has due date before creation date");

        Patient queriedPatient = new Patient()
        {
            FirstName = prescriptionPostDto.Patient.FirstName,
            LastName = prescriptionPostDto.Patient.LastName,
            Birthdate = prescriptionPostDto.Patient.BirthDate
        };

        int patientId = 0;
        if (!await _policy.PatientExists(queriedPatient))
        {
            patientId = await _patientRepository.CreatePatient(queriedPatient);
        }
        else
        {
           var patient =  await _patientRepository.GetPatientByPatientDetails(queriedPatient);
           patientId = patient.IdPatient;
        }

        Doctor? doc = await _doctorRepository.GetAnyDoctor();
        
        int prescriptionId = await _prescriptionRepository.AddPrescription(new Prescription()
        {
            IdPatient = patientId,
            Date = prescriptionPostDto.Date,
            DueDate = prescriptionPostDto.DueDate,
            IdDoctor = doc!.IdDoctor
        });
        foreach (var medicamentDoseDto in prescriptionPostDto.Medicaments)
        {
            await _prescriptionMedicamentRepository.AddPrescriptionMedicament(prescriptionId, medicamentDoseDto);
        }

        return true;
    }
}