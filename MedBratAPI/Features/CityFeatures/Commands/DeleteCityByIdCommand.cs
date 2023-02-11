using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.CityFeatures.Commands
{
    public class DeleteCityByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCityByIdCommandHandler : IRequestHandler<DeleteCityByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteCityByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteCityByIdCommand command, CancellationToken cancellationToken)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (city == null) return default;

                _context.Cities.Remove(city);
                await _context.SaveChanges();

                return city.Id;
            }
        }
    }
}
