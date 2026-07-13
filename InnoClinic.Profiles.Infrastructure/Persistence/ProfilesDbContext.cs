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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



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
                    Status = "At work", // Точно совпадает с фильтром!
                    OfficeAddress = "123 Health Ave, Room 101",
                    UserId = "user-guid-1"
                },
                new Doctor
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    FirstName = "Alice",
                    LastName = "Smith",
                    MiddleName = "Jane",
                    PhotoUrl = "https://example.com/photos/alicesmith.jpg",
                    Specialization = "Pediatrician",
                    CareerStartYear = 2018,
                    Status = "At work", // Точно совпадает с фильтром!
                    OfficeAddress = "123 Health Ave, Room 205",
                    UserId = "user-guid-2"
                }
            );

        }
    }
}
