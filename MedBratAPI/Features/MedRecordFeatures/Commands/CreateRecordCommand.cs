using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.MedRecordFeatures.Commands
{
    public class CreateRecordCommand : IRequest<MedRecord>
    {
        [JsonRequired]
        public int PatientId { get; set; }
        [JsonRequired]
        public int DoctorId { get; set; }

        public class CreateTicketCommandHandler : IRequestHandler<CreateRecordCommand, MedRecord>
        {
            private readonly IApplicationContext _context;
            public CreateTicketCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<MedRecord> Handle(CreateRecordCommand command, CancellationToken cancellationToken)
            {
                if (_context.MedRecords.FirstOrDefault(c => c.PatientId == command.PatientId 
                                                            && c.DoctorId == command.DoctorId) != null)
                    return null;

                var record = new MedRecord();
                record.PatientId = command.PatientId;
                record.DoctorId = command.DoctorId;

                _context.MedRecords.Add(record);
                await _context.SaveChanges();

                return record;
            }
        }
    }
}
