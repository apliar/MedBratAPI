using MedBratAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MedBratAPI.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Curator> Curators { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Clinic> Clinics { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<MedRecord> MedRecords { get; set; } = null!;
        public DbSet<MedTicket> MedTickets { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Doctors)
                .WithMany(d => d.Patients)
                .UsingEntity<MedRecord>(
                    j => j
                        .HasOne(pt => pt.Doctor)
                        .WithMany(t => t.MedRecords)
                        .HasForeignKey(pt => pt.DoctorId),
                    j => j
                        .HasOne(pt => pt.Patient)
                        .WithMany(p => p.MedRecords)
                        .HasForeignKey(pt => pt.PatientId),
                    j =>
                    {
                        j.HasKey(t => new { t.PatientId, t.DoctorId });
                        j.ToTable("MedRecords");
                        j.Property(r => r.Visits)
                        .HasConversion(
                            w => JsonConvert.SerializeObject(w),
                            w => JsonConvert.DeserializeObject<List<Visit>>(w));
                    });
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Doctors)
                .WithMany(d => d.Patients)
                .UsingEntity<MedTicket>(
                    j => j
                        .HasOne(pt => pt.Doctor)
                        .WithMany(t => t.MedTickets)
                        .HasForeignKey(pt => pt.DoctorId),
                    j => j
                        .HasOne(pt => pt.Patient)
                        .WithMany(p => p.MedTickets)
                        .HasForeignKey(pt => pt.PatientId),
                    j =>
                    {
                        j.HasKey(t => t.Id);
                        j.ToTable("MedTickets");
                    });
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Schedule)
                .WithOne(s => s.Doctor)
                .HasForeignKey<Schedule>(s => s.DoctorId);
            modelBuilder.Entity<Schedule>()
                .Property(s => s.WeekSchedule)
                .HasConversion(
                    w => JsonConvert.SerializeObject(w),
                    w => JsonConvert.DeserializeObject<Dictionary<int, List<TimeSpan>>>(w));

            base.OnModelCreating(modelBuilder);
        }
    }
}
