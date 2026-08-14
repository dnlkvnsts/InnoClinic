using InnoClinic.Profiles.Application.DTOs;
using InnoClinic.Profiles.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Queries.FindPatient
{
    public  class FindPatientQueryHandler : IRequestHandler<FindPatientQuery, PatientMatchResultDto >
    {
        private readonly IPatientRepository _patientRepository;

        public FindPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientMatchResultDto> Handle(FindPatientQuery request, CancellationToken cancellationToken)
        {
            var unlinkedPatients = await _patientRepository.GetUnlinkedPatientsAsync(cancellationToken);

            foreach (var patient in unlinkedPatients)
            {
                int score = 0;

                if (string.Equals(patient.FirstName, request.FirstName, StringComparison.OrdinalIgnoreCase))
                    score += 5;

                if (string.Equals(patient.LastName, request.LastName, StringComparison.OrdinalIgnoreCase))
                    score += 5;

                if (!string.IsNullOrEmpty(request.MiddleName) &&
                    string.Equals(patient.MiddleName, request.MiddleName, StringComparison.OrdinalIgnoreCase))
                    score += 5;

                if (patient.DateOfBirth.Date == request.DateOfBirth.Date)
                    score += 3;

                
                if (score >= 13)
                {
                    var dto = new PatientDto(
                        patient.FirstName,
                        patient.LastName,
                        patient.MiddleName,
                        patient.Phone,
                        patient.PhotoUrl,
                        patient.DateOfBirth,
                        patient.IsLinkedToAccount
                    );

                    return new PatientMatchResultDto(true, dto, patient.Id);
                }
            }

            return new PatientMatchResultDto(false, null, null);
        }


    }
}
