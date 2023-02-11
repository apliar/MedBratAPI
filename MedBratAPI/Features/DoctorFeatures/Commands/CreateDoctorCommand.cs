using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.DoctorFeatures.Commands
{
    public class CreateDoctorCommand : CreateUserCommand, IRequest<Doctor>
    {
        public string? Specialization { get; set; }
        public int? ClinicId { get; set; }

        public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Doctor>
        {
            private readonly IApplicationContext _context;
            public CreateDoctorCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Doctor> Handle(CreateDoctorCommand command, CancellationToken cancellationToken)
            {
                if (_context.Users.FirstOrDefault(c => c.Polis == command.Polis) != null)
                    return null;

                var doctor = new Doctor();
                doctor.Name = command.Name;
                doctor.Polis = command.Polis;
                doctor.Email = command.Email;
                doctor.DateOfBirth = command.DateOfBirth;
                doctor.Sex = command.Sex;
                doctor.Password = command.Password;
                doctor.Specialization = command.Specialization;
                doctor.Role = _context.Roles.First(r => r.Name == "doctor");
                doctor.ClinicId = command.ClinicId;
                doctor.Schedule = new Schedule();

                _context.Users.Add(doctor);
                await _context.SaveChanges();

                return doctor;
            }
        }
    }
}
