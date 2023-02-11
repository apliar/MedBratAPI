using MedBratAPI.Context;
using MedBratAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Features.DoctorFeatures.Commands
{
    public class DeleteDoctorByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteDoctorByIdCommandHandler : IRequestHandler<DeleteDoctorByIdCommand, int>
        {
            private readonly IApplicationContext _context;
            public DeleteDoctorByIdCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteDoctorByIdCommand command, CancellationToken cancellationToken)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == command.Id);
                if (doctor == null) return default;

                _context.Doctors.Remove(doctor);
                await _context.SaveChanges();

                return doctor.Id;
            }
        }
    }
}
