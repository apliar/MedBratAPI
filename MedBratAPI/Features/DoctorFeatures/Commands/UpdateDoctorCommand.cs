using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.DoctorFeatures.Commands
{
    public class UpdateDoctorCommand : UpdateUserCommand, IRequest<int>
    {
        public string? Specialization { get; set; }

        public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateDoctorCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateDoctorCommand command, CancellationToken cancellationToken)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (doctor == null) return default;

                doctor.Name = command.Name;
                doctor.Polis = command.Polis;
                doctor.Email = command.Email;
                doctor.DateOfBirth = command.DateOfBirth;
                doctor.Sex = command.Sex;
                doctor.Specialization = command.Specialization;

                await _context.SaveChanges();

                return doctor.Id;
            }
        }
    }
}
