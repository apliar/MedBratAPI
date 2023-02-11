using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.RoleFeatures.Commands
{
    public class DeleteRoleByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteRoleByIdCommandHandler : IRequestHandler<DeleteRoleByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteRoleByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteRoleByIdCommand command, CancellationToken cancellationToken)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (role == null) return default;

                _context.Roles.Remove(role);
                await _context.SaveChanges();

                return role.Id;
            }
        }
    }
}
