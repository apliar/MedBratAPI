using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.DoctorFeatures.Queries
{
    public class GetAllDoctorsQuery : IRequest<IEnumerable<Doctor>>
    {
        public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<Doctor>>
        {
            private readonly IApplicationContext _context;

            public GetAllDoctorsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Doctor>> Handle(GetAllDoctorsQuery query, CancellationToken cancellationToken)
            {
                var doctorList = await _context.Doctors.ToListAsync();
                if (doctorList == null) return null;

                return doctorList.AsReadOnly();
            }
        }
    }
}
