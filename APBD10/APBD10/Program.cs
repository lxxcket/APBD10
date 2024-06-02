using APBD10.Contexts;
using APBD10.Entities;
using APBD10.Policies;
using APBD10.Repositories;
using APBD10.UseCases;
using Microsoft.EntityFrameworkCore;

public class Program
{
    
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
        builder.Services.AddDbContext<MedicamentContext>(option => option.UseSqlServer(connectionString));
        //
        builder.Services.AddScoped<IPrescriptionAddingPolicy, PrescriptionAddingPolicy>();
        //
        builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
        builder.Services.AddScoped<IPrescriptionMedicamentRepository, PrescriptionMedicamentRepository>();
        builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
        builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        //
        builder.Services.AddScoped<IPrescriptionAddUseCase, PrescriptionAddUseCase>();
        builder.Services.AddScoped<IGetPatientUseCase, GetPatientUseCase>();
        // builder.Services.AddScoped<IGetTripsUseCase, GetTripsUseCase>();
        // builder.Services.AddScoped<IDeleteClientUseCase, DeleteClientUseCase>();
        
        var app = builder.Build();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}