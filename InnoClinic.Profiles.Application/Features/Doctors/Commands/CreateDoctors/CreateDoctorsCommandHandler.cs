using InnoClinic.Shared.Contracts;
using InnoClinic.Profiles.Application.Interfaces;
using InnoClinic.Profiles.Domain.Entities;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Shared.Contracts;


namespace InnoClinic.Profiles.Application.Features.Doctors.Commands.CreateDoctors
{
    public class CreateDoctorsCommandHandler : IRequestHandler<CreateDoctorsCommand, Guid>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateDoctorsCommandHandler(IDoctorRepository doctorRepository, IPublishEndpoint publishEndpoint)
        {
            _doctorRepository = doctorRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateDoctorsCommand request, CancellationToken cancellationToken)
        {
            

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                DateOfBirth = request.DateOfBirth,
                PhotoUrl = request.PhotoUrl,
                CareerStartYear = request.CareerStartYear,
                Status = string.IsNullOrWhiteSpace(request.Status) ? "At work" : request.Status,
                SpecializationId = request.SpecializationId,
                OfficeAddress = request.OfficeAddress,
                AccountId = Guid.Empty,
            };


            await _doctorRepository.AddAsync(doctor, cancellationToken);

            await _publishEndpoint.Publish(new DoctorCreated(
                doctor.Id,
                request.Email,
                request.FirstName,
                request.LastName
                ), cancellationToken);


            return doctor.Id;
        }
    }
}
