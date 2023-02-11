using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.MedTicketFeatures.Commands
{
    public class CreateTicketCommand : IRequest<MedTicket>
    {
        [JsonRequired]
        public int PatientId { get; set; }
        [JsonRequired]
        public int DoctorId { get; set; }
        [JsonRequired]
        public DateTime Time { get; set; }

        public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, MedTicket>
        {
            private readonly IApplicationContext _context;
            public CreateTicketCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<MedTicket> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
            {
                var ticket = new MedTicket();
                ticket.PatientId = command.PatientId;
                ticket.DoctorId = command.DoctorId;
                ticket.Time = command.Time;

                _context.MedTickets.Add(ticket);
                await _context.SaveChanges();

                return ticket;
            }
        }
    }
}
