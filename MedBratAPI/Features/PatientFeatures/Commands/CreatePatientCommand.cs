using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.PatientFeatures.Commands
{
    public class CreatePatientCommand : CreateUserCommand, IRequest<Patient>
    {

        public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Patient>
        {
            private readonly IApplicationContext _context;
            public CreatePatientCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Patient> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
            {
                if (_context.Users.FirstOrDefault(c => c.Polis == command.Polis) != null)
                    return null;

                var patient = new Patient();
                patient.Name = command.Name;
                patient.Polis = command.Polis;
                patient.Email = command.Email;
                patient.DateOfBirth = command.DateOfBirth;
                patient.Sex = command.Sex;
                patient.Password = command.Password;
                patient.Role = _context.Roles.First(r => r.Name == "patient");

                _context.Patients.Add(patient);
                await _context.SaveChanges();

                return patient;
            }
        }
    }
}
