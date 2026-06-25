using InnoClinic.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InnoClinic.Services.Infrastructure.Persistence
{
    public class ServicesDbContext : DbContext
    {

        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options) { }


        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
