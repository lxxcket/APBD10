using APBD10.DTOs;
using APBD10.Entities;
using APBD10.Exceptions;
using APBD10.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace APBD10.Controllers;

[ApiController]
[Route("/prescriptionapp")]
public class PrescriptionAppController : ControllerBase
{
    private IPrescriptionAddUseCase _prescriptionAddUseCase;
    private IGetPatientUseCase _getPatientUseCase;

    public PrescriptionAppController(IPrescriptionAddUseCase prescriptionAddUseCase, 
        IGetPatientUseCase getPatientUseCase)
    {
        _prescriptionAddUseCase = prescriptionAddUseCase;
        _getPatientUseCase = getPatientUseCase;
    }
    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionPostDTO prescriptionPostDto)
    {
        try
        {
            await _prescriptionAddUseCase.AddPrescription(prescriptionPostDto);
        }
        catch (DomainException e)
        {
            return BadRequest(new { message = e.Message });
        }

        return Ok("Success");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientInformation(int id)
    {
        PatientWithPrescriptionsDTO patient;
        try
        {
            patient = await _getPatientUseCase.GetPatient(id);
        }
        catch (DomainException e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
        return Ok(patient);
    }
    
    
}