using InnoClinic.Profiles.Application.Contracts;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using MassTransit;


namespace InnoClinic.Profiles.Application.Consumers
{
    public  class SpecializationCreatedConsumer : IConsumer<SpecializationCreated>
    {
        private readonly ProfilesDbContext _context;

        public SpecializationCreatedConsumer(ProfilesDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<SpecializationCreated> context)
        {
            var message = context.Message;

            var localSpecialization = new Specialization
            {
                Id = message.Id,                       
                SpecializationName = message.SpecializationName,
                IsActive = true
            };

           
            _context.Specializations.Add(localSpecialization);
            await _context.SaveChangesAsync();
        }

    }
}
