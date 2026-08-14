using InnoClinic.Profiles.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Commands.LinkExistingPatient
{
    public class LinkExistingPatientCommandHandler : IRequestHandler<LinkExistingPatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;

        public LinkExistingPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<bool> Handle(LinkExistingPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
            if (patient == null) return false;

            patient.IsLinkedToAccount = true;
            patient.AccountId = request.AccountId;

            await _patientRepository.UpdateAsync(patient, cancellationToken);
            return true;
        }

    }
}
