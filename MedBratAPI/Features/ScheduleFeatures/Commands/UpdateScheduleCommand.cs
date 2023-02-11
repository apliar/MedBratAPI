using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.ScheduleFeatures.Commands
{
    public class UpdateScheduleCommand : IRequest<int>
    {
        [JsonRequired]
        public int DoctorId { get; set; }

        public Dictionary<int, List<TimeSpan>> WeekSchedule { get; set; }
            = new Dictionary<int, List<TimeSpan>>();

        public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateScheduleCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateScheduleCommand command, CancellationToken cancellationToken)
            {
                var schedule = await _context.Schedules.FirstOrDefaultAsync(d => d.DoctorId == command.DoctorId);
                if (schedule == null) return default;

                schedule.WeekSchedule = command.WeekSchedule;

                await _context.SaveChanges();

                return schedule.Id;
            }
        }
    }
}
