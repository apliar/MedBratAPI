using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.DoctorFeatures.Queries
{
    public class GetDoctorByIdQuery : IRequest<Doctor>
    {
        public int Id { get; set; }

        public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Doctor>
        {
            private readonly IApplicationContext _context;

            public GetDoctorByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Doctor> Handle(GetDoctorByIdQuery query, CancellationToken cancellationToken)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == query.Id);
                if (doctor == null) return null;

                return doctor;
            }
        }
    }
}
