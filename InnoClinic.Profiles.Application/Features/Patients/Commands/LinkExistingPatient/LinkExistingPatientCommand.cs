using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Commands.LinkExistingPatient
{
    public record LinkExistingPatientCommand(
         Guid PatientId,
         Guid AccountId
     ) : IRequest<bool>;
}
