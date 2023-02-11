using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.ScheduleFeatures.Commands
{
    public class DeleteScheduleByDoctorIdCommand : IRequest<int>
    {
        public int DoctorId { get; set; }

        public class DeleteScheduleByDoctorIdCommandHandler : IRequestHandler<DeleteScheduleByDoctorIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteScheduleByDoctorIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteScheduleByDoctorIdCommand command, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FirstOrDefaultAsync(d => d.DoctorId == command.DoctorId);
                if (schedule == null) return default;

                _context.Schedules.Remove(schedule);
                await _context.SaveChanges();

                return schedule.Id;
            }
        }
    }
}
