using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.PatientFeatures.Queries
{
    public class GetPatientByIdQuery : IRequest<Patient>
    {
        public int Id { get; set; }

        public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
        {
            private readonly IApplicationContext _context;

            public GetPatientByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Patient> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == query.Id);
                if (patient == null) return null;

                return patient;
            }
        }
    }
}
