using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace InnoClinic.Profiles.Infrastructure.Persistence
{
    public class ProfilesDbContext : DbContext
    {
        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

     
            var therapyId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var surgeryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var cardiologyId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = therapyId,
                    SpecializationName = "Therapist",
                    IsActive = true
                },
                new Specialization
                {
                    Id = surgeryId,
                    SpecializationName = "Surgeon",
                    IsActive = true
                },
                new Specialization
                {
                    Id = cardiologyId,
                    SpecializationName = "Cardiologist",
                    IsActive = true
                }
            );

          
            modelBuilder.Entity<Doctor>().HasData(
               
                new Doctor
                {
                    Id = Guid.Parse("bb22bb22-2222-2222-2222-222222222222"),
                    FirstName = "Alex",
                    LastName = "Petrov",
                    MiddleName = "Nikolaevich",
                    DateOfBirth = new DateTime(1990, 8, 24, 0, 0, 0, DateTimeKind.Utc),
                    PhotoUrl = null,
                    CareerStartYear = 2018,
                    Status = "At work",
                    SpecializationId = surgeryId,
                    OfficeAddress = "Minsk, office 405",
                    AccountId = Guid.Parse("55555555-5555-5555-5555-555555555555")
                },
               
                new Doctor
                {
                    Id = Guid.Parse("cc33cc33-3333-3333-3333-333333333333"),
                    FirstName = "Olga",
                    LastName = "Ivanova",
                    MiddleName = "Mikhailovna",
                    DateOfBirth = new DateTime(1978, 11, 2, 0, 0, 0, DateTimeKind.Utc),
                    PhotoUrl = null,
                    CareerStartYear = 2005,
                    Status = "On vacation",
                    SpecializationId = therapyId,
                    OfficeAddress = "Minsk, office 302",
                    AccountId = Guid.Parse("66666666-6666-6666-6666-666666666666")
                },
               
                new Doctor
                {
                    Id = Guid.Parse("dd44dd44-4444-4444-4444-444444444444"),
                    FirstName = "Alex",
                    LastName = "Smith",
                    MiddleName = "John",
                    DateOfBirth = new DateTime(1985, 3, 12, 0, 0, 0, DateTimeKind.Utc),
                    PhotoUrl = null,
                    CareerStartYear = 2012,
                    Status = "At work",
                    SpecializationId = cardiologyId,
                    OfficeAddress = "Minsk, office 210",
                    AccountId = Guid.Parse("77777777-7777-7777-7777-777777777777")
                }
            );

            
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    FirstName = "Emily",
                    LastName = "Brown",
                    MiddleName = "Grace",
                    Phone = "+1234567890",
                    PhotoUrl = "https://example.com/photos/emilybrown.jpg",
                    IsLinkedToAccount = true,
                    DateOfBirth = new DateTime(1995, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                    AccountId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },
                new Patient
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    FirstName = "James",
                    LastName = "Wilson",
                    MiddleName = null,
                    Phone = "+10987654321",
                    PhotoUrl = null,
                    IsLinkedToAccount = false,
                    DateOfBirth = new DateTime(1988, 11, 23, 0, 0, 0, DateTimeKind.Utc),
                    AccountId = null
                }
            );
        }
    }
}