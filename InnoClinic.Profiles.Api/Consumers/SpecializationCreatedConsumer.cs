using InnoClinic.Profiles.Application.Contracts;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using InnoClinic.Shared.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace InnoClinic.Profiles.Infrastructure.Consumers
{
    public  class SpecializationCreatedConsumer : IConsumer<SpecializationCreated>
    {
        private readonly ProfilesDbContext _context;
        private readonly ILogger<SpecializationCreatedConsumer> _logger;

        public SpecializationCreatedConsumer(ProfilesDbContext context, ILogger<SpecializationCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SpecializationCreated> context)
        {
            var message = context.Message;

            var exists = await _context.Specializations
                .AnyAsync(s => s.Id == message.Id, context.CancellationToken);


            if (exists)
            {
                _logger.LogWarning("Specialization with Id {SpecializationId} already exists. Skipping duplicate message.", message.Id);
                return; 
            }

            var localSpecialization = new Specialization
            {
                Id = message.Id,                       
                SpecializationName = message.SpecializationName,
                IsActive = true
            };

           
            _context.Specializations.Add(localSpecialization);
            await _context.SaveChangesAsync(context.CancellationToken);
        }

    }
}
