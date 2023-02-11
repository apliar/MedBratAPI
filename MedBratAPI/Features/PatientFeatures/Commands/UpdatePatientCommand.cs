using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommand : UpdateUserCommand, IRequest<int>
    {

        public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdatePatientCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (patient == null) return default;

                patient.Name = command.Name;
                patient.Polis = command.Polis;
                patient.Email = command.Email;
                patient.DateOfBirth = command.DateOfBirth;
                patient.Sex = command.Sex;

                await _context.SaveChanges();

                return patient.Id;
            }
        }
    }
}
