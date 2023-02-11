using MedBratAPI.Context;
using MedBratAPI.Features.CommonFeatures.Commands;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CuratorFeatures.Commands
{
    public class UpdateCuratorCommand : UpdateUserCommand, IRequest<int>
    {

        public class UpdateCuratorCommandHandler : IRequestHandler<UpdateCuratorCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateCuratorCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateCuratorCommand command, CancellationToken cancellationToken)
            {
                var curator = await _context.Curators.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (curator == null) return default;

                curator.Name = command.Name;
                curator.Polis = command.Polis;
                curator.Email = command.Email;
                curator.DateOfBirth = command.DateOfBirth;
                curator.Sex = command.Sex;

                await _context.SaveChanges();

                return curator.Id;
            }
        }
    }
}
