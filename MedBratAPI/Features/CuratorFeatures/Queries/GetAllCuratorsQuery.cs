using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.CuratorFeatures.Queries
{
    public class GetAllCuratorsQuery : IRequest<IEnumerable<Curator>>
    {
        public class GetAllCuratorsQueryHandler : IRequestHandler<GetAllCuratorsQuery, IEnumerable<Curator>>
        {
            private readonly IApplicationContext _context;

            public GetAllCuratorsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Curator>> Handle(GetAllCuratorsQuery query, CancellationToken cancellationToken)
            {
                var curatorList = await _context.Curators.ToListAsync();
                if (curatorList == null) return null;

                return curatorList.AsReadOnly();
            }
        }
    }
}
