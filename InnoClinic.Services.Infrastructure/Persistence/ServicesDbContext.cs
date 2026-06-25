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

        
            var consultationCategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var diagnosticsCategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var analysesCategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<ServiceCategory>().HasData(
                new ServiceCategory { Id = consultationCategoryId, CategoryName = "consultations", TimeSlotSize = 30 },
                new ServiceCategory { Id = diagnosticsCategoryId, CategoryName = "diagnostics", TimeSlotSize = 60 },
                new ServiceCategory { Id = analysesCategoryId, CategoryName = "analyses", TimeSlotSize = 15 }
            );

         
            var cardiologyId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var neurologyId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = cardiologyId, SpecializationName = "Кардиология", IsActive = true },
                new Specialization { Id = neurologyId, SpecializationName = "Неврология", IsActive = true }
            );

           
            modelBuilder.Entity<Service>().HasData(
                
                new Service
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "Первичный прием кардиолога",
                    Price = 1500,
                    IsActive = true,
                    CategoryId = consultationCategoryId,
                    SpecializationId = cardiologyId
                },
                new Service
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "Прием невролога",
                    Price = 1700,
                    IsActive = true,
                    CategoryId = consultationCategoryId,
                    SpecializationId = neurologyId
                },
                
                new Service
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "УЗИ сердца",
                    Price = 2500,
                    IsActive = true,
                    CategoryId = diagnosticsCategoryId,
                    SpecializationId = null
                },
               
                new Service
                {
                    Id = Guid.NewGuid(),
                    ServiceName = "Общий анализ крови",
                    Price = 500,
                    IsActive = true,
                    CategoryId = analysesCategoryId,
                    SpecializationId = null
                }
            );
        }


    }
}
