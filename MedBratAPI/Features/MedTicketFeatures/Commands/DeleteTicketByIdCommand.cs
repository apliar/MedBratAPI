using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.MedTicketFeatures.Commands
{
    public class DeleteTicketByIdCommand : IRequest<Guid>
    {
        [JsonRequired]
        public Guid Id { get; set; }

        public class DeleteTicketByIdCommandHandler : IRequestHandler<DeleteTicketByIdCommand, Guid>
        {
            private readonly IApplicationContext _context;
            public DeleteTicketByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(DeleteTicketByIdCommand command, CancellationToken cancellationToken)
            {
                var ticket = await _context.MedTickets.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (ticket == null) return default;

                _context.MedTickets.Remove(ticket);
                await _context.SaveChanges();

                return ticket.Id;
            }
        }
    }
}
