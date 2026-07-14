using InnoClinic.Appointments.Domain;
using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using InnoClinic.Profiles.Application.Interfaces;
using MassTransit;
using MassTransit.Transports;
using MediatR;


namespace InnoClinic.Profiles.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IEnumerable<PatientDto>>
    {

        private readonly IPatientRepository _patientRepository;
        private readonly IPublishEndpoint _publishEndpoint;



        public GetPatientsQueryHandler(IPatientRepository patientRepository, IPublishEndpoint publishEndpoint)
        {
            _patientRepository = patientRepository;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<IEnumerable<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = _patientRepository.GetPatientsQuery().ToList();

            foreach (var patient in patients)
            {
                await _publishEndpoint.Publish(new PatientCreated(
                    patient.Id,
                    patient.FirstName,
                    patient.LastName,
                    patient.MiddleName
                ), cancellationToken);
            }

            var result = patients.Select(p => new PatientDto(
                p.FirstName,
                p.LastName,
                p.MiddleName,
                p.DateOfBirth,
                p.IsLinkedToAccount
                )).ToList();

            return result;
        }




    }
}
