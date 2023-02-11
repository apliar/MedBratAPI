using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.PatientFeatures.Commands
{
    public class DeletePatientByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeletePatientByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeletePatientByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeletePatientByIdCommand command, CancellationToken cancellationToken)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (patient == null) return default;

                _context.Patients.Remove(patient);
                await _context.SaveChanges();

                return patient.Id;
            }
        }
    }
}
