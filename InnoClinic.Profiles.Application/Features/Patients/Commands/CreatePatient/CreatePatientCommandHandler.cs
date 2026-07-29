using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Application.Interfaces;
using MediatR;

namespace InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient
{
    public  class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
    {
        private readonly IPatientRepository _patientRepository;

        public CreatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }



        public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken token)
        {
            var newPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Phone = request.Phone,
                PhotoUrl = request.PhotoUrl,
                DateOfBirth = request.DateOfBirth,
                IsLinkedToAccount = true, 
                AccountId = request.AccountId
            };


            await _patientRepository.AddAsync(newPatient, token);

            return newPatient.Id;
        }
    }
}
