using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.ScheduleFeatures.Commands
{
    public class CreateScheduleCommand : IRequest<Schedule>
    {
        [JsonRequired]
        public int DoctorId { get; set; }

        public Dictionary<int, List<TimeSpan>> WeekSchedule { get; set; }
            = new Dictionary<int, List<TimeSpan>>();

        public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Schedule>
        {
            private readonly IApplicationContext _context;
            public CreateScheduleCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Schedule> Handle(CreateScheduleCommand command, CancellationToken cancellationToken)
            {
                if (_context.Schedules.FirstOrDefault(c => c.DoctorId == command.DoctorId) != null) return null;

                var schedule = new Schedule();
                schedule.DoctorId = command.DoctorId;
                schedule.WeekSchedule = command.WeekSchedule;

                _context.Schedules.Add(schedule);
                await _context.SaveChanges();

                return schedule;
            }
        }
    }
}
