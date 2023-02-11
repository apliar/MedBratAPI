using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CityFeatures.Commands
{
    public class CreateCityCommand : IRequest<City>
    {
        [JsonRequired]
        public string Name { get; set; }

        public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, City>
        {
            private readonly IApplicationContext _context;
            public CreateCityCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<City> Handle(CreateCityCommand command, CancellationToken cancellationToken)
            {
                if (_context.Cities.FirstOrDefault(c => c.Name == command.Name) != null) return null;

                var city = new City();
                city.Name = command.Name;

                _context.Cities.Add(city);
                await _context.SaveChanges();

                return city;
            }
        }
    }
}
