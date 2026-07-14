using InnoClinic.Appointments.Domain;
using InnoClinic.Appointments.Domain.Entities;
using InnoClinic.Appointments.Infrastructure.Persistence;
using MassTransit;


namespace InnoClinic.Appointments.Infrastructure.Consumers
{
    public class ServiceCreatedConsumer : IConsumer<ServiceCreated>
    {
        private readonly AppointmentsDbContext _context;


        public ServiceCreatedConsumer(AppointmentsDbContext context)
        {
            _context = context;
        }




        public async Task Consume(ConsumeContext<ServiceCreated> context)
        {
            var message = context.Message;

            var existingService = await _context.Services.FindAsync(message.Id);
            if (existingService != null) return;


            var service = new Service
            {
                Id = message.Id,
                ServiceName = message.ServiceName,
                Price = message.Price
            };


            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }



    }
}
