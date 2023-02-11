using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MedBratAPI.Features.CuratorFeatures.Commands
{
    public class DeleteCuratorByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCuratorByIdCommandHandler : IRequestHandler<DeleteCuratorByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteCuratorByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteCuratorByIdCommand command, CancellationToken cancellationToken)
            {
                var curator = await _context.Curators.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (curator == null) return default;

                _context.Curators.Remove(curator);
                await _context.SaveChanges();

                return curator.Id;
            }
        }
    }
}
