using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.CuratorFeatures.Queries
{
    public class GetCuratorByIdQuery : IRequest<Curator>
    {
        public int Id { get; set; }

        public class GetCuratorByIdQueryHandler : IRequestHandler<GetCuratorByIdQuery, Curator>
        {
            private readonly IApplicationContext _context;

            public GetCuratorByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Curator> Handle(GetCuratorByIdQuery query, CancellationToken cancellationToken)
            {
                var curator = await _context.Curators.FirstOrDefaultAsync(d => d.Id == query.Id);
                if (curator == null) return null;

                return curator;
            }
        }
    }
}
