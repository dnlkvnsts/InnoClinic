using FluentAssertions;
using InnoClinic.Services.Domain.Entities;
using InnoClinic.Services.Infrastructure.Persistence;
using InnoClinic.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Tests.Infrastructure
{
    public class ServiceRepositoryTests
    {

        private ServicesDbContext DbInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ServicesDbContext>().
                UseInMemoryDatabase(databaseName : Guid.NewGuid().ToString())
                .Options;

            return new ServicesDbContext(options);
        }



        [Fact]

        public async Task GetServicesAsync_ShouldReturnOnlyActiveServicesWithActiveSpecializations()
        {
            using var  context = DbInMemoryContext();
            var repository = new ServiceRepository(context);


            var activeSpec = new Specialization { Id = Guid.NewGuid(), SpecializationName = "Cardiology", IsActive = true };
            var inactiveSpec = new Specialization { Id = Guid.NewGuid(), SpecializationName = "Deprecated Spec", IsActive = false };
            var category = new ServiceCategory { Id = Guid.NewGuid(), CategoryName = "consultations", TimeSlotSize = 30 };



            var validServiceWithSpec = new Service
            {
                Id = Guid.NewGuid(),
                ServiceName = "Active Consultation",
                IsActive = true,
                Category = category,
                Specialization = activeSpec
            };


            var validServiceWithoutSpec = new Service
            {
                Id = Guid.NewGuid(),
                ServiceName = "Blood Test",
                IsActive = true,
                Category = category,
                Specialization = null
            };

            var inactiveService = new Service
            {
                Id = Guid.NewGuid(),
                ServiceName = "Old Consultation",
                IsActive = false,
                Category = category,
                Specialization = activeSpec
            };

            var serviceWithInactiveSpec = new Service
            {
                Id = Guid.NewGuid(),
                ServiceName = "Consultation for Inactive Spec",
                IsActive = true,
                Category = category,
                Specialization = inactiveSpec
            };


            context.Services.AddRange(

                validServiceWithSpec,
                validServiceWithoutSpec,
                inactiveService,
                serviceWithInactiveSpec

            );


            await context.SaveChangesAsync();



            var result = await repository.GetServicesAsync(CancellationToken.None);



            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(s => s.ServiceName == "Active Consultation");
            result.Should().Contain(s => s.ServiceName == "Blood Test");
            result.Should().NotContain(s => s.ServiceName == "Old Consultation");
            result.Should().NotContain(s => s.ServiceName == "Consultation for Inactive Spec");


        }





    }
}
