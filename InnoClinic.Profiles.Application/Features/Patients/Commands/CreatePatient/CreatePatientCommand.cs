using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Patients.Commands.CreatePatient
{
    public record CreatePatientCommand(
        string FirstName,
        string LastName,
        string? MiddleName,
        string Phone,
        DateTime DateOfBirth,
        string? PhotoUrl,
        Guid AccountId
    ) : IRequest<Guid>;
}
