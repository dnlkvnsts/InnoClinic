using InnoClinic.Appointments.Domain;
using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Features.Doctors.Queries.GetDoctors;
using InnoClinic.Profiles.Application.Interfaces;
using MassTransit;
using MediatR;


namespace InnoClinic.Profiles.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IEnumerable<PatientDto>>
    {

        private readonly IPatientRepository _patientRepository;
     


        public GetPatientsQueryHandler(IPatientRepository patientRepository)
        {
           _patientRepository = patientRepository;
        }


        public async Task<IEnumerable<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = _patientRepository.GetPatientsQuery().ToList();



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
