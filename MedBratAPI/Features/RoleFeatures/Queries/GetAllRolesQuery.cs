using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.RoleFeatures.Queries
{
    public class GetAllRolesQuery : IRequest<IEnumerable<Role>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<Role>>
        {
            private readonly IApplicationContext _context;

            public GetAllRolesQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
            {
                var rolesList = await _context.Roles.ToListAsync();
                if (rolesList == null) return null;

                return rolesList.AsReadOnly();
            }
        }
    }
}
