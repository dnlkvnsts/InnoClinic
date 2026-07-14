using InnoClinic.Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace InnoClinic.Appointments.Infrastructure.Persistence
{
    public class AppointmentsDbContext : DbContext
    {

        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options) { }


        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Service> Services { get; set; }


        public DbSet<Patient> Patients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors"); 
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MiddleName).HasMaxLength(100);
            });


            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Price).HasColumnType("numeric(18,2)").IsRequired();
            });



            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MiddleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Price).HasColumnType("numeric(18,2)").IsRequired();
            });


            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PatientId).IsRequired();
                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.ServiceId).IsRequired();

                entity.Property(e => e.Date).HasColumnType("date").IsRequired();
                entity.Property(e => e.Time).HasColumnType("time").IsRequired();
                entity.Property(e => e.IsApproved).IsRequired();


                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(a => a.Service)                 
                    .WithMany(s => s.Appointments)             
                    .HasForeignKey(a => a.ServiceId)            
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Patient)
                    .WithMany(s => s.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
