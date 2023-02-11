using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.MedRecordFeatures.Commands
{
    public class CreateVisitForRecordCommand : IRequest<int>
    {
        [JsonRequired]
        public int PatientId { get; set; }
        [JsonRequired]
        public int DoctorId { get; set; }
        [JsonRequired]
        public Visit Visit { get; set; }

        public class UpdateScheduleCommandHandler : IRequestHandler<CreateVisitForRecordCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateScheduleCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateVisitForRecordCommand command, CancellationToken cancellationToken)
            {
                var record = await _context.MedRecords
                    .FirstOrDefaultAsync(d => d.PatientId == command.PatientId && d.DoctorId == command.DoctorId);
                if (record == null) return default;

                record.Visits.Add(command.Visit);

                _context.MedRecords.Update(record);
                await _context.SaveChanges();

                return 1;
            }
        }
    }
}
