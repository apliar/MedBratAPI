using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CuratorFeatures.Commands
{
    public class CreateCuratorCommand : CreateUserCommand, IRequest<Curator>
    {
        public int? ClinicId { get; set; }

        public class CreateCuratorCommandHandler : IRequestHandler<CreateCuratorCommand, Curator>
        {
            private readonly IApplicationContext _context;
            public CreateCuratorCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<Curator> Handle(CreateCuratorCommand command, CancellationToken cancellationToken)
            {
                if (_context.Users.FirstOrDefault(c => c.Polis == command.Polis) != null)
                    return null;

                var curator = new Curator();
                curator.Name = command.Name;
                curator.Polis = command.Polis;
                curator.Email = command.Email;
                curator.DateOfBirth = command.DateOfBirth;
                curator.Sex = command.Sex;
                curator.Password = command.Password;
                curator.Role = _context.Roles.First(r => r.Name == "curator");
                curator.ClinicId = command.ClinicId;

                _context.Curators.Add(curator);
                await _context.SaveChanges();

                return curator;
            }
        }
    }
}
