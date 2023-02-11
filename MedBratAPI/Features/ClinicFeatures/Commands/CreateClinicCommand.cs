using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.ClinicFeatures.Commands
{
    public class CreateClinicCommand : IRequest<Clinic>
    {
        [JsonRequired]
        public string Name { get; set; }
        [JsonRequired]
        public string? Address { get; set; }
        [JsonRequired]
        public int? CityId { get; set; }

        public class CreateClinicCommandHandler : IRequestHandler<CreateClinicCommand, Clinic>
        {
            private readonly IApplicationContext _context;
            public CreateClinicCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Clinic> Handle(CreateClinicCommand command, CancellationToken cancellationToken)
            {
                if (_context.Clinics.FirstOrDefault(c => c.Name == command.Name && c.Address == command.Address) != null) 
                    return null;

                var clinic = new Clinic();
                clinic.Name = command.Name;
                clinic.Address = command.Address;
                clinic.CityId = command.CityId;

                _context.Clinics.Add(clinic);
                await _context.SaveChanges();

                return clinic;
            }
        }
    }
}
