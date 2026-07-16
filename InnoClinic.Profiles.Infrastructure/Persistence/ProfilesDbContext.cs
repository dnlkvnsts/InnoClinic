using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

          
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = therapyId,
                    SpecializationName = "Терапевт",
                    IsActive = true
                },
                new Specialization
                {
                    Id = surgeryId,
                    SpecializationName = "Хирург",
                    IsActive = true
                }
            );

        
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    FirstName = "John",
                    LastName = "Doe",
                    MiddleName = "Robert",
                    PhotoUrl = "https://example.com/photos/johndoe.jpg",
                    Specialization = "Cardiologist",
                    CareerStartYear = 2015,
                    Status = "At work", 
                    OfficeAddress = "123 Health Ave, Room 101",
                    UserId = "user-guid-1"
                },
                new Doctor
                {
                    Id = Guid.Parse("bb22bb22-2222-2222-2222-222222222222"),
                    FirstName = "Алексей",
                    LastName = "Петров",
                    MiddleName = "Николаевич",
                    DateOfBirth = new DateTime(1990, 8, 24),
                    PhotoUrl = null,
                    CareerStartYear = 2018,
                    Status = "At work",     
                    SpecializationId = surgeryId, 
                    OfficeAddress = "г. Минск, каб. 405",
                    AccountId = Guid.NewGuid()
                },
                new Doctor
                {
                    Id = Guid.Parse("cc33cc33-3333-3333-3333-333333333333"),
                    FirstName = "Ольга",
                    LastName = "Иванова", 
                    MiddleName = "Михайловна",
                    DateOfBirth = new DateTime(1978, 11, 2),
                    PhotoUrl = null,
                    CareerStartYear = 2005,
                    Status = "On vacation", 
                    SpecializationId = therapyId,
                    OfficeAddress = "г. Минск, каб. 302",
                    AccountId = Guid.NewGuid()
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
