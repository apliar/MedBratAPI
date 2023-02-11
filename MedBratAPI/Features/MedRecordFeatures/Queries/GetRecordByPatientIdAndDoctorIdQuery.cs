using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.MedRecordFeatures.Queries
{
    public class GetRecordByPatientIdAndDoctorIdQuery : IRequest<MedRecord>
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public class GetRecordByPatientIdAndDoctorIdQueryHandler : IRequestHandler<GetRecordByPatientIdAndDoctorIdQuery, MedRecord>
        {
            private readonly IApplicationContext _context;

            public GetRecordByPatientIdAndDoctorIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<MedRecord> Handle(GetRecordByPatientIdAndDoctorIdQuery query, CancellationToken cancellationToken)
            {
                var record = await _context.MedRecords
                    .FirstOrDefaultAsync(d => d.PatientId == query.PatientId && d.DoctorId == query.DoctorId);
                if (record == null) return null;

                return record;
            }
        }
    }
}
