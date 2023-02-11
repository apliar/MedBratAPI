using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.MedRecordFeatures.Queries
{
    public class GetAllRecordsByPatientIdQuery : IRequest<IEnumerable<MedRecord>>
    {
        public int PatientId { get; set; }

        public class GetAllRecordsByPatientIdQueryHandler : IRequestHandler<GetAllRecordsByPatientIdQuery, IEnumerable<MedRecord>>
        {
            private readonly IApplicationContext _context;

            public GetAllRecordsByPatientIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<MedRecord>> Handle(GetAllRecordsByPatientIdQuery query, CancellationToken cancellationToken)
            {
                var records = await _context.MedRecords
                    .Where(d => d.PatientId == query.PatientId)
                    .ToListAsync();
                if (records == null) return null;

                return records;
            }
        }
    }
}
