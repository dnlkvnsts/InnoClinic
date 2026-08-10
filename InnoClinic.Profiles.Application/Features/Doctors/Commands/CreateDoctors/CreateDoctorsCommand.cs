using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Profiles.Application.Features.Doctors.Commands.CreateDoctors
{
    public record CreateDoctorsCommand(
        string FirstName,
        string LastName,
        string? MiddleName,
        DateTime DateOfBirth,
        string Email,
        Guid SpecializationId,
        string OfficeAddress,
        int CareerStartYear,
        string Status = "At work",
        string? PhotoUrl = null
    ) : IRequest<Guid>;
}
