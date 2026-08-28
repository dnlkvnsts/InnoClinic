using InnoClinic.Profiles.Application.Interfaces;
using MassTransit;
using InnoClinic.Shared.Contracts;
using InnoClinic.Profiles.Domain.Entities;



namespace InnoClinic.Profiles.Infrastructure.Consumers
{
    public class DoctorAccountCreatedConsumer : IConsumer<DoctorAccountCreated>
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorAccountCreatedConsumer(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task Consume(ConsumeContext<DoctorAccountCreated> context)
        {
            var msg = context.Message;

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                AccountId = msg.AccountId, 
                FirstName = msg.FirstName,
                LastName = msg.LastName,
                MiddleName = msg.MiddleName,
                DateOfBirth = msg.DateOfBirth,
                PhotoUrl = msg.PhotoUrl,
                CareerStartYear = msg.CareerStartYear,
                Status = string.IsNullOrWhiteSpace(msg.Status) ? "At work" : msg.Status,
                SpecializationId = msg.SpecializationId,
                OfficeAddress = msg.OfficeAddress
            };

            await _doctorRepository.AddAsync(doctor, context.CancellationToken);
        }
    }
}
