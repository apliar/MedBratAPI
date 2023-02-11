using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.CityFeatures.Queries
{
    public class GetAllCitiesQuery : IRequest<IEnumerable<City>>
    {
        public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<City>>
        {
            private readonly IApplicationContext _context;

            public GetAllCitiesQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<City>> Handle(GetAllCitiesQuery query, CancellationToken cancellationToken)
            {
                var citiesList = await _context.Cities.ToListAsync();
                if (citiesList == null) return null;

                return citiesList.AsReadOnly();
            }
        }
    }
}

