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
                    Id = Guid.Parse("aa11aa11-1111-1111-1111-111111111111"),
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Сергеевич",
                    DateOfBirth = new DateTime(1985, 5, 12),
                    PhotoUrl = "https://example.com/photos/ivanov.jpg",
                    CareerStartYear = 2010, 
                    Status = "At work",    
                    SpecializationId = therapyId, 
                    OfficeAddress = "г. Минск, каб. 301",
                    AccountId = Guid.NewGuid()
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
        }
    }
}
