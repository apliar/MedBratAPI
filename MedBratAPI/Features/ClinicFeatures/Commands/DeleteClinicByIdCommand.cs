using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.ClinicFeatures.Commands
{
    public class DeleteClinicByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteClinicByIdCommandHandler : IRequestHandler<DeleteClinicByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteClinicByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteClinicByIdCommand command, CancellationToken cancellationToken)
            {
                var clinic = await _context.Clinics.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (clinic == null) return default;

                _context.Clinics.Remove(clinic);
                await _context.SaveChanges();

                return clinic.Id;
            }
        }
    }
}
