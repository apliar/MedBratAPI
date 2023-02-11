using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.RoleFeatures.Commands
{
    public class CreateRoleCommand : IRequest<Role>
    {
        [JsonRequired]
        public string Name { get; set; }

        public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role>
        {
            private readonly IApplicationContext _context;
            public CreateRoleCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Role> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
            {
                if (_context.Roles.FirstOrDefault(c => c.Name == command.Name) != null) return null;

                var role = new Role();
                role.Name = command.Name;

                _context.Roles.Add(role);
                await _context.SaveChanges();

                return role;
            }
        }
    }
}
