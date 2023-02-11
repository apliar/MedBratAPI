using MedBratAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedBratAPI.Context
{
    public interface IApplicationContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<Clinic> Clinics { get; set; }
        DbSet<Curator> Curators { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<MedRecord> MedRecords { get; set; }
        DbSet<MedTicket> MedTickets { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChanges();
    }
}