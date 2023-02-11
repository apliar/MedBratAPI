using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.ScheduleFeatures.Queries
{
    public class GetScheduleByDoctorIdQuery : IRequest<Schedule>
    {
        public int DoctorId { get; set; }

        public class GetScheduleByDoctorIdQueryHandler : IRequestHandler<GetScheduleByDoctorIdQuery, Schedule>
        {
            private readonly IApplicationContext _context;

            public GetScheduleByDoctorIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Schedule> Handle(GetScheduleByDoctorIdQuery query, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FirstOrDefaultAsync(d => d.DoctorId == query.DoctorId);
                if (schedule == null) return null;

                return schedule;
            }
        }
    }
}
