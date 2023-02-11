using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.MedRecordFeatures.Commands
{
    public class DeleteRecordCommand : IRequest<int>
    {
        [JsonRequired]
        public int PatientId { get; set; }
        [JsonRequired]
        public int DoctorId { get; set; }

        public class DeleteTicketByIdCommandHandler : IRequestHandler<DeleteRecordCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteTicketByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteRecordCommand command, CancellationToken cancellationToken)
            {
                var record = await _context.MedRecords
                    .FirstOrDefaultAsync(d => d.PatientId == command.PatientId && d.DoctorId == command.DoctorId);
                if (record == null) return default;

                _context.MedRecords.Remove(record);
                await _context.SaveChanges();

                return 1;
            }
        }
    }
}
