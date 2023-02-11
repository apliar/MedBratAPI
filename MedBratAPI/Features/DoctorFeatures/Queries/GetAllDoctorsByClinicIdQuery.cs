using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.DoctorFeatures.Queries
{
    public class GetAllDoctorsByClinicIdQuery : IRequest<IEnumerable<Doctor>>
    {
        public int ClinicId { get; set; }

        public class GetAllDoctorsByClinicIdQueryHandler : IRequestHandler<GetAllDoctorsByClinicIdQuery, IEnumerable<Doctor>>
        {
            private readonly IApplicationContext _context;

            public GetAllDoctorsByClinicIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Doctor>> Handle(GetAllDoctorsByClinicIdQuery query, CancellationToken cancellationToken)
            {
                var doctorsList = await _context.Doctors
                    .Where(d => d.ClinicId == query.ClinicId)
                    .ToListAsync();
                if (doctorsList == null) return null;

                return doctorsList;
            }
        }
    }
}
