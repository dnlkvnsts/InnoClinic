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
        }
    }
}
