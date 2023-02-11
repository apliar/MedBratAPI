using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.MedRecordFeatures.Queries
{
    public class GetAllRecordsByDoctorIdQuery : IRequest<IEnumerable<MedRecord>>
    {
        public int DoctorId { get; set; }

        public class GetAllRecordsByDoctorIdQueryHandler : IRequestHandler<GetAllRecordsByDoctorIdQuery, IEnumerable<MedRecord>>
        {
            private readonly IApplicationContext _context;

            public GetAllRecordsByDoctorIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<MedRecord>> Handle(GetAllRecordsByDoctorIdQuery query, CancellationToken cancellationToken)
            {
                var records = await _context.MedRecords
                    .Where(d => d.DoctorId == query.DoctorId)
                    .ToListAsync();
                if (records == null) return null;

                return records;
            }
        }
    }
}
