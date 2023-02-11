using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.CuratorFeatures.Queries
{
    public class GetAllCuratorsByClinicIdQuery : IRequest<IEnumerable<Curator>>
    {
        public int ClinicId { get; set; }

        public class GetAllCuratorsByClinicIdQueryHandler : IRequestHandler<GetAllCuratorsByClinicIdQuery, IEnumerable<Curator>>
        {
            private readonly IApplicationContext _context;

            public GetAllCuratorsByClinicIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Curator>> Handle(GetAllCuratorsByClinicIdQuery query, CancellationToken cancellationToken)
            {
                var curatorsList = await _context.Curators
                    .Where(d => d.ClinicId == query.ClinicId)
                    .ToListAsync();
                if (curatorsList == null) return null;

                return curatorsList;
            }
        }
    }
}
