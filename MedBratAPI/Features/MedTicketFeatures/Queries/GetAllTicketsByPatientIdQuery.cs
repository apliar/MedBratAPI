using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.MedTicketFeatures.Queries
{
    public class GetAllTicketsByPatientIdQuery : IRequest<IEnumerable<MedTicket>>
    {
        public int PatientId { get; set; }

        public class GetAllTicketsByPatientIdQueryHandler : IRequestHandler<GetAllTicketsByPatientIdQuery, IEnumerable<MedTicket>>
        {
            private readonly IApplicationContext _context;

            public GetAllTicketsByPatientIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<MedTicket>> Handle(GetAllTicketsByPatientIdQuery query, CancellationToken cancellationToken)
            {
                var tickets = await _context.MedTickets
                    .Where(d => d.PatientId == query.PatientId)
                    .ToListAsync();
                if (tickets == null) return null;

                return tickets;
            }
        }
    }
}
