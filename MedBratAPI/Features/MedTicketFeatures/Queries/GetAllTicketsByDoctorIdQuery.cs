using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.MedTicketFeatures.Queries
{
    public class GetAllTicketsByDoctorIdQuery : IRequest<IEnumerable<MedTicket>>
    {
        public int DoctorId { get; set; }

        public class GetAllTicketsByDoctorIdQueryHandler : IRequestHandler<GetAllTicketsByDoctorIdQuery, IEnumerable<MedTicket>>
        {
            private readonly IApplicationContext _context;

            public GetAllTicketsByDoctorIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<MedTicket>> Handle(GetAllTicketsByDoctorIdQuery query, CancellationToken cancellationToken)
            {
                var tickets = await _context.MedTickets
                    .Where(d => d.DoctorId == query.DoctorId)
                    .ToListAsync();
                if (tickets == null) return null;

                return tickets;
            }
        }
    }
}
