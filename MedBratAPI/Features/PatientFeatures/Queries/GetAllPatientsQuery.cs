using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.PatientFeatures.Queries
{
    public class GetAllPatientsQuery : IRequest<IEnumerable<Patient>>
    {
        public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<Patient>>
        {
            private readonly IApplicationContext _context;

            public GetAllPatientsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Patient>> Handle(GetAllPatientsQuery query, CancellationToken cancellationToken)
            {
                var patientList = await _context.Patients.ToListAsync();
                if (patientList == null) return null;

                return patientList.AsReadOnly();
            }
        }
    }
}
