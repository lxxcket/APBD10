using APBD10.DTOs;

namespace APBD10.UseCases;

public interface IPrescriptionAddUseCase
{
    Task<bool> AddPrescription(PrescriptionPostDTO prescriptionPostDto);
}