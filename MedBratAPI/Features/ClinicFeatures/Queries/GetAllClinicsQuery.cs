using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.ClinicFeatures.Queries
{
    public class GetAllClinicsQuery : IRequest<IEnumerable<Clinic>>
    {
        public class GetAllClinicsQueryHandler : IRequestHandler<GetAllClinicsQuery, IEnumerable<Clinic>>
        {
            private readonly IApplicationContext _context;

            public GetAllClinicsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Clinic>> Handle(GetAllClinicsQuery query, CancellationToken cancellationToken)
            {
                var clinicsList = await _context.Clinics.ToListAsync();
                if (clinicsList == null) return null;

                return clinicsList.AsReadOnly();
            }
        }
    }
}

